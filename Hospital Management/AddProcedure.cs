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
    public partial class AddProcedure : Form
    {
        public AddProcedure()
        {
            InitializeComponent();
        }
        ConnectionDB conn = new ConnectionDB();
        private static ArrayList ListProcCode = new ArrayList();
        private static ArrayList ListProcName = new ArrayList();
        private static ArrayList ListProcCost = new ArrayList();
        
        private void AddProcedure_Load(object sender, EventArgs e)
        {
            getdata();
        }
        private void getdata()
        {
            try
            {
                conn.Open();
                string query = "select * from procedures";
                SqlDataReader row = conn.ExecuteReader(query);
               
                if (row.HasRows)
                {
                    while (row.Read())
                    {
                        ListProcCode.Add(row["code"].ToString());
                        ListProcName.Add(row["name"].ToString());
                        ListProcCost.Add(row["cost"].ToString());
                    }
                }
                row.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            updateComboBoxes();
        }
        private void updateComboBoxes()
        {
            try
            {
                comboBox1.Items.Clear();
                for (int i = 0; i < ListProcCode.Count; i++)
                {
                    comboBox1.Items.Add(ListProcName[i].ToString());
                }
                comboBox1.SelectedIndex = 0;
            }catch(Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int ind = ListProcName.IndexOf(comboBox1.SelectedItem.ToString());
                if (ind == -1) throw new MyException("Invalid Request!! Please Try Again!!");
                textBox2.Text = ListProcCost[ind].ToString();
            }
            catch(Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }
        private int getStayID(int patientID)
        {
            ArrayList IDInfo = new ArrayList();
            int stayID=-1;
            string query = String.Format("select stayid from stay where patient={0}", patientID);
            try
            {
                conn.Open();
                SqlDataReader read = conn.ExecuteReader(query);
                if (read.Read())
                {
                    stayID = read.GetInt32(0);
                }
                read.Close();
            }catch(Exception exc)
            {
                Console.WriteLine(exc);
            }
            finally
            {
                
            }
            return stayID;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0) throw new MyException("First Enter Patient ID!!");
                else if (textBox2.Text.Length == 0) throw new MyException("Select A Procedure First!!");

                int appointmentID=Convert.ToInt32(textBox1.Text);
                string query1 = String.Format("select appointmentid,patient,physician from appointment where appointmentid={0};", appointmentID);
                conn.Open();
                SqlDataReader row = conn.ExecuteReader(query1);
                int patient, physician,procCode,stayID;
                if (row.Read())
                {
                    patient = Convert.ToInt32(row["patient"]);
                    physician = Convert.ToInt32(row["physician"]);                   
                }
                else       
                throw new MyException("Enter Valid Patient ID!! Patient ID not found!!");
                row.Close();
                //finding procedure code and stay id from the table
                procCode = Convert.ToInt32(ListProcCode[comboBox1.SelectedIndex]);
                stayID = getStayID(patient);
                //calculating current time and date of procedure and finding days, month and year
                DateTime Date = DateTime.Now;
                /*string month = Date.Month < 10 ? "0" + Date.Month : Date.Month+"";
                string day = Date.Day < 10 ? "0" + Date.Day : Date.Day + "";
                string date = String.Format("{0}/{1}/{2}", Date.Year, month, day);*/
                string date = Date.ToString("yyyy-MM-dd");
                //writing query 
                string query2 = String.Format("insert into undergoes(patient,procede,stay,date,physician,paid) values({0},{1},{2},'{3}',{4},'{5}');",patient,procCode,stayID,date,physician,false);
                conn.ExecuteNonQuery(query2);
                MessageBox.Show("Successfully Added Procedure!!", "Successful!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch(FormatException exc)
            {
                MessageBox.Show("Enter Valid Patient ID!!","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
