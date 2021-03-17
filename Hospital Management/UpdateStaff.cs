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
    public partial class UpdateStaff : Form
    {

        ConnectionDB conn = new ConnectionDB();
        public UpdateStaff()
        {
            InitializeComponent();
        }

        private void UpdateStaff_Load(object sender, EventArgs e)
        {
            label3.Visible = label7.Visible = label8.Visible = label9.Visible = false;
            textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = false;
            button2.Enabled = button3.Enabled = false;
        }
        private void fetchData(string query)
        {
            try
            {
                conn.Open();
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = true;
                        button2.Enabled = button3.Enabled = true;
                        textBox1.Enabled = false;
                        button1.Enabled = false;
                        textBox2.Text = row["name"].ToString();
                        textBox3.Text = row["username"].ToString();
                        textBox4.Text = row["password"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username entered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                label3.Visible = true;
                return;
            }
            else
            {
                fetchData(string.Format("select * from Users where username=('{0}');", textBox1.Text));
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label3.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label7.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label9.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.TextLength == 0)
            {
                label7.Visible = true;
            }
            if (textBox3.TextLength == 0)
            {
                label8.Visible = true;
            }
            if (textBox4.TextLength == 0)
            {
                label9.Text = "Password cannot be left blank";
                label9.Visible = true;
            }
            if (textBox4.TextLength > 0 && textBox4.TextLength < 6)
            {
                label9.Text = "Pasword should have at least 6 characters";
                label9.Visible = true;
            }
            if (label7.Visible == true || label8.Visible == true || label9.Visible == true)
            {
                return;
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = string.Format("update Users set name=('{0}') , username=('{1}') , password=('{2}') where username=('{3}');", textBox2.Text, textBox3.Text, textBox4.Text, textBox1.Text);
                    int rtvalue = conn.ExecuteNonQuery(query);
                    if (rtvalue == -1)
                    {
                        throw new Exception();
                    }
                    MessageBox.Show("Record Updation Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
                    button1.Enabled = true;
                    textBox1.Enabled = true;
                    button2.Enabled = button3.Enabled = false;
                    textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = false;
                }
                catch (Exception err)
                {
                    MessageBox.Show("Username already exists please select a different Username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            button1.Enabled = true;
            textBox1.Enabled = true;
            button2.Enabled = button3.Enabled = false;
            textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = false;
        }
    }
}
