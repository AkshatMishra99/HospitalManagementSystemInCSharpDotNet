using System;
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
   
    
    public partial class Login : Form
    {
        private string user, password, type;
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Server=.\\SQLEXPRESS;database=Hospital;Integrated Security=true ");
            userType.SelectedIndex=0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select password,type from Users where username='"+username.Text+"'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            
            try
            {
                user = username.Text;
                password = pass.Text;
                type = (userType.Items[userType.SelectedIndex]).ToString().ToLower();
                
                if (!reader.Read())
                {
                    MessageBox.Show("NO SUCH USER PRESENT!!", "INVALID USER", MessageBoxButtons.OK);
                }
                else
                {
                    Console.WriteLine("{0}, {1}", reader.GetString(0), reader.GetString(1));
                    if(reader.GetString(0)==password)
                    {
                        if (reader.GetString(1) == type)
                        {
                            MessageBox.Show("Welcome " + user + "!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            reader.Close();
                            conn.Close();
                            if (type == "admin")
                                new AdminScreen(this).Show();
                            else if (type == "patient")
                                new PatientView(this).Show();
                            //
                            else if (type == "staff")
                                new StaffView(this).Show();
                                this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Your not authorised as "+type+". Please choose the correct type!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong password. Please enter the correct password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception E)
            {
                Console.WriteLine(E.Message);
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void userType_SelectedIndexChanged(object sender, EventArgs e)
        {
            type = userType.Items[userType.SelectedIndex].ToString();
        }
    }
    class ConnectionDB
    {

        SqlConnection conn;
        public static string strProvider;


        public bool Open()
        {
            try
            {
                strProvider = "Server=.\\SQLEXPRESS;database=Hospital;Integrated Security=true ";
                conn = new SqlConnection(strProvider);
                conn.Open();
                return true;
            }
            catch (Exception er)
            {
                MessageBox.Show("Connection Error ! " + er.Message, "Information");
            }
            return false;
        }

        public void Close()
        {
            conn.Close();
            conn.Dispose();
        }

        public DataSet ExecuteDataSet(string sql)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds, "result");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public SqlDataReader ExecuteReader(string sql)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public int ExecuteNonQuery(string sql)
        {
            try
            {
                SqlCommand exec = new SqlCommand(sql, conn);
                exec.ExecuteNonQuery();
                exec.Dispose();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return -1;
        }
    }
}
