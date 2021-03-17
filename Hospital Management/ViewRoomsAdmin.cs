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
    public partial class ViewRoomsAdmin : Form
    {
        private Panel panel;
        ConnectionDB conn = new ConnectionDB();
        private static ArrayList Listroomno = new ArrayList();
        private static ArrayList Listroomtype = new ArrayList();
        private static ArrayList Listavailable = new ArrayList();
        private static ArrayList ListOccupiedBy = new ArrayList();
        private bool roomFound = false;
        public ViewRoomsAdmin(Panel panel)
        {
            InitializeComponent();
            this.panel = panel;
        }

        private void ViewRoomsAdmin_Load(object sender, EventArgs e)
        {
            
            GetData();
            if (Listroomno.Count > 0)
            {
                updateDatagrid();
            }
        }
        private void GetData()
        {
            dataGridView1.Rows.Clear();
            Listroomno.Clear();
            Listroomtype.Clear();
            Listavailable.Clear();
            ListOccupiedBy.Clear();
            try
            {
                conn.Open();
                string query = "select roomno,roomtype,unavailable,patient.name from room left join patient on room.occby=patient.ssn;";
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        Listroomno.Add(row["roomno"].ToString());
                        Listroomtype.Add(row["roomtype"].ToString());
                        Listavailable.Add(row["unavailable"].ToString()=="True"?"No":"Yes");
                        string occby = row["name"].ToString();
                        ListOccupiedBy.Add((occby == "NULL" ? "None" : occby));
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
            for (int i = 0; i < Listroomno.Count; i++)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dataGridView1);
                newRow.Cells[0].Value = Listroomno[i];
                newRow.Cells[1].Value = Listroomtype[i];
                newRow.Cells[2].Value = Listavailable[i];
                newRow.Cells[3].Value = ListOccupiedBy[i];
                dataGridView1.Rows.Add(newRow);
            }
        }
        bool IsRoomOccupied(string RoomNo) 
        {
            string query = String.Format("select unavailable from room where roomno={0}", RoomNo);
            bool unavailable = false;
            SqlDataReader Read=conn.ExecuteReader(query);
            if (Read.Read())
            {
                if (Read.GetBoolean(0)) unavailable = true;
            }
            Read.Close();
            return unavailable;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if (!roomFound)
                {
                    throw new MyException("First Search Room Number!!");
                }
                string roomNO = textBox1.Text;
                if (IsRoomOccupied(roomNO))
                    throw new Exception("Can not delete room as it is occupied!!!");
                string deleteQuery = String.Format("delete from room where roomno={0};", roomNO);
                conn.ExecuteNonQuery(deleteQuery);
                MessageBox.Show("Record Deleted of room no=" + roomNO+" !!!");
                roomFound = false;
                GetData();
                if (Listroomno.Count > 0)
                {
                    updateDatagrid();
                }
                conn.Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }          
        }
        //function to search room by room no
        private void GetData2()
        {
            dataGridView1.Rows.Clear();
            Listroomno.Clear();
            Listroomtype.Clear();
            Listavailable.Clear();
            ListOccupiedBy.Clear();
            try
            {
                string roomNO = textBox1.Text;
                if (roomNO.Length == 0)
                {
                    throw new MyException("Enter Room Number First!!!");
                }
                Convert.ToInt32(roomNO);
                conn.Open();
                 string query = "select roomno,roomtype,unavailable,patient.name from room left join patient on room.occby=patient.ssn where roomno="+roomNO+";";
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        Listroomno.Add(row["roomno"].ToString());
                        Listroomtype.Add(row["roomtype"].ToString());
                        Listavailable.Add(row["unavailable"].ToString() == "True" ? "No" : "Yes");
                        string occby = row["name"].ToString();
                        ListOccupiedBy.Add((occby == "NULL" ? "None" : occby));
                    }
                    roomFound = true;
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

        private void button3_Click(object sender, EventArgs e)
        {
            GetData2();
            if (Listroomno.Count > 0)
            {
                updateDatagrid();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel.Controls.Clear();
            UpdateRoomAdmin obj=new UpdateRoomAdmin(panel);
            obj.TopLevel = false;
            obj.AutoScroll = true;
        }
    }
}
