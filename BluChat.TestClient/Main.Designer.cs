namespace BluChat.TestClient
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txt_IpAdress = new TextBox();
            label1 = new Label();
            txt_port = new TextBox();
            label2 = new Label();
            btn_start = new Button();
            dgw_log = new DataGridView();
            context = new DataGridViewTextBoxColumn();
            Time = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgw_log).BeginInit();
            SuspendLayout();
            // 
            // txt_IpAdress
            // 
            txt_IpAdress.Location = new Point(14, 55);
            txt_IpAdress.Margin = new Padding(5, 6, 5, 6);
            txt_IpAdress.Name = "txt_IpAdress";
            txt_IpAdress.Size = new Size(119, 35);
            txt_IpAdress.TabIndex = 0;
            txt_IpAdress.Text = "127.0.0.1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 19);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(93, 30);
            label1.TabIndex = 1;
            label1.Text = "IpAdress";
            // 
            // txt_port
            // 
            txt_port.Location = new Point(148, 55);
            txt_port.Margin = new Padding(5, 6, 5, 6);
            txt_port.Name = "txt_port";
            txt_port.Size = new Size(102, 35);
            txt_port.TabIndex = 2;
            txt_port.Text = "9000";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(132, 55);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(18, 30);
            label2.TabIndex = 3;
            label2.Text = ":";
            // 
            // btn_start
            // 
            btn_start.Location = new Point(14, 112);
            btn_start.Name = "btn_start";
            btn_start.Size = new Size(238, 44);
            btn_start.TabIndex = 4;
            btn_start.Text = "Connect";
            btn_start.UseVisualStyleBackColor = true;
            btn_start.Click += btn_start_Click;
            // 
            // dgw_log
            // 
            dgw_log.AllowUserToAddRows = false;
            dgw_log.AllowUserToDeleteRows = false;
            dgw_log.AllowUserToOrderColumns = true;
            dgw_log.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgw_log.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgw_log.Columns.AddRange(new DataGridViewColumn[] { context, Time });
            dgw_log.Location = new Point(279, 55);
            dgw_log.Name = "dgw_log";
            dgw_log.ReadOnly = true;
            dgw_log.Size = new Size(764, 429);
            dgw_log.TabIndex = 5;
            // 
            // context
            // 
            context.DataPropertyName = "context";
            context.HeaderText = "Message";
            context.Name = "context";
            context.ReadOnly = true;
            // 
            // Time
            // 
            Time.DataPropertyName = "Time";
            Time.HeaderText = "Time of send";
            Time.Name = "Time";
            Time.ReadOnly = true;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1055, 496);
            Controls.Add(dgw_log);
            Controls.Add(btn_start);
            Controls.Add(label2);
            Controls.Add(txt_port);
            Controls.Add(label1);
            Controls.Add(txt_IpAdress);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Margin = new Padding(5, 6, 5, 6);
            Name = "Main";
            Text = "Main";
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)dgw_log).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_IpAdress;
        private Label label1;
        private TextBox txt_port;
        private Label label2;
        private Button btn_start;
        private DataGridView dgw_log;
        private DataGridViewTextBoxColumn context;
        private DataGridViewTextBoxColumn Time;
    }
}
