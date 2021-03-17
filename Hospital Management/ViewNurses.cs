using System;
using System.Collections;
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
    public partial class ViewNurses : Form
    {

        ConnectionDB conn = new ConnectionDB();
        private static ArrayList ListID = new ArrayList();
        private static ArrayList Listname = new ArrayList();
        private static ArrayList Listposition = new ArrayList();
        private static ArrayList Listregistered = new ArrayList();
        public ViewNurses()
        {
            InitializeComponent();
        }

        private void ViewNurses_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            ListID.Clear();
            Listname.Clear();
            Listposition.Clear();
            Listregistered.Clear();
            GetData();
            if (ListID.Count > 0)
            {
                updateDatagrid();
            }
        }
        private void GetData()
        {
            dataGridView1.Rows.Clear();
            ListID.Clear();
            Listname.Clear();
            Listposition.Clear();
            Listregistered.Clear();
            try
            {
                conn.Open();
                string query = "select * from nurse";
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListID.Add(row["empid"].ToString());
                        Listname.Add(row["name"].ToString());
                        Listposition.Add(row["position"].ToString());
                        Listregistered.Add(row["registered"].ToString() == "1" ? true.ToString() : false.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Data not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        private void updateDatagrid()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < ListID.Count; i++)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dataGridView1);
                newRow.Cells[0].Value = ListID[i];
                newRow.Cells[1].Value = Listname[i];
                newRow.Cells[2].Value = Listposition[i];
                newRow.Cells[3].Value = Listregistered[i];
                dataGridView1.Rows.Add(newRow);
            }
        }
        private void GetData2()
        {
            dataGridView1.Rows.Clear();
            ListID.Clear();
            Listname.Clear();
            Listposition.Clear();
            Listregistered.Clear();
            try
            {
                conn.Open();
                string query = "select * from nurse where empid="+textBox1.Text+";";
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListID.Add(row["empid"].ToString());
                        Listname.Add(row["name"].ToString());
                        Listposition.Add(row["position"].ToString());
                        Listregistered.Add(row["registered"].ToString() == "1" ? true.ToString() : false.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Data not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            ListID.Clear();
            Listname.Clear();
            Listposition.Clear();
            Listregistered.Clear();
            GetData2();
            if (ListID.Count > 0)
            {
                updateDatagrid();
            }
        }
    }
}
