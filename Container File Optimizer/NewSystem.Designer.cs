namespace Container_File_Optimizer
{
    partial class NewSystem
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
            this.components = new System.ComponentModel.Container();
            this.labelCreator = new System.Windows.Forms.Label();
            this.labelSystemName = new System.Windows.Forms.Label();
            this.textBoxSystemName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textBoxCreator = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCreateSystem = new System.Windows.Forms.Button();
            this.buttonRemoveApplication = new System.Windows.Forms.Button();
            this.buttonAddApplication = new System.Windows.Forms.Button();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.labelCreatorCount = new System.Windows.Forms.Label();
            this.labelSystemNameCount = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCreator
            // 
            this.labelCreator.AutoSize = true;
            this.labelCreator.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreator.Location = new System.Drawing.Point(7, 72);
            this.labelCreator.Name = "labelCreator";
            this.labelCreator.Size = new System.Drawing.Size(71, 22);
            this.labelCreator.TabIndex = 8;
            this.labelCreator.Text = "Creator:";
            // 
            // labelSystemName
            // 
            this.labelSystemName.AutoSize = true;
            this.labelSystemName.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSystemName.Location = new System.Drawing.Point(7, 6);
            this.labelSystemName.Name = "labelSystemName";
            this.labelSystemName.Size = new System.Drawing.Size(117, 22);
            this.labelSystemName.TabIndex = 7;
            this.labelSystemName.Text = "System Name:";
            // 
            // textBoxSystemName
            // 
            this.textBoxSystemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSystemName.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSystemName.Location = new System.Drawing.Point(11, 30);
            this.textBoxSystemName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSystemName.MaxLength = 32;
            this.textBoxSystemName.Name = "textBoxSystemName";
            this.textBoxSystemName.Size = new System.Drawing.Size(408, 30);
            this.textBoxSystemName.TabIndex = 5;
            this.textBoxSystemName.TextChanged += new System.EventHandler(this.textBoxSystemName_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 165);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(431, 201);
            this.panel2.TabIndex = 11;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(431, 201);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // textBoxCreator
            // 
            this.textBoxCreator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCreator.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCreator.Location = new System.Drawing.Point(11, 96);
            this.textBoxCreator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxCreator.MaxLength = 32;
            this.textBoxCreator.Name = "textBoxCreator";
            this.textBoxCreator.Size = new System.Drawing.Size(408, 30);
            this.textBoxCreator.TabIndex = 10;
            this.textBoxCreator.TextChanged += new System.EventHandler(this.textBoxCreatorName_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonCreateSystem);
            this.panel1.Controls.Add(this.buttonRemoveApplication);
            this.panel1.Controls.Add(this.buttonAddApplication);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 366);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(431, 78);
            this.panel1.TabIndex = 10;
            // 
            // buttonCreateSystem
            // 
            this.buttonCreateSystem.Location = new System.Drawing.Point(181, 4);
            this.buttonCreateSystem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCreateSystem.Name = "buttonCreateSystem";
            this.buttonCreateSystem.Size = new System.Drawing.Size(238, 70);
            this.buttonCreateSystem.TabIndex = 3;
            this.buttonCreateSystem.Text = "Continue";
            this.buttonCreateSystem.UseVisualStyleBackColor = true;
            this.buttonCreateSystem.Click += new System.EventHandler(this.buttonCreateSystem_Click);
            this.buttonCreateSystem.MouseHover += new System.EventHandler(this.ShowToolTip);
            // 
            // buttonRemoveApplication
            // 
            this.buttonRemoveApplication.Location = new System.Drawing.Point(3, 42);
            this.buttonRemoveApplication.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonRemoveApplication.Name = "buttonRemoveApplication";
            this.buttonRemoveApplication.Size = new System.Drawing.Size(172, 34);
            this.buttonRemoveApplication.TabIndex = 2;
            this.buttonRemoveApplication.Text = "Remove Container";
            this.buttonRemoveApplication.UseVisualStyleBackColor = true;
            // 
            // buttonAddApplication
            // 
            this.buttonAddApplication.Location = new System.Drawing.Point(3, 5);
            this.buttonAddApplication.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAddApplication.Name = "buttonAddApplication";
            this.buttonAddApplication.Size = new System.Drawing.Size(172, 34);
            this.buttonAddApplication.TabIndex = 1;
            this.buttonAddApplication.Text = "Add Container";
            this.buttonAddApplication.UseVisualStyleBackColor = true;
            // 
            // labelCreatorCount
            // 
            this.labelCreatorCount.AutoSize = true;
            this.labelCreatorCount.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreatorCount.Location = new System.Drawing.Point(365, 128);
            this.labelCreatorCount.Name = "labelCreatorCount";
            this.labelCreatorCount.Size = new System.Drawing.Size(44, 22);
            this.labelCreatorCount.TabIndex = 12;
            this.labelCreatorCount.Text = "0/32";
            // 
            // labelSystemNameCount
            // 
            this.labelSystemNameCount.AutoSize = true;
            this.labelSystemNameCount.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSystemNameCount.Location = new System.Drawing.Point(365, 62);
            this.labelSystemNameCount.Name = "labelSystemNameCount";
            this.labelSystemNameCount.Size = new System.Drawing.Size(44, 22);
            this.labelSystemNameCount.TabIndex = 13;
            this.labelSystemNameCount.Text = "0/32";
            // 
            // NewSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 444);
            this.Controls.Add(this.labelSystemNameCount);
            this.Controls.Add(this.labelCreatorCount);
            this.Controls.Add(this.textBoxCreator);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelCreator);
            this.Controls.Add(this.labelSystemName);
            this.Controls.Add(this.textBoxSystemName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "NewSystem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create System - New System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewSystem_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewSystem_Load);
            this.Load += new System.EventHandler(this.NewSystem_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelCreator;
        private System.Windows.Forms.Label labelSystemName;
        private System.Windows.Forms.TextBox textBoxSystemName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxCreator;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAddApplication;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button buttonCreateSystem;
        private System.Windows.Forms.Button buttonRemoveApplication;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Label labelCreatorCount;
        private System.Windows.Forms.Label labelSystemNameCount;
    }
}