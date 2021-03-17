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
    public partial class ViewBill : Form
    {
        public ViewBill()
        {
            InitializeComponent();
        }
        ConnectionDB conn = new ConnectionDB();
        ArrayList ListStayID = new ArrayList();
        ArrayList ListProcedureID = new ArrayList();
        ArrayList ListProcedureName = new ArrayList();
        ArrayList ListCost = new ArrayList();
        ArrayList ListPaid = new ArrayList();
        ArrayList ListRoomNo = new ArrayList();
        ArrayList ListRoomType = new ArrayList();
        ArrayList ListRoomCost = new ArrayList();
        double totalProcedureCost = 0, totalRoomCost = 0, medicationCost = 0, additionalCharges = 0, total_cost = 0;
        int billNo, patientID,appointmentID;
        bool newBill = false,paid=false;
        private void ViewBill_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (paid == true)
            {
                MessageBox.Show(String.Format("Bill Already Paid!!", total_cost), "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return ;
            }
                
            DialogResult res = MessageBox.Show(String.Format("Are you sure you want to pay ₹ {0}?",total_cost), "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                conn.Open();
                String query = String.Format("update Bills set paid='true' where bill_no={0}", billNo);
                int noerr=conn.ExecuteNonQuery(query);
                conn.Close();
                if(noerr==0)
                MessageBox.Show(String.Format("Successfully Paid ₹{0}!!",total_cost),"Successful",MessageBoxButtons.OK,MessageBoxIcon.Information);
                else MessageBox.Show(String.Format("Some Error Occured!!", total_cost), "Payment Unuccessful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnGenerateBill();
                //Some task…  
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void GetBillNo()
        {
            string query = String.Format("select bill_no,paid from Bills where appointment={0}", appointmentID);
            SqlDataReader read = conn.ExecuteReader(query);
            if (!read.Read())
                newBill = true;
            else
            {
                paid = read.GetBoolean(1);
                billNo = read.GetInt32(0);
            }
            read.Close();
            if (newBill)
            {
                string getBillNo = "select TOP 1 bill_no from Bills order by bill_no desc ;";
                SqlDataReader getBillNoReader = conn.ExecuteReader(getBillNo);
                billNo = 1;
                if (getBillNoReader.Read())
                    billNo = getBillNoReader.GetInt32(0) + 1;
                getBillNoReader.Close();
            }
            
        }
        void OnGenerateBill()
        {
            try
            {
                conn.Open();
                if (textBox1.Text.Length == 0)
                {
                    throw new MyException("Enter Appointment ID first!!");
                }
                appointmentID = Convert.ToInt32(textBox1.Text);
                conn.Open();

                //getting patient id from appointmentid
                SqlDataReader read = conn.ExecuteReader("select patient from appointment where appointmentid=" + appointmentID + ";");
                if (!read.Read())
                {
                    throw new MyException("Appointment ID not found!!Enter Valid Appointment ID!!");
                }
                patientID = read.GetInt32(0);
                read.Close();

                //getting data from the db using patient id
                getData(patientID);

                //getting bill number from db
                GetBillNo();
                total_cost = totalProcedureCost + totalRoomCost+medicationCost+additionalCharges;
                updateDataGrid();

                //if bill is new, then insert into db else do nothing
                if (newBill)
                    InsertIntoDB();

                //MessageBox.Show("")

            }
            catch (FormatException exc)
            {
                MessageBox.Show("Enter Valid Appointment ID", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void button1_Click(object sender, EventArgs e)
        {
            
            OnGenerateBill();
        }
        private void InsertIntoDB()
        {
            try
            {
                string insertIntoDB = String.Format("insert into Bills(bill_no,appointment,procedure_fees,medication_fees,room_charges,additional_charges,paid) values({0},{1},{2},{3},{4},{5},{6});", billNo, appointmentID, totalProcedureCost, medicationCost, totalRoomCost, additionalCharges, paid ? 1:0); 
                conn.ExecuteNonQuery(insertIntoDB);
            }catch(Exception c)
            {
                Console.WriteLine(c.Message);
            }
            
        } 
        private void InitializeData()
        {
             ListStayID.Clear();
             ListProcedureID.Clear();
             ListProcedureName.Clear();
             ListCost.Clear();
             ListPaid.Clear();
             ListRoomNo.Clear();
             ListRoomType.Clear();
             ListRoomCost.Clear();
            totalProcedureCost = 0; totalRoomCost = 0; medicationCost = 0; additionalCharges = 0; total_cost = 0;
        }
        private void getData(int patientID)
        {
            InitializeData();
            try
            {
                conn.Open();
                //finding lists of all procedures done under the name of the patient
                string query = String.Format("select stay, procede,paid from undergoes where patient={0}", patientID, false);
                SqlDataReader read = conn.ExecuteReader(query);
                while (read.Read())
                {
                    ListProcedureID.Add(read.GetInt32(1));
                    ListPaid.Add(read.GetBoolean(2));
                }
                read.Close();

                //fetching procedure costs and name with the respective procedure ids
                for (int i = 0; i < ListProcedureID.Count; i++)
                {
                    bool isNotPaid = !Convert.ToBoolean(ListPaid[i]);
                    if (isNotPaid)
                    {
                        String query2 = String.Format("select name, cost from procedures where code={0};", ListProcedureID[i]);
                        SqlDataReader read2 = conn.ExecuteReader(query2);
                        if (read2.Read())
                        {
                            ListProcedureName.Add(read2.GetString(0));
                            decimal cost2= read2.GetDecimal(1);
                            double cost = Convert.ToDouble(cost2);
                            ListCost.Add(cost);
                            totalProcedureCost += cost;
                            read2.Close();
                        }
                        
                    }
                }
                //read.Close();

                //finding all stays taken by patient
                string query3 = String.Format("select stayid from stay where patient={0};", patientID);
                read = conn.ExecuteReader(query3);
                while (read.Read())
                {
                    ListStayID.Add(read.GetInt32(0));
                }
                read.Close();

                //calculating room charges for each room taken by patient id
                for (int i = 0; i < ListStayID.Count; i++)
                {
                    String query2 = String.Format("select roomno, room.roomtype, room_cost.cost from room left join room_cost on room.roomtype = room_cost.roomtype where room.roomno = (select room from stay where stayid = {0}); ", ListStayID[i]);
                    SqlDataReader read2 = conn.ExecuteReader(query2);
                    if (read2.Read())
                    {
                        ListRoomNo.Add(read2.GetInt32(0));
                        ListRoomType.Add(read2.GetString(1)); ;
                        int roomcost = read2.GetInt32(2);
                        ListRoomCost.Add(roomcost);
                        totalRoomCost += roomcost;
                    }
                    read2.Close();
                }

                //calculating total cost
                total_cost += totalProcedureCost + totalRoomCost + additionalCharges + medicationCost;

                //checking if that bill exists in db
                

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally{
                //conn.Close();
            }
        }
        private void updateDataGrid()
        {
            dataGridView1.Rows.Clear();
            
            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(dataGridView1);
            newRow.Cells[0].Value = billNo;
            newRow.Cells[1].Value = patientID;
            newRow.Cells[2].Value = totalProcedureCost;
            newRow.Cells[3].Value = medicationCost;
            newRow.Cells[4].Value = totalRoomCost;
            newRow.Cells[5].Value = additionalCharges;
            newRow.Cells[6].Value = total_cost;
            newRow.Cells[7].Value = paid ? "YES":"NO";
            dataGridView1.Rows.Add(newRow);
            for(int i = 0; i < ListProcedureID.Count; i++)
            {
                dataGridView2.Rows.Clear();
                DataGridViewRow newRow2 = new DataGridViewRow();
                newRow2.CreateCells(dataGridView2);
                newRow2.Cells[0].Value = ListProcedureID[i];
                newRow2.Cells[1].Value = ListProcedureName[i];
                newRow2.Cells[2].Value = ListCost[i];
                dataGridView2.Rows.Add(newRow2);
            }
            for (int i = 0; i < ListRoomNo.Count; i++)
            {
                dataGridView3.Rows.Clear();
                DataGridViewRow newRow2 = new DataGridViewRow();
                newRow2.CreateCells(dataGridView3);
                newRow2.Cells[0].Value = ListRoomNo[i];
                newRow2.Cells[1].Value = ListRoomType[i];
                newRow2.Cells[2].Value = ListRoomCost[i];
                dataGridView3.Rows.Add(newRow2);
            }

        }
    }
}
