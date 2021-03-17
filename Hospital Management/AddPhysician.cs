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
    public partial class AddPhysician : Form
    {
        SqlConnection conn;
        public AddPhysician()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.TextLength == 0)
            {
                label6.Visible = true;
            }
            if (textBox3.TextLength == 0)
            {
                label7.Visible = true;
            }
            if (textBox4.TextLength == 0)
            {
                label8.Text = "Field cannot be left blank";
                label8.Visible = true;
            }
            if (textBox4.TextLength > 0 && textBox4.TextLength < 3)
            {
                label8.Text = "Enter a valid 3 digit ID";
                label8.Visible = true;
            }
            if (label6.Visible == true || label7.Visible == true || label8.Visible == true)
                return;
            else
            {
                conn.Open();
                SqlCommand readLastPhysicianId = new SqlCommand("select employeeid , ssn from physician order by employeeid desc", conn);
                SqlDataReader reader;
                int physicianId = 7001;
                int ssn = 2001;
                try
                {
                    reader = readLastPhysicianId.ExecuteReader();
                    if (reader.Read())
                    {
                        physicianId = reader.GetInt32(0) + 1;
                        ssn = reader.GetInt32(1) + 1;
                    }
                    reader.Close();
                    textBox1.Text = physicianId.ToString();
                    string query = "insert into physician values(" + physicianId + ",'" + textBox2.Text + "','" + textBox3.Text + "','" + ssn + "','" + textBox4.Text + "');";
                    SqlCommand enterPatientIntoDB = new SqlCommand(query, conn);
                    enterPatientIntoDB.ExecuteNonQuery();
                    enterPatientIntoDB.Dispose();
                    conn.Close();
                    MessageBox.Show("Physician Registration Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void AddPhysician_Load(object sender, EventArgs e)
        {
            textBox4.MaxLength = 3;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            conn = new SqlConnection("Server=.\\SQLEXPRESS;database=Hospital;Integrated Security=true");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label6.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label7.Visible = false;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label8.Visible = false;
        }
    }
}
