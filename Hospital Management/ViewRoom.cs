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
    public partial class ViewRoom : Form
    {
        public ViewRoom()
        {
            InitializeComponent();
        }
        ConnectionDB conn = new ConnectionDB();
        private static ArrayList ListID = new ArrayList();
        private static ArrayList ListName = new ArrayList();
        private static ArrayList ListRoomNo = new ArrayList();
        private void button1_Click(object sender, EventArgs e)
        {
            GetData();
            try
            {
                if (ListID.Count != 0)
                {
                    updateDatagrid();
                }
                else throw new MyException("No Room Booked By This Patient!!");
            }catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void GetData()
        {
            dataGridView1.Rows.Clear();
            ListID.Clear();
            ListName.Clear();
            ListRoomNo.Clear();
            try
            {
                conn.Open();
                if (textBox1.Text.Length == 0)
                {
                    throw new MyException("Enter Patient ID or Name First!!");
                }
                string query;
                try
                {
                    int patientID = Convert.ToInt32(textBox1.Text);
                    query = "select room.roomno, patient.ssn, patient.name from room left join patient on room.occby=patient.ssn where occby="+textBox1.Text+"; ";
                }catch(FormatException exc)
                {
                    query = "select room.roomno, patient.ssn, patient.name from room left join patient on room.occby=patient.ssn where name='" + textBox1.Text + "'; ";
                }              
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListID.Add(row["ssn"].ToString());
                        ListName.Add(row["name"].ToString());
                        ListRoomNo.Add(row["roomno"].ToString());                    
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
                MessageBox.Show(err.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                newRow.Cells[1].Value = ListName[i];
                newRow.Cells[2].Value = ListRoomNo[i];
                //newRow.Cells[3].Value = Listregistered[i];
                dataGridView1.Rows.Add(newRow);
            }
        }

        private void ViewRoom_Load(object sender, EventArgs e)
        {

        }
    }
}
