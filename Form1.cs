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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            /*MessageBox.Show($"Thank you for renting: {tbCustomerName.Text} " +
                $"Date rented: {dpDateRented.Value} " +
                $"Date returned: {dpDateReturned.Value} " +
                $"Car Type: {cbTypeOfCar.Text}");*/

            // assign variables to user inputs
            // strings
            string customerName = tbCustomerName.Text;
            string dateOut = dpDateRented.Value.ToString();
            string dateIn = dpDateReturned.Value.ToString();
            // var
            var carType = cbTypeOfCar.SelectedItem.ToString();


            MessageBox.Show($"Customer Name: {customerName}\n\r" +
                $"Date Rented: {dateOut}\n\r" +
                $"Date Returned: {dateIn}\n\r" + 
                $"Car Type: {carType}\n\r" +
                $"THANK YOU FOR YOUR RENTAL!!");
        }
    }
}
