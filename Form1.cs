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
                    MessageBox.Show(
                        $"Customer Name: {customerName}\n\r" +
                        $"Date Rented: {dateOut}\n\r" +
                        $"Date Returned: {dateIn}\n\r" +
                        $"Cost: {cost}\n\r" +
                        $"Car Type: {carType}\n\r" +
                        $"THANK YOU FOR YOUR RENTAL!!");
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
    }
}
