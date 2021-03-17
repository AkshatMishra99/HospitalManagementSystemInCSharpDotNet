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
    public partial class RegisterPatient : Form
    {
        public RegisterPatient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if()
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || richTextBox1.Text.Length == 0)
            {
                if (textBox1.Text.Length == 0)
                {
                    MessageBox.Show("Enter Name of Patient!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(richTextBox1.Text.Length==0){
                    MessageBox.Show("Enter Address of Patient!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBox2.Text.Length == 0)
                {
                    MessageBox.Show("Enter Phone Number of Patient!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                conn.Open();
                SqlCommand readLastPatientId = new SqlCommand("select ssn from patient order by ssn desc", conn);
                SqlDataReader reader;
                int patientId = 1001;
                
                
                try
                {
                    reader = readLastPatientId.ExecuteReader();
                    if (reader.Read())
                    {
                        patientId = reader.GetInt32(0)+1;
                    }
                    reader.Close();
                    string query = "insert into patient values(" + patientId + ",'" + textBox1.Text + "','" + richTextBox1.Text + "','" + textBox2.Text + "','" + dateTimePicker1.Value.Year+"/"+dateTimePicker1.Value.Month+"/"+dateTimePicker1.Value.Day + "');";
                    SqlCommand enterPatientIntoDB = new SqlCommand(query,conn);
                    enterPatientIntoDB.ExecuteNonQuery();
                    enterPatientIntoDB.Dispose();
                    conn.Close();
                    MessageBox.Show("Patient Successfully Registered!!", "Successful!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new BookAppointment(textBox1.Text).Show();
                    Console.WriteLine(dateTimePicker1.Value.ToShortDateString());
                }
                catch(Exception E)
                {
                    MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        SqlConnection conn;
        private void RegisterPatient_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Server=.\\SQLEXPRESS;database=Hospital;Integrated Security=true");

        }
    }
}
