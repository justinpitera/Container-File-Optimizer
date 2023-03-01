using System.Data.SqlClient;

namespace Container_File_Optimizer
{
    partial class NewContainer
    {
        //conection string for SQL
        string connectionString = "Container_File_Optimizer.Properties.Settings.ContainerfileDatabaseConnectionString";

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

        /*
        *  This Fundction uses SQL commands to add a application to the database 
        */
        private void createApp()
        {
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Application (app_name,app_creator) VALUES (@a, @b)", cnn))
            {
                //Execute SQL INSERT
                cmd.Parameters.AddWithValue("@a", "value");
                cmd.Parameters.AddWithValue("@b", "value");

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
        }

        /*
       *  This Fundction uses SQL commands to add a File to the database 
       */
        private void createFile()
        {
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO File (file_name,file_path) VALUES (@a, @b)", cnn))
            {
                //Execute SQL INSERT
                cmd.Parameters.AddWithValue("@a", "value");
                cmd.Parameters.AddWithValue("@b", "value");

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
        }

        /*
         *  This Fundction uses SQL commands to add a connection between a system and an application 
         */
        private void addAppFileConection()
        {
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO AppFile (app_id,file_id) VALUES (@a, @b)", cnn))
            {
                //Execute SQL INSERT
                cmd.Parameters.AddWithValue("@a", "value");
                cmd.Parameters.AddWithValue("@b", "value");

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelCreator = new System.Windows.Forms.Label();
            this.labelSystemName = new System.Windows.Forms.Label();
            this.textBoxSystemName = new System.Windows.Forms.TextBox();
            this.textBoxCreator = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCreateSystem = new System.Windows.Forms.Button();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.labelSystemNameCount = new System.Windows.Forms.Label();
            this.labelCreatorCount = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCreator
            // 
            this.labelCreator.AutoSize = true;
            this.labelCreator.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreator.Location = new System.Drawing.Point(7, 69);
            this.labelCreator.Name = "labelCreator";
            this.labelCreator.Size = new System.Drawing.Size(101, 22);
            this.labelCreator.TabIndex = 16;
            this.labelCreator.Text = "Description:";
            // 
            // labelSystemName
            // 
            this.labelSystemName.AutoSize = true;
            this.labelSystemName.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSystemName.Location = new System.Drawing.Point(7, 3);
            this.labelSystemName.Name = "labelSystemName";
            this.labelSystemName.Size = new System.Drawing.Size(135, 22);
            this.labelSystemName.TabIndex = 15;
            this.labelSystemName.Text = "Container Name:";
            // 
            // textBoxSystemName
            // 
            this.textBoxSystemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSystemName.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSystemName.Location = new System.Drawing.Point(11, 27);
            this.textBoxSystemName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSystemName.MaxLength = 32;
            this.textBoxSystemName.Name = "textBoxSystemName";
            this.textBoxSystemName.Size = new System.Drawing.Size(491, 30);
            this.textBoxSystemName.TabIndex = 14;
            // 
            // textBoxCreator
            // 
            this.textBoxCreator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCreator.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCreator.Location = new System.Drawing.Point(11, 93);
            this.textBoxCreator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxCreator.MaxLength = 255;
            this.textBoxCreator.Name = "textBoxCreator";
            this.textBoxCreator.Size = new System.Drawing.Size(491, 30);
            this.textBoxCreator.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonCreateSystem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 207);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 78);
            this.panel1.TabIndex = 18;
            // 
            // buttonCreateSystem
            // 
            this.buttonCreateSystem.Location = new System.Drawing.Point(11, 4);
            this.buttonCreateSystem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCreateSystem.Name = "buttonCreateSystem";
            this.buttonCreateSystem.Size = new System.Drawing.Size(491, 65);
            this.buttonCreateSystem.TabIndex = 3;
            this.buttonCreateSystem.Text = "Add Container";
            this.buttonCreateSystem.UseVisualStyleBackColor = true;
            // 
            // labelSystemNameCount
            // 
            this.labelSystemNameCount.AutoSize = true;
            this.labelSystemNameCount.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSystemNameCount.Location = new System.Drawing.Point(413, 59);
            this.labelSystemNameCount.Name = "labelSystemNameCount";
            this.labelSystemNameCount.Size = new System.Drawing.Size(52, 22);
            this.labelSystemNameCount.TabIndex = 21;
            this.labelSystemNameCount.Text = "0 / 32";
            // 
            // labelCreatorCount
            // 
            this.labelCreatorCount.AutoSize = true;
            this.labelCreatorCount.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreatorCount.Location = new System.Drawing.Point(413, 125);
            this.labelCreatorCount.Name = "labelCreatorCount";
            this.labelCreatorCount.Size = new System.Drawing.Size(61, 22);
            this.labelCreatorCount.TabIndex = 20;
            this.labelCreatorCount.Text = "0 / 255";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(402, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 22;
            this.button1.Text = "Find file path...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(11, 162);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.MaxLength = 255;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(385, 30);
            this.textBox1.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 22);
            this.label1.TabIndex = 24;
            this.label1.Text = "File Path:";
            // 
            // NewContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 285);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelCreator);
            this.Controls.Add(this.labelSystemName);
            this.Controls.Add(this.textBoxSystemName);
            this.Controls.Add(this.textBoxCreator);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelSystemNameCount);
            this.Controls.Add(this.labelCreatorCount);
            this.Name = "NewContainer";
            this.Text = "Create New Container - {systemName}";
            this.Load += new System.EventHandler(this.NewContainer_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelCreator;
        private System.Windows.Forms.Label labelSystemName;
        private System.Windows.Forms.TextBox textBoxSystemName;
        private System.Windows.Forms.TextBox textBoxCreator;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCreateSystem;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Label labelSystemNameCount;
        private System.Windows.Forms.Label labelCreatorCount;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}