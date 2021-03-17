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
    public partial class UpdateRoomAdmin : Form
    {
        Panel panel;
        public UpdateRoomAdmin(Panel panel)
        {
            InitializeComponent();
            this.panel = panel;

        }
        ConnectionDB conn = new ConnectionDB();

        private void UpdateRoomAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "select roomtype from room_cost;";
                SqlDataReader reader = conn.ExecuteReader(query);
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0));
                }
                reader.Close();   
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex == -1) throw new MyException("First Select Room Type!!!");
                if (textBox1.Text.Length == 0) throw new MyException("First Enter Room Cost!!!");
                
                conn.Open();
                double roomCost = Convert.ToDouble(textBox1.Text);
                string roomtype = comboBox1.SelectedItem.ToString();
                string query = String.Format("update room_cost set cost={0} where roomtype='{1}';", roomCost, roomtype);
                conn.ExecuteNonQuery(query);
                MessageBox.Show("Successfully Updated Cost!!!", "Successful!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel.Controls.Clear();
            }
            catch(FormatException exc)
            {
                MessageBox.Show("Enter Valid Room Cost!!", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
