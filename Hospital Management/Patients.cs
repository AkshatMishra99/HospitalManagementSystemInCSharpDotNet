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
    
    public partial class Patients : Form
    {
        ConnectionDB conn = new ConnectionDB();
        private static ArrayList ListID = new ArrayList();
        private static ArrayList ListFirstname = new ArrayList();
        private static ArrayList ListLastname = new ArrayList();
        private static ArrayList ListTelephone = new ArrayList(); 
        public Patients()
        {
            InitializeComponent();
        }

        private void Patients_Load(object sender, EventArgs e)
        {
            GetData();
            if (ListID.Count > 0)
            {
                updateDatagrid();
            }
            else
            {
                MessageBox.Show("Data not found");
            }
        }
        private void GetData()
        {
            try
            {
                conn.Open();
                string query = "select * from patient";

                //SqlDataReader row;  
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListID.Add(row["ssn"].ToString());
                        ListFirstname.Add(row["name"].ToString());
                        ListLastname.Add(row["address"].ToString());
                        ListTelephone.Add(row["phone"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Data not found");
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
                newRow.Cells[1].Value = ListFirstname[i];
                newRow.Cells[2].Value = ListLastname[i];
                newRow.Cells[3].Value = ListTelephone[i];
                dataGridView1.Rows.Add(newRow);
            }
        }
    }
} 
