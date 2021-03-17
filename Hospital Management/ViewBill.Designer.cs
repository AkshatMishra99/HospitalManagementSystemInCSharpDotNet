namespace Hospital_Management
{
    partial class ViewBill
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.proc_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.proc_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.proc_cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.room_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.room_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.room_cost2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.bill_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.procedure_fees = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.medication_fees = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.room_cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.add_charges = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_charges = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pending = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter Appointment ID";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(138, 57);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(171, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(315, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Generate Bill";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Bill Details";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bill_no,
            this.patient,
            this.procedure_fees,
            this.medication_fees,
            this.room_cost,
            this.add_charges,
            this.total_charges,
            this.pending});
            this.dataGridView1.Location = new System.Drawing.Point(27, 108);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(568, 75);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.proc_id,
            this.proc_name,
            this.proc_cost});
            this.dataGridView2.Location = new System.Drawing.Point(27, 217);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(297, 84);
            this.dataGridView2.TabIndex = 6;
            // 
            // proc_id
            // 
            this.proc_id.HeaderText = "Procedure ID";
            this.proc_id.Name = "proc_id";
            // 
            // proc_name
            // 
            this.proc_name.HeaderText = "Procedure Cost";
            this.proc_name.Name = "proc_name";
            // 
            // proc_cost
            // 
            this.proc_cost.HeaderText = "Procedure Cost";
            this.proc_cost.Name = "proc_cost";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "View Procedures Expenses";
            // 
            // room_no
            // 
            this.room_no.HeaderText = "Room No";
            this.room_no.Name = "room_no";
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.room_no,
            this.room_type,
            this.room_cost2});
            this.dataGridView3.Location = new System.Drawing.Point(351, 217);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(244, 84);
            this.dataGridView3.TabIndex = 7;
            // 
            // room_type
            // 
            this.room_type.HeaderText = "Room Type";
            this.room_type.Name = "room_type";
            // 
            // room_cost2
            // 
            this.room_cost2.HeaderText = "Room Cost";
            this.room_cost2.Name = "room_cost2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(348, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Room Expenses";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(278, 327);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 27);
            this.button2.TabIndex = 11;
            this.button2.Text = "Pay Bill";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // bill_no
            // 
            this.bill_no.HeaderText = "Bill No";
            this.bill_no.Name = "bill_no";
            this.bill_no.ReadOnly = true;
            // 
            // patient
            // 
            this.patient.HeaderText = "Patient ID";
            this.patient.Name = "patient";
            this.patient.ReadOnly = true;
            // 
            // procedure_fees
            // 
            this.procedure_fees.HeaderText = "Procedure Cost";
            this.procedure_fees.Name = "procedure_fees";
            this.procedure_fees.ReadOnly = true;
            // 
            // medication_fees
            // 
            this.medication_fees.HeaderText = "Medication Cost";
            this.medication_fees.Name = "medication_fees";
            this.medication_fees.ReadOnly = true;
            // 
            // room_cost
            // 
            this.room_cost.HeaderText = "Room Cost";
            this.room_cost.Name = "room_cost";
            this.room_cost.ReadOnly = true;
            // 
            // add_charges
            // 
            this.add_charges.HeaderText = "Additional Charges";
            this.add_charges.Name = "add_charges";
            this.add_charges.ReadOnly = true;
            // 
            // total_charges
            // 
            this.total_charges.HeaderText = "Total Cost";
            this.total_charges.Name = "total_charges";
            this.total_charges.ReadOnly = true;
            // 
            // pending
            // 
            this.pending.HeaderText = "Paid";
            this.pending.Name = "pending";
            this.pending.ReadOnly = true;
            // 
            // ViewBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 480);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewBill";
            this.Load += new System.EventHandler(this.ViewBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn proc_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn proc_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn proc_cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn room_no;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn room_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn room_cost2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn bill_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn patient;
        private System.Windows.Forms.DataGridViewTextBoxColumn procedure_fees;
        private System.Windows.Forms.DataGridViewTextBoxColumn medication_fees;
        private System.Windows.Forms.DataGridViewTextBoxColumn room_cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn add_charges;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_charges;
        private System.Windows.Forms.DataGridViewTextBoxColumn pending;
    }
}