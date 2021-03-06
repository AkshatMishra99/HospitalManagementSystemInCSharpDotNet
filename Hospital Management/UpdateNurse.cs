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
    public partial class UpdateNurse : Form
    {

        ConnectionDB conn = new ConnectionDB();
        public UpdateNurse()
        {
            InitializeComponent();
        }

        private void UpdateNurse_Load(object sender, EventArgs e)
        {
            label3.Visible = label7.Visible = label8.Visible = label9.Visible = false;
            textBox2.Enabled = textBox3.Enabled = comboBox1.Enabled = false;
            button2.Enabled = button3.Enabled = false;
            comboBox1.SelectedIndex = 0;
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
                        textBox2.Enabled = textBox3.Enabled = comboBox1.Enabled = true;
                        button2.Enabled = button3.Enabled = true;
                        textBox1.Enabled = false;
                        button1.Enabled = false;
                        textBox2.Text = row["name"].ToString();
                        textBox3.Text = row["position"].ToString();
                        comboBox1.Text = row["registered"].ToString() == "True" ? "Permanent" : "Contract";
                    }
                }
                else
                {
                    MessageBox.Show("Invalid employee ID entered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                fetchData(string.Format("select * from nurse where empid=({0});", textBox1.Text));
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
            if (comboBox1.SelectedIndex == 0)
            {
                label9.Visible = true;
            }
            if (label7.Visible == true || label8.Visible == true || label9.Visible == true)
            {
                return;
            }
            else
            {
                conn.Open();
                int registered = 0;
                if (comboBox1.SelectedIndex == 1)
                {
                    registered = 1;
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    registered = 0;
                }
                string query = string.Format("update nurse set name=('{0}') , position=('{1}') , registered=({2}) where empid=({3});", textBox2.Text, textBox3.Text, registered, textBox1.Text);
                conn.ExecuteReader(query);
                conn.Close();
                MessageBox.Show("Record Updation Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = textBox2.Text = textBox3.Text = "";
                comboBox1.SelectedIndex = 0;
                button1.Enabled = true;
                textBox1.Enabled = true;
                button2.Enabled = button3.Enabled = false;
                textBox2.Enabled = textBox3.Enabled = false;
                comboBox1.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "";
            button1.Enabled = true;
            comboBox1.SelectedIndex = 0;
            textBox1.Enabled = true;
            button2.Enabled = button3.Enabled = false;
            textBox2.Enabled = textBox3.Enabled = false;
            comboBox1.Enabled = false;
        }
    }
}
