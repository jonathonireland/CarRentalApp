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
    
    public partial class ManageUsers : Form
    {
        private readonly CarRentalEntities _db;
        public ManageUsers()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            // not implemented yet
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of selected Row
                var id = (int)gvUserList.SelectedRows[0].Cells["id"].Value;
                // Query database for record
                var user = _db.Users.FirstOrDefault(q => q.id == id);
                var genericPassword = "Password@123";
                var new_password = Utils.HashPassword(genericPassword);
                user.password = new_password;
                _db.SaveChanges();

                MessageBox.Show($"{user.username}'s Password has been reset!");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnDeactivateUser_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of selected Row
                var id = (int)gvUserList.SelectedRows[0].Cells["id"].Value;
                // Query database for record
                var user = _db.Users.FirstOrDefault(q => q.id == id);
                user.isActive = user.isActive == true ? false : true;
                _db.SaveChanges();

                MessageBox.Show($"{user.username}'s Active Status has changed!");

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
