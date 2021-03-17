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
    public partial class ViewAppointmentsAdmin : Form
    {

        ConnectionDB conn = new ConnectionDB();
        private static ArrayList ListAppointmentID = new ArrayList();
        private static ArrayList ListPatientID = new ArrayList();
        private static ArrayList ListNurseID = new ArrayList();
        private static ArrayList ListPhysicianID = new ArrayList();
        private static ArrayList ListStartDate = new ArrayList();
        private static ArrayList ListEndDate = new ArrayList();
        public ViewAppointmentsAdmin()
        {
            InitializeComponent();
        }

        private void ViewAppointmentsAdmin_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            ListAppointmentID.Clear();
            ListPatientID.Clear();
            ListNurseID.Clear();
            ListPhysicianID.Clear();
            ListStartDate.Clear();
            ListEndDate.Clear();
            GetData();
            if (ListAppointmentID.Count > 0 && ListPatientID.Count > 0)
            {
                updateDatagrid();
            }
        }
        private void GetData()
        {
            try
            {
                conn.Open();
                string query = "select * from appointment";
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListAppointmentID.Add(row["appointmentid"].ToString());
                        ListPatientID.Add(row["patient"].ToString());
                        ListNurseID.Add(row["prepnurse"].ToString());
                        ListPhysicianID.Add(row["physician"].ToString());
                        ListStartDate.Add(row["start_dt_time"].ToString());
                        ListEndDate.Add(row["end_dt_time"].ToString());
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
            for (int i = 0; i < ListAppointmentID.Count; i++)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dataGridView1);
                newRow.Cells[0].Value = ListAppointmentID[i];
                newRow.Cells[1].Value = ListPatientID[i];
                newRow.Cells[2].Value = ListNurseID[i];
                newRow.Cells[3].Value = ListPhysicianID[i];
                newRow.Cells[4].Value = ListStartDate[i];
                newRow.Cells[5].Value = ListEndDate[i];
                dataGridView1.Rows.Add(newRow);
            }
        }
        private void GetData3()
        {
            try
            {
                string apptDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                if (apptDate.Length == 0)
                {
                    throw new MyException("Enter Appointment Date First!!");
                }
                conn.Open();
                string query = String.Format("select * from appointment where start_dt_time='{0}';",apptDate);
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListAppointmentID.Add(row["appointmentid"].ToString());
                        ListPatientID.Add(row["patient"].ToString());
                        ListNurseID.Add(row["prepnurse"].ToString());
                        ListPhysicianID.Add(row["physician"].ToString());
                        ListStartDate.Add(row["start_dt_time"].ToString());
                        ListEndDate.Add(row["end_dt_time"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No Appointments Found on this Date!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            dataGridView1.Rows.Clear();
            ListAppointmentID.Clear();
            ListPatientID.Clear();
            ListNurseID.Clear();
            ListPhysicianID.Clear();
            ListStartDate.Clear();
            ListEndDate.Clear();
            GetData2();
            if (ListAppointmentID.Count > 0 && ListPatientID.Count > 0)
            {
                updateDatagrid();
            }
        }
        private void GetData2()
        {
            try
            {
                string appid = textBox1.Text;
                if (appid.Length == 0)
                {
                    throw new MyException("Enter Appointment ID First!!");
                }
                conn.Open();
                string query = String.Format("select * from appointment where appointmentid={0};", appid);
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListAppointmentID.Add(row["appointmentid"].ToString());
                        ListPatientID.Add(row["patient"].ToString());
                        ListNurseID.Add(row["prepnurse"].ToString());
                        ListPhysicianID.Add(row["physician"].ToString());
                        ListStartDate.Add(row["start_dt_time"].ToString());
                        ListEndDate.Add(row["end_dt_time"].ToString());
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
            ListAppointmentID.Clear();
            ListPatientID.Clear();
            ListNurseID.Clear();
            ListPhysicianID.Clear();
            ListStartDate.Clear();
            ListEndDate.Clear();
            GetData3();
            if (ListAppointmentID.Count > 0 && ListPatientID.Count > 0)
            {
                updateDatagrid();
            }
        }
    }
}
