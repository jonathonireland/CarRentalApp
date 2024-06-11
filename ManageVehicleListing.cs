﻿using System;
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
    public partial class ManageVehicleListing : Form
    {
        private readonly CarRentalEntities _db;
        public ManageVehicleListing()
        {
            InitializeComponent();
            _db = new CarRentalEntities(); 
        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {
            //Select * from TypeOfCars
            //var cars = _db.TypesOfCars.ToList();
            /*var cars = _db.TypesOfCars
                .Select(q => new { CarId = q.id, CarName = q.Make })
                .ToList();*/
            var cars = _db.TypesOfCars
                .Select(q => new { 
                    Make = q.Make, 
                    Model = q.Model, 
                    VIN = q.VIN, 
                    Year = q.Year, 
                    LicensePlateNumber = q.LicensePlateNumber,
                    q.id
                })
                .ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            gvVehicleList.Columns[5].Visible = false;
            /*gvVehicleList.Columns[0].HeaderText = "ID";
            gvVehicleList.Columns[1].HeaderText = "NAME";*/

        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of selected Row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                // Query database for record
                var car = _db.TypesOfCars.FirstOrDefault(q => q.id == id);
            
                // Launch Add Edit Vehicle window with data
                var addEditVehicle = new AddEditVehicle(car, this);
                addEditVehicle.MdiParent = this.MdiParent;
                addEditVehicle.Show();
            } 
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnAddNewCar_Click(object sender, EventArgs e)
        {
            try 
            { 
            
                AddEditVehicle addEditVehicle = new AddEditVehicle(this);
                addEditVehicle.MdiParent = this.MdiParent;
                addEditVehicle.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of selected Row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                // Query database for record
                var car = _db.TypesOfCars.FirstOrDefault(q => q.id == id);

                DialogResult dr = MessageBox.Show("Are you Sure You Want To Delete This Record?",
                    "Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);
                
                if(dr == DialogResult.Yes)
                {

                    // delete vehicle from table
                    _db.TypesOfCars.Remove(car);
                    _db.SaveChanges();
                }
                PopulateGrid();
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Simple Refresh Option
            PopulateGrid();
        }

        public void PopulateGrid()
        {
            // Select a custom model collection of cars from database
            var cars = _db.TypesOfCars
                .Select(
                    q => new
                    {
                        Make = q.Make,
                        Model = q.Model,
                        VIN = q.VIN,
                        Year = q.Year,
                        LicensePlateNumber = q.LicensePlateNumber,
                        q.id
                    }
                )
                .ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            //Hide the column for ID. Changed from the hard coded column value to the name, 
            // to make it more dynamic. 
            gvVehicleList.Columns["Id"].Visible = false;
        }
    }
}
