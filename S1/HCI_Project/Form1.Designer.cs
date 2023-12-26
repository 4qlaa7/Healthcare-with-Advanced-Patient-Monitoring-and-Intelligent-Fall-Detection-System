
namespace HCI_Project
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.MicBtn = new System.Windows.Forms.Button();
            this.Guide = new System.Windows.Forms.Button();
            this.HistoryBtn = new System.Windows.Forms.Button();
            this.SOSBtn = new System.Windows.Forms.Button();
            this.RoomsBtn = new System.Windows.Forms.Button();
            this.HomeBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MicBtn);
            this.panel1.Controls.Add(this.Guide);
            this.panel1.Controls.Add(this.HistoryBtn);
            this.panel1.Controls.Add(this.SOSBtn);
            this.panel1.Controls.Add(this.RoomsBtn);
            this.panel1.Controls.Add(this.HomeBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 89);
            this.panel1.TabIndex = 0;
            // 
            // MicBtn
            // 
            this.MicBtn.BackColor = System.Drawing.Color.Red;
            this.MicBtn.BackgroundImage = global::HCI_Project.Properties.Resources.mic3;
            this.MicBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.MicBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.MicBtn.Location = new System.Drawing.Point(700, 0);
            this.MicBtn.Name = "MicBtn";
            this.MicBtn.Size = new System.Drawing.Size(100, 89);
            this.MicBtn.TabIndex = 5;
            this.MicBtn.UseVisualStyleBackColor = false;
            this.MicBtn.Click += new System.EventHandler(this.MicBtn_Click);
            // 
            // Guide
            // 
            this.Guide.Dock = System.Windows.Forms.DockStyle.Left;
            this.Guide.Location = new System.Drawing.Point(400, 0);
            this.Guide.Name = "Guide";
            this.Guide.Size = new System.Drawing.Size(100, 89);
            this.Guide.TabIndex = 4;
            this.Guide.Text = "Guide";
            this.Guide.UseVisualStyleBackColor = true;
            this.Guide.Click += new System.EventHandler(this.Guide_Click);
            // 
            // HistoryBtn
            // 
            this.HistoryBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.HistoryBtn.Location = new System.Drawing.Point(300, 0);
            this.HistoryBtn.Name = "HistoryBtn";
            this.HistoryBtn.Size = new System.Drawing.Size(100, 89);
            this.HistoryBtn.TabIndex = 3;
            this.HistoryBtn.Text = "History";
            this.HistoryBtn.UseVisualStyleBackColor = true;
            this.HistoryBtn.Click += new System.EventHandler(this.HistoryBtn_Click);
            // 
            // SOSBtn
            // 
            this.SOSBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.SOSBtn.Location = new System.Drawing.Point(200, 0);
            this.SOSBtn.Name = "SOSBtn";
            this.SOSBtn.Size = new System.Drawing.Size(100, 89);
            this.SOSBtn.TabIndex = 2;
            this.SOSBtn.Text = "SOS";
            this.SOSBtn.UseVisualStyleBackColor = true;
            this.SOSBtn.Click += new System.EventHandler(this.SOSBtn_Click);
            // 
            // RoomsBtn
            // 
            this.RoomsBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.RoomsBtn.Location = new System.Drawing.Point(100, 0);
            this.RoomsBtn.Name = "RoomsBtn";
            this.RoomsBtn.Size = new System.Drawing.Size(100, 89);
            this.RoomsBtn.TabIndex = 1;
            this.RoomsBtn.Text = "Rooms";
            this.RoomsBtn.UseVisualStyleBackColor = true;
            this.RoomsBtn.Click += new System.EventHandler(this.RoomsBtn_Click);
            // 
            // HomeBtn
            // 
            this.HomeBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.HomeBtn.Location = new System.Drawing.Point(0, 0);
            this.HomeBtn.Name = "HomeBtn";
            this.HomeBtn.Size = new System.Drawing.Size(100, 89);
            this.HomeBtn.TabIndex = 0;
            this.HomeBtn.Text = "Home";
            this.HomeBtn.UseVisualStyleBackColor = true;
            this.HomeBtn.Click += new System.EventHandler(this.HomeBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 361);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button HomeBtn;
        private System.Windows.Forms.Button HistoryBtn;
        private System.Windows.Forms.Button SOSBtn;
        private System.Windows.Forms.Button RoomsBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Guide;
        private System.Windows.Forms.Button MicBtn;
    }
}

