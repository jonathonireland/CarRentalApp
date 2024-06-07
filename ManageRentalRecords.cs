using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class ManageRentalRecords : Form
    {
        private readonly CarRentalEntities _db;
        public ManageRentalRecords()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void ManageRentalRecords_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void PopulateGrid()
        {
            // Select a custom model collection of records from database
            var records = _db.CarRentalRecords.Select(q => new 
            { 
                Customer = q.CustomerName,
                DateIn = q.DateReturned,
                DateOut = q.DateRented,
                Id = q.id,
                q.Cost,
                Car = q.TypesOfCar.Make + " " + q.TypesOfCar.Model
            }).ToList();

            gvRecordList.DataSource = records;
            gvRecordList.Columns["DateIn"].HeaderText = "Date In";
            gvRecordList.Columns["DateOut"].HeaderText = "Date Out";
            // Hide the column for ID. Changed from the hard coded column value to the name, 
            // to make it more dynmaic
            gvRecordList.Columns["Id"].Visible = false;

        }

        private void btnAddNewRecord_Click(object sender, EventArgs e)
        {
            var AddRentalRecord = new AddEditRentalRecord 
            { 
                MdiParent = this.MdiParent
            };
            AddRentalRecord.Show();
        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of selected Row
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;

                // Query database for record
                var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);

                // Launch Add Edit Vehicle window with data
                var addEditRentalRecord = new AddEditRentalRecord(record);
                addEditRentalRecord.MdiParent = this.MdiParent;
                addEditRentalRecord.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of selected Row
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;

                // Query database for record
                var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);

                // delete vehicle from table
                _db.CarRentalRecords.Remove(record);
                _db.SaveChanges();

                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }
    }
}
