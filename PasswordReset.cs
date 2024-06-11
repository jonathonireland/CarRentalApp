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
    
    public partial class PasswordReset : Form
    {
        private readonly CarRentalEntities _db;
        private User _user;
        public PasswordReset(User user)
        {
            InitializeComponent();
            _db = new CarRentalEntities();
            _user = user;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // query database for current user id and update the password saved
                var password = tbPassword.Text;
                var confirmPassword = tbPassword2.Text;
                var user = _db.Users.FirstOrDefault(q => q.id == _user.id);
                if (password != confirmPassword)
                {
                    MessageBox.Show("Passwords do not match. Please try again!");

                }
                else
                {
                    user.password = Utils.HashPassword(password);
                    _db.SaveChanges();
                    MessageBox.Show($"Password for {user.username} has been reset.");
                }

            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Password reset operation has created an error: {ex}");
            }
            
        }
    }
}
