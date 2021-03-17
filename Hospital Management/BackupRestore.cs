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
    public partial class BackupRestore : Form
    {
        
        public BackupRestore()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS;database=master;Integrated Security=true ");
        SqlCommand cmd;
        SqlDataReader dr;
        private void BackupRestore_Load(object sender, EventArgs e)
        {
            label3.Visible = false;
            serverName(".");

        }
        public void serverName(string str)
        {
            try
            {
                conn.Open();
                string query = "Select *  from sysservers  where srvproduct='SQL Server'";
                cmd = new SqlCommand(query, conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    comboBox1.Items.Add(dr[2]);

                }
            }catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dr.Close();
                conn.Close();
            }                
        }
        public void Createconnection()

        {
            try
            {
               

                comboBox2.Items.Clear();

                string query = "select * from sysdatabases";
                conn.Open();
                cmd = new SqlCommand(query, conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr[0]);
                }               
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dr.Close();
                conn.Close();
            }


        }
        public void blank(string str)

        {
            try
            {
                if (string.IsNullOrEmpty(comboBox1.Text) | string.IsNullOrEmpty(comboBox2.Text))

                {

                    // label3.Visible = true;

                    MessageBox.Show("Server Name & Database can not be Blank");

                    return;

                }

                else

                {

                    if (str == "backup")

                    {

                        saveFileDialog1.FileName = comboBox2.Text;

                        saveFileDialog1.ShowDialog();

                        string s = null;

                        s = saveFileDialog1.FileName;

                        string query = "Backup database " + comboBox2.Text + " to disk='" + s + "'";
                        conn.Open();
                        cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        label3.Visible = true;
                        label3.ForeColor = Color.Green;
                        label3.Text = "Database BackUp has been created successful";

                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            

        }
        void cmbbackup_Click(object sender, EventArgs e)

        {

            blank("backup");

        }



        void cmbserver_SelectedIndexChanged(object sender, EventArgs e)
        {
            Createconnection();
        }
    }
}
