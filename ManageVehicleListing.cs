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
            // Get Id of selected Row
            var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

            // Query database for record
            var car = _db.TypesOfCars.FirstOrDefault(q => q.id == id);
            
            // Launch Add Edit Vehicle window with data
            var addEditVehicle = new AddEditVehicle(car);
            addEditVehicle.MdiParent = this.MdiParent;
            addEditVehicle.Show();
        }

        private void btnAddNewCar_Click(object sender, EventArgs e)
        {
            AddEditVehicle addEditVehicle = new AddEditVehicle();
            addEditVehicle.MdiParent = this.MdiParent;
            addEditVehicle.Show();
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            // Get Id of selected Row
            var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

            // Query database for record
            var car = _db.TypesOfCars.FirstOrDefault(q => q.id == id);

            // delete vehicle from table
            _db.TypesOfCars.Remove(car);
            _db.SaveChanges();

            gvVehicleList.Refresh();
        }
    }
}
