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
            var OpenForms = Application.OpenForms.Cast<Form>();
            var isOpen = OpenForms.Any(q => q.Name == "AddUser");
            if (!isOpen)
            {
                var addUser = new AddUser(this);
                addUser.MdiParent = this.MdiParent;
                addUser.Show();
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of selected Row
                var id = (int)gvUserList.SelectedRows[0].Cells["id"].Value;
                // Query database for record
                var user = _db.Users.FirstOrDefault(q => q.id == id);
                user.password = Utils.DefaultHashPassword();
                _db.SaveChanges();
                MessageBox.Show($"{user.username}'s Password has been reset!");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public void PopulateGrid()
        {
            // Select a custom model collection of cars from database
            var users = _db.Users
                .Select(
                    q => new
                    {
                        q.id,
                        q.username,
                        q.UserRoles.FirstOrDefault().Role.name,
                        q.isActive
                    }
                )
                .ToList();
            gvUserList.DataSource = users;
            gvUserList.Columns["username"].HeaderText = "username";
            gvUserList.Columns["name"].HeaderText = "Role Name";
            gvUserList.Columns["isActive"].HeaderText = "Active";
            //Hide the column for ID. Changed from the hard coded column value to the name, 
            // to make it more dynamic. 
            gvUserList.Columns["id"].Visible = false;
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
            PopulateGrid();
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }
    }
}
