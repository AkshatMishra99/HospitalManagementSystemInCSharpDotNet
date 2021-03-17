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
    public partial class BookAppointment : Form
    {
        public BookAppointment()
        {
            InitializeComponent();
            PatientName = "";
        }
        string PatientName;
        public BookAppointment(string name)
        {
            InitializeComponent();
            PatientName = name;
        }
        SqlConnection conn;
        int totalAppointments = 0;
        bool validDate;
        private void BookAppointment_Load(object sender, EventArgs e)
        {
            conn= new SqlConnection("Server=.\\SQLEXPRESS;database=Hospital;Integrated Security=true ");
            conn.Open();            
            try
            {
                SqlCommand read = new SqlCommand("select name from department ", conn);
                SqlDataReader reader = read.ExecuteReader();
                while (reader.Read())
                {
                    string dept = reader.GetString(0);
                    comboBox1.Items.Add(dept);
                    Console.WriteLine("read:" + dept);
                }
                reader.Close();
                
                SqlCommand comm2 = new SqlCommand("select top 1 appointmentid from appointment order by appointmentid desc;", conn);
                SqlDataReader reader2 = comm2.ExecuteReader();
                if (reader2.Read())
                    totalAppointments = reader2.GetInt32(0);
               
                reader2.Close();
               
                validDate = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            dateTimePicker1.MinDate = DateTime.Now;
            textBox1.Text = PatientName;

        }
        private class Physician
        {
            internal int id,ssn;
            internal string name, position,department;
            internal Physician(int id,string name,string position,int ssn,string department)
            {
                this.id = id;
                this.ssn = ssn;
                this.name = name;
                this.department = department;
                this.position = position;
            }
        }
        List <Physician> phyList = new List<Physician>();
        Physician curr;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            listBox1.Items.Clear();
            SqlCommand cmd = new SqlCommand("select * from physician where departmentid=(select departmentid from department where name='"+comboBox1.SelectedItem+"');", conn);
            phyList.Clear();
            SqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int phId, depId, ssn;
                    string name, add;
                    phId = reader.GetInt32(0);
                    name = reader.GetString(1);
                    add = reader.GetString(2);
                    ssn = reader.GetInt32(3);
                    Console.WriteLine("{0} {1} {2} {3}", phId, name, add, ssn);
                    phyList.Add(new Physician(phId,name,add,ssn,comboBox1.SelectedItem.ToString()));
                    listBox1.Items.Add(name);
                }              
                conn.Close();
                reader.Close();
                listBox1.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error-Code:" + ex.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
                Console.WriteLine("Connection was closed");
            }
        }
        
        Dictionary<String, int> apptdateTimesCount = new Dictionary<String, int>();
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        void IsValidAppointmentDate()
        {
            string dateAppointment = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            Console.WriteLine("date time picker:" + dateAppointment);
            
            if (apptdateTimesCount.ContainsKey(dateAppointment) && apptdateTimesCount[dateAppointment] >= 10)
            {
                Console.WriteLine("daaddd" + apptdateTimesCount[dateAppointment]);

                MessageBox.Show("No Appointments available on this date!!", "Please Chose Another Date!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                validDate = true;
            }

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
            //MessageBox.Show(apptdateTimesCount[dateAppointment] + "");
        }
        class Appointment
        {
            internal int appointmentid, patient, prepnurse, physician;
            internal DateTime start_dt_time, end_dt_time;
            public Appointment(int appointmentid,int patient,int prepnurse,int physician,DateTime start_dt_time,DateTime end_dt_time)
            {
                this.appointmentid = appointmentid;
                this.patient = patient;
                this.prepnurse = prepnurse;
                this.physician = physician;
                this.end_dt_time = end_dt_time;
                this.start_dt_time = start_dt_time;
            }
        }
        void SetPhysicianID()
        {
            //conn.Open();
            try
            {
                curr = phyList[listBox1.SelectedIndex];

                SqlCommand comm = new SqlCommand("select end_dt_time from appointment where physician='" + curr.id + "';", conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {

                    DateTime temp = reader.GetDateTime(0);
                    string newDate = temp.ToString("yyyy-MM-dd");

                    Console.WriteLine(newDate);
                    if (apptdateTimesCount.ContainsKey(newDate))
                        apptdateTimesCount[newDate] += 1;
                    else apptdateTimesCount[newDate] = 1;
                }
                reader.Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("Error-Code:" + E.Message, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(E);
            }
            finally
            {
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (textBox1.Text.Length == 0 || comboBox1.SelectedIndex == -1 || listBox1.SelectedIndex == -1) 
            {
                if (textBox1.Text.Length == 0)
                {
                    MessageBox.Show("Enter Name!!", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    SqlCommand selectPatient = new SqlCommand("select ssn from patient where name='" + textBox1.Text + "';", conn);
                    SqlDataReader reader = selectPatient.ExecuteReader();

                    bool isPatient = false;
                    Appointment app;
                    int PatientId = -1, NurseId, PhysicianId;

                    if (reader.Read())
                    {
                        PatientId = reader.GetInt32(0);
                        isPatient = true;
                        reader.Close();
                    }
                    reader.Close();
                    SqlCommand selectNurse = new SqlCommand("select empid from nurse where registered=0;", conn);
                    SqlDataReader reader1 = selectNurse.ExecuteReader();
                    if (reader1.Read())
                    {
                        NurseId = reader1.GetInt32(0);

                        reader1.Close();
                        SqlCommand updateNurseStatus = new SqlCommand("update nurse set registered=1 where empid=" + NurseId, conn);
                        updateNurseStatus.ExecuteNonQuery();
                        updateNurseStatus.Dispose();
                    }
                    else
                    {
                        reader1.Close();
                        MessageBox.Show("No nurses available!!", "Mild Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        NurseId = -1;
                    }
                    SetPhysicianID();
                    IsValidAppointmentDate();
                    PhysicianId = curr.id;
                    if (isPatient && validDate)
                    {
                        app = new Appointment(totalAppointments + 1, PatientId, NurseId, PhysicianId, dateTimePicker1.Value, dateTimePicker1.Value);
                        string query = String.Format("insert into appointment Values({0},{1},{2},{3},'{4}/{5}/{6}','{4}/{5}/{6}');", app.appointmentid, app.patient, app.prepnurse, app.physician, dateTimePicker1.Value.Year,dateTimePicker1.Value.Month,dateTimePicker1.Value.Day);
                        SqlCommand makeAppointment = new SqlCommand(query, conn);
                        makeAppointment.ExecuteNonQuery();
                        makeAppointment.Dispose();
                        
                        MessageBox.Show("Appointment Successfully added!!! Please note appointment ID is "+app.appointmentid+"!!! Your Patient ID is "+app.patient+"!!!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        if (!isPatient)
                        {
                            MessageBox.Show("Appointment can't be added as patient not registered!!!", "Unregistered Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (!validDate)
                        {
                            MessageBox.Show("Appointment can't be added as date not valid!!! Please select a valid date!!", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Appointment can't be added!!! Please try again!!", "Unknown Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch(Exception E)
                {
                    MessageBox.Show(E.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(E);
                }
                finally
                {
                    conn.Close();
                }                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
