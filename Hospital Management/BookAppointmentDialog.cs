using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_Management
{
    public partial class BookAppointmentDialog : Form
    {
        public BookAppointmentDialog()
        {
            InitializeComponent();
        }
        SqlConnection conn;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                int idPatient = -1;
                try
                {
                    idPatient = Convert.ToInt32(textBox1.Text);
                }
                catch
                {
                    idPatient = -1;
                }
                SqlCommand readPatientComm = new SqlCommand("select ssn,name from patient where name='" + textBox1.Text + "' or ssn='" + idPatient + "';", conn);
                SqlDataReader readerPatient = readPatientComm.ExecuteReader();
                if (readerPatient.Read())
                {
                    radioButton2.Checked = true;
                }
                else
                {
                    radioButton1.Checked = true;
                }
                readerPatient.Close();
                
            }catch(Exception E)
            {
                MessageBox.Show(E.Message,"Error Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            
            button2.Enabled = true;
        }

        private void BookAppointmentDialog_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Server =.\\SQLEXPRESS; database = Hospital; Integrated Security = true ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) 
            {
                RegisterPatient obj = new RegisterPatient();
                obj.Show();
               
            }
            else if(radioButton2.Checked)
            {
                BookAppointment obj = new BookAppointment(textBox1.Text);
                obj.Show();
            }
            else
            {
                MessageBox.Show("Please Check Name or Patient ID", "Enter Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Hide();
        }
    }
}
