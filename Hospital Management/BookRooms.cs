using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Hospital_Management
{
    public partial class BookRooms : Form
    {
        public BookRooms()
        {
            InitializeComponent();
        }
        ConnectionDB conn = new ConnectionDB();
        private static ArrayList ListRoomNO = new ArrayList();
        private static ArrayList ListRoomType = new ArrayList();
    
        private int SelectedRoomNo;
        int empPhyId;
        private void GetData()
        {
            comboBox1.Items.Clear();
            ListRoomNO.Clear();
            ListRoomType.Clear();
            try
            {
                empPhyId = -1;                
                conn.Open();                
                string query = "select roomno,roomtype from room where unavailable=0;";
                //SqlDataReader row;  
                SqlDataReader row;
                row = conn.ExecuteReader(query);
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListRoomNO.Add(row["roomno"].ToString());
                        ListRoomType.Add(row["roomtype"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No Rooms Available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
               

            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }                  
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            conn.Open();
            bool foundPatient = false;
            bool foundApp = false;
            try
                {
                if (textBox1.Text.Length == 0)
                {
                    throw new MyException("Enter Appointment ID First!!");
                }
                empPhyId = Convert.ToInt32(textBox1.Text);
                foundPatient = true;
                string query2 = "select patient from appointment where appointmentid=" + empPhyId + ";";
                SqlDataReader readId = conn.ExecuteReader(query2);
                if (readId.Read())
                {
                    foundPatient = true;
                    empPhyId = readId.GetInt32(0);
                }
                else
                {
                    foundPatient = false;
                }
                readId.Close();
                string query3;
                int stayID = getStayID();
                string fromDate,toDate;
                string fromMonth, fromDay, toMonth, toDay;
                fromMonth = (dateTimePicker1.Value.Month < 9) ? "0" + dateTimePicker1.Value.Month : dateTimePicker1.Value.Month + "";
                fromDay = (dateTimePicker1.Value.Day < 9) ? "0" + dateTimePicker1.Value.Day : dateTimePicker1.Value.Day + "";

                toMonth = (dateTimePicker1.Value.Month < 9) ? "0" + dateTimePicker1.Value.Month : dateTimePicker1.Value.Month + "";
                toDay = (dateTimePicker2.Value.Day < 9) ? "0" + dateTimePicker2.Value.Day : dateTimePicker2.Value.Day + "";
                fromDate = dateTimePicker1.Value.Year + "/" +fromMonth+ "/" + fromDay;
                toDate= dateTimePicker2.Value.Year + "/" + toMonth + "/" + toDay;
                if (foundPatient)
                {
                    conn.Open();
                    
                    query3 = String.Format("insert into stay values({0},{1},{2},'{3}','{4}'); ", stayID, empPhyId, SelectedRoomNo, fromDate, toDate);
                    Console.WriteLine(query3);
                    conn.ExecuteNonQuery(query3);
                    conn.ExecuteNonQuery("update room set unavailable=1,occby=" + empPhyId + " where roomno=" + SelectedRoomNo + ";");
                    MessageBox.Show("Room Booked Successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetData();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Enter Valid Patient ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
        private int getStayID()
        {
            try
            {
                conn.Open();
                string query = "select max(stayid) as count from stay;";
                SqlDataReader read = conn.ExecuteReader(query);
                if (read.Read())
                {
                    if (!read.IsDBNull(0))
                        return read.GetInt32(0) + 1;
                    else return 0;

                } else return 0;
                read.Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                
                conn.Close();
            }
            return 0;
        }
        private void BookRooms_Load(object sender, EventArgs e)
        {
            GetData();
            if (ListRoomNO.Count > 0)
            {
                for(int i = 0; i < ListRoomNO.Count; i++)
                {
                    comboBox1.Items.Add(ListRoomNO[i]);
                    //comboBox2.Items.Add(ListRoomType[i]);
                }
                comboBox1.SelectedIndex = 0;
                dateTimePicker1.MinDate = dateTimePicker2.MinDate = DateTime.Now;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedRoomNo = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            string roomtype= ListRoomType[comboBox1.SelectedIndex].ToString();
            conn.Open();
            int roomcost = 0 ;
            try
            {
                string query = "select cost from room_cost where roomtype='" + roomtype + "';";
                SqlDataReader read = conn.ExecuteReader(query);
                if (read.Read())
                {
                    roomcost = read.GetInt32(0);
                }
                else
                {
                    throw new MyException("Invalid Room Type");
                }
            }catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            textBox2.Text = roomtype + "=====" + roomcost;           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
        }
    }
    class MyException : Exception
    {
        public MyException() { }
        public MyException(string message) : base(message)
        {

        }
    }
}
