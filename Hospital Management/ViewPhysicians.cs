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
    public partial class ViewPhysicians : Form
    {

        ConnectionDB conn = new ConnectionDB();
        private static ArrayList ListID = new ArrayList();
        private static ArrayList Listname = new ArrayList();
        private static ArrayList Listposition = new ArrayList();
        private static ArrayList Listdeptid = new ArrayList();
        public ViewPhysicians()
        {
            InitializeComponent();
        }

        private void ViewPhysicians_Load(object sender, EventArgs e)
        {
            
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
            Listdeptid.Clear();
            try
            {
                conn.Open();
                string query = "select * from physician";
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListID.Add(row["employeeid"].ToString());
                        Listname.Add(row["name"].ToString());
                        Listposition.Add(row["position"].ToString());
                        Listdeptid.Add(row["departmentid"].ToString());
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
                newRow.Cells[3].Value = Listdeptid[i];
                dataGridView1.Rows.Add(newRow);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if (textBox1.Text.Length == 0)
                {
                    throw new MyException("Enter Physician ID first!!!");
                }
                int patientID = Convert.ToInt32(textBox1.Text);
                int ind = ListID.IndexOf(textBox1.Text);
                if (ind == -1)
                {
                    throw new MyException("No Record Found!!");
                }
                string query = "delete from physician where employeeid=" + patientID + ";";
                conn.ExecuteNonQuery(query);
                MessageBox.Show("Record Successfully deleted!!", "Deletion Successful!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetData();
                if (ListID.Count > 0)
                {
                    updateDatagrid();
                }

            }
            catch (FormatException exc)
            {
                MessageBox.Show("Enter Valid Physician ID!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
