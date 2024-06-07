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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addRentalRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addRentalRecord = new AddEditRentalRecord
            {
                MdiParent = this
            };
            addRentalRecord.Show();
        }

        private void editVehicleListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var vehicleListing = new ManageVehicleListing
            {
                MdiParent = this
            };
            vehicleListing.Show();
        }

        private void viewArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageRentalRecords manageRentalRecords = new ManageRentalRecords
            {
                MdiParent = this
            };
            manageRentalRecords.Show();
        }
    }
}
