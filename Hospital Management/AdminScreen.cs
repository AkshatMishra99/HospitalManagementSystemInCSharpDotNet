using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_Management
{
    public partial class AdminScreen : Form
    {
        Form parent;
        public AdminScreen(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ViewPatients viewPatients = new ViewPatients();
            viewPatients.TopLevel = false;
            viewPatients.AutoScroll = true;
            panel2.Controls.Add(viewPatients);
            viewPatients.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ViewPhysicians viewPhysicians = new ViewPhysicians();
            viewPhysicians.TopLevel = false;
            viewPhysicians.AutoScroll = true;
            panel2.Controls.Add(viewPhysicians);
            viewPhysicians.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                panel2.Controls.Clear();
                ViewAppointmentsAdmin viewAppointmentsAdmin = new ViewAppointmentsAdmin();
                viewAppointmentsAdmin.TopLevel = false;
                viewAppointmentsAdmin.AutoScroll = true;
                panel2.Controls.Add(viewAppointmentsAdmin);
                viewAppointmentsAdmin.Show();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ViewNurses viewNurses = new ViewNurses();
            viewNurses.TopLevel = false;
            viewNurses.AutoScroll = true;
            panel2.Controls.Add(viewNurses);
            viewNurses.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ViewRoomsAdmin viewrooms = new ViewRoomsAdmin(panel2);
            viewrooms.TopLevel = false;
            viewrooms.AutoScroll = true;
            panel2.Controls.Add(viewrooms);
            viewrooms.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ViewUsers viewUsers = new ViewUsers();
            viewUsers.TopLevel = false;
            viewUsers.AutoScroll = true;
            panel2.Controls.Add(viewUsers);
            viewUsers.Show();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            AddRecord addRecord = new AddRecord(panel2);
            addRecord.TopLevel = false;
            addRecord.AutoScroll = true;
            panel2.Controls.Add(addRecord);
            addRecord.Show();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            BackupRestore addRecord = new BackupRestore();
            addRecord.TopLevel = false;
            addRecord.AutoScroll = true;
            panel2.Controls.Add(addRecord);
            addRecord.Show();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            UpdateRecord updateRecord = new UpdateRecord(panel2);
            updateRecord.TopLevel = false;
            updateRecord.AutoScroll = true;
            panel2.Controls.Add(updateRecord);
            updateRecord.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
