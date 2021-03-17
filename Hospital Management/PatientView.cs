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
    public partial class PatientView : Form
    {
        Form parent;
        public PatientView(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void PatientView_Load(object sender, EventArgs e)
        {

        }

        private void PatientView_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Close();
        }
    }
}
