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
    public partial class AddNurse : Form
    {

        SqlConnection conn;
        public AddNurse()
        {
            InitializeComponent();
        }

        private void AddNurse_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label8.Visible = false;
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
            if (comboBox1.SelectedIndex == 0)
            {
                label8.Visible = true;
            }
            if (label6.Visible == true || label7.Visible == true || label8.Visible == true)
                return;
            else
            {
                conn.Open();
                SqlCommand readLastNurseId = new SqlCommand("select empid , ssn from nurse order by empid desc", conn);
                SqlDataReader reader;
                int nurseId = 5001;
                int ssn = 3001;
                int registered = 0;
                try
                {
                    reader = readLastNurseId.ExecuteReader();
                    if (reader.Read())
                    {
                        nurseId = reader.GetInt32(0) + 1;
                        ssn = reader.GetInt32(1) + 1;
                    }
                    reader.Close();
                    textBox1.Text = nurseId.ToString();
                    if (comboBox1.SelectedIndex == 1)
                    {
                        registered = 1;
                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        registered = 0;
                    }
                    string query = string.Format("insert into nurse values({0},'{1}','{2}',{3},{4});", nurseId, textBox2.Text, textBox3.Text, registered, ssn); ;
                    SqlCommand enterPatientIntoDB = new SqlCommand(query, conn);
                    enterPatientIntoDB.ExecuteNonQuery();
                    enterPatientIntoDB.Dispose();
                    conn.Close();
                    MessageBox.Show("Nurse Registration Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
