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
    public partial class StaffView : Form
    {
        Form parent;
        public StaffView(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void StaffView_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddProcedure ob = new AddProcedure();
            panel2.Controls.Clear();
            ob.TopLevel = false;
            ob.AutoScroll = true;
            panel2.Controls.Add(ob);
            ob.Show();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            ViewBill ob = new ViewBill();
            panel2.Controls.Clear();
            ob.TopLevel = false;
            ob.AutoScroll = true;
            panel2.Controls.Add(ob);
            ob.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewRoom ob = new ViewRoom();
            panel2.Controls.Clear();
            ob.TopLevel = false;
            ob.AutoScroll = true;
            panel2.Controls.Add(ob);
            ob.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            BookRooms ob = new BookRooms();
            ob.TopLevel = false;
            ob.AutoScroll = true;
            panel2.Controls.Add(ob);
            ob.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            ViewAppointments ob = new ViewAppointments();
            ob.TopLevel = false;
            ob.AutoScroll = true;
            panel2.Controls.Add(ob);
            ob.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            BookAppointmentDialog ob = new BookAppointmentDialog();
            ob.TopLevel = false;
            ob.AutoScroll = true;
            panel2.Controls.Add(ob);
            ob.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }
    }
}
