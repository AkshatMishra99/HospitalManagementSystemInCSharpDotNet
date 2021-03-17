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
    public partial class AddStaff : Form
    {

        SqlConnection conn;
        public AddStaff()
        {
            InitializeComponent();
        }

        private void AddStaff_Load(object sender, EventArgs e)
        {
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            conn = new SqlConnection("Server=.\\SQLEXPRESS;database=Hospital;Integrated Security=true");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label6.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                label6.Visible = true;
            }
            if (textBox4.TextLength == 0)
            {
                label7.Visible = true;
            }
            if (textBox2.TextLength == 0)
            {
                label8.Text = "Password cannot be left blank";
                label8.Visible = true;
            }
            if (textBox2.TextLength > 0 && textBox2.TextLength < 6)
            {
                label8.Text = "Pasword should have at least 6 characters";
                label8.Visible = true;
            }
            if (label6.Visible == true || label7.Visible == true || label8.Visible == true)
                return;
            else
            {
                conn.Open();
                SqlCommand readLastStaffId = new SqlCommand("select id from Users order by id desc", conn);
                SqlDataReader reader;
                int staffId = 8001;
                try
                {
                    reader = readLastStaffId.ExecuteReader();
                    if (reader.Read())
                    {
                        staffId = reader.GetInt32(0) + 1;
                    }
                    reader.Close();
                    string query = string.Format("insert into Users values({0},'{1}','{2}','{3}','{4}');", staffId, textBox4.Text, textBox2.Text, textBox3.Text, textBox1.Text);
                    SqlCommand enterPatientIntoDB = new SqlCommand(query, conn);
                    enterPatientIntoDB.ExecuteNonQuery();
                    enterPatientIntoDB.Dispose();
                    conn.Close();
                    MessageBox.Show("Staff Registration Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception E)
                {
                    MessageBox.Show("Username already exists please select a different Username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label7.Visible = false;
        }
    }
}
