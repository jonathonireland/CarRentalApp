using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class AddEditRentalRecord : Form
    {
        private bool isEditMode;
        private readonly CarRentalEntities _db;
       
        public AddEditRentalRecord()
        {
            InitializeComponent();
            lblTitle.Text = "Add New Record";
            this.Text = "Add New Record";
            isEditMode = false;
            _db = new CarRentalEntities();
        }

        public AddEditRentalRecord(CarRentalRecord recordToEdit)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Rental Record";
            this.Text = "Edit Rental Record";
            if (recordToEdit == null)
            {
                MessageBox.Show("Please ensure that you selected a valid record to edit.");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new CarRentalEntities();
                PopulateFields(recordToEdit);
            }
        }

        private void PopulateFields(CarRentalRecord recordToEdit)
        {
            tbCustomerName.Text = recordToEdit.CustomerName;
            dpDateRented.Value = (DateTime)recordToEdit.DateRented;
            dpDateReturned.Value = (DateTime)recordToEdit.DateReturned;
            tbCost.Text = recordToEdit.Cost.ToString();
            lblRecordId.Text = recordToEdit.id.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // assign variables to user inputs
                // strings
                string customerName = tbCustomerName.Text;
                // var
                var dateOut = dpDateRented.Value;
                var dateIn = dpDateReturned.Value;
                var carType = cbTypeOfCar.Text;
                var isValid = true;
                var errorMessage = "";
                // double
                double cost = Convert.ToDouble(tbCost.Text);

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(carType))
                {
                    errorMessage += "Please Provide Missing data \n\r";
                    isValid = false;
                }

                if(dateOut > dateIn)
                {
                    errorMessage += "Illegal Date Selection \n\r";
                    isValid = false;
                }

                // can be if(isValid == true) 
                if (isValid)
                {
                    var rentalRecord = new CarRentalRecord();

                    if (isEditMode)
                    {
                        var id = int.Parse(lblRecordId.Text);
                        rentalRecord = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);
                    }

                    rentalRecord.CustomerName = customerName;
                    rentalRecord.DateRented = dateOut;
                    rentalRecord.DateReturned = dateIn;
                    rentalRecord.Cost = (decimal)cost;
                    rentalRecord.TypeOfCarId = (int)cbTypeOfCar.SelectedValue;
                    
                    if(!isEditMode)
                        _db.CarRentalRecords.Add(rentalRecord);

                    _db.SaveChanges();

                    MessageBox.Show(
                        $"Customer Name: {customerName}\n\r" +
                        $"Date Rented: {dateOut}\n\r" +
                        $"Date Returned: {dateIn}\n\r" +
                        $"Cost: {cost}\n\r" +
                        $"Car Type: {carType}\n\r" +
                        $"THANK YOU FOR YOUR RENTAL!!");

                    Close();

                } 
                else
                {
                    MessageBox.Show("Error: " + errorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // throw;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Select * From Rental TypesOfCars
            /*var cars = carRentalEntities.TypesOfCars.ToList();*/
            var cars = _db.TypesOfCars
                .Select(q => new { Id = q.id, Name = q.Make + " " + q.Model })
                .ToList();
            cbTypeOfCar.DisplayMember = "Name";
            cbTypeOfCar.ValueMember = "id";
            cbTypeOfCar.DataSource = cars;
        }

      
    }
}
