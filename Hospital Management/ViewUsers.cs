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
    public partial class ViewUsers : Form
    {

        ConnectionDB conn = new ConnectionDB();
        private static ArrayList Listusername = new ArrayList();
        private static ArrayList Listpassword = new ArrayList();
        private static ArrayList Listtype = new ArrayList();
        public ViewUsers()
        {
            InitializeComponent();
        }

        private void ViewUsers_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Listusername.Clear();
            Listpassword.Clear();
            Listtype.Clear();
            GetData();
            if (Listusername.Count > 0)
            {
                updateDatagrid();
            }
        }
        private void GetData()
        {
            try
            {
                conn.Open();
                string query = "select * from Users";
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        Listusername.Add(row["username"].ToString());
                        Listpassword.Add(row["password"].ToString());
                        Listtype.Add(row["type"].ToString());
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
            for (int i = 0; i < Listusername.Count; i++)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dataGridView1);
                newRow.Cells[0].Value = Listusername[i];
                newRow.Cells[1].Value = Listpassword[i];
                newRow.Cells[2].Value = Listtype[i];
                dataGridView1.Rows.Add(newRow);
            }
        }
    }
}
