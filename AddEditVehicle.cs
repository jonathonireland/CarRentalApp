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
    public partial class AddEditVehicle : Form
    {
        private bool isEditMode;
        private readonly CarRentalEntities _db;
        public AddEditVehicle()
        {
            InitializeComponent();
            lblTitle.Text = "Add New Vehicle";
            isEditMode = false;
            _db = new CarRentalEntities();

        }

        public AddEditVehicle(TypesOfCar carToEdit) 
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle";
            PopulateFields(carToEdit);
            isEditMode = true;
        }

        private void PopulateFields(TypesOfCar car)
        {
            lblId.Text = car.id.ToString();
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbVIN.Text = car.VIN;
            tbYear.Text = car.Year.ToString();
            tbLicensePlateNumber.Text = car.LicensePlateNumber;
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                // Edit Code
                var id = int.Parse(lblId.Text);
                var car = _db.TypesOfCars.FirstOrDefault(q => q.id == id);
                car.Model = tbModel.Text;
                car.Make = tbMake.Text;
                car.Year = int.Parse(tbYear.Text);
                car.VIN = tbVIN.Text;
                car.LicensePlateNumber = tbLicensePlateNumber.Text;

                _db.SaveChanges();
            }
            else 
            {
                // Add Code Here
                var newCar = new TypesOfCar
                {
                    LicensePlateNumber = tbLicensePlateNumber.Text,
                    Make = tbMake.Text,
                    Model = tbModel.Text,
                    VIN = tbVIN.Text,
                    Year = int.Parse(tbYear.Text)
                };

                _db.TypesOfCars.Add(newCar);
                _db.SaveChanges();

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
