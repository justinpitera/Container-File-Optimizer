using System.Data.SqlClient;

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
            this.labelCreator = new System.Windows.Forms.Label();
            this.labelSystemName = new System.Windows.Forms.Label();
            this.textBoxSystemName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkedListBoxContainers = new System.Windows.Forms.CheckedListBox();
            this.textBoxCreator = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonAddContainer = new System.Windows.Forms.Button();
            this.buttonSystemViewer = new System.Windows.Forms.Button();
            this.buttonCreateSystem = new System.Windows.Forms.Button();
            this.labelCreatorCount = new System.Windows.Forms.Label();
            this.labelSystemNameCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCreator
            // 
            this.labelCreator.AutoSize = true;
            this.labelCreator.Font = new System.Drawing.Font("Segoe UI Variable Display Semib", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreator.ForeColor = System.Drawing.SystemColors.Control;
            this.labelCreator.Location = new System.Drawing.Point(5, 54);
            this.labelCreator.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCreator.Name = "labelCreator";
            this.labelCreator.Size = new System.Drawing.Size(70, 21);
            this.labelCreator.TabIndex = 8;
            this.labelCreator.Text = "Creator:";
            // 
            // labelSystemName
            // 
            this.labelSystemName.AutoSize = true;
            this.labelSystemName.Font = new System.Drawing.Font("Segoe UI Variable Display Semib", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSystemName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelSystemName.Location = new System.Drawing.Point(5, 5);
            this.labelSystemName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSystemName.Name = "labelSystemName";
            this.labelSystemName.Size = new System.Drawing.Size(115, 21);
            this.labelSystemName.TabIndex = 7;
            this.labelSystemName.Text = "System Name:";
            // 
            // textBoxSystemName
            // 
            this.textBoxSystemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSystemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSystemName.Location = new System.Drawing.Point(10, 28);
            this.textBoxSystemName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSystemName.MaxLength = 50;
            this.textBoxSystemName.Name = "textBoxSystemName";
            this.textBoxSystemName.Size = new System.Drawing.Size(369, 23);
            this.textBoxSystemName.TabIndex = 5;
            this.textBoxSystemName.TextChanged += new System.EventHandler(this.textBoxSystemName_TextChanged);
            this.textBoxSystemName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSystemName_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkedListBoxContainers);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 145);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(386, 163);
            this.panel2.TabIndex = 11;
            // 
            // checkedListBoxContainers
            // 
            this.checkedListBoxContainers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxContainers.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxContainers.FormattingEnabled = true;
            this.checkedListBoxContainers.Location = new System.Drawing.Point(0, 0);
            this.checkedListBoxContainers.Margin = new System.Windows.Forms.Padding(2);
            this.checkedListBoxContainers.Name = "checkedListBoxContainers";
            this.checkedListBoxContainers.Size = new System.Drawing.Size(386, 163);
            this.checkedListBoxContainers.TabIndex = 0;
            // 
            // textBoxCreator
            // 
            this.textBoxCreator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCreator.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCreator.Location = new System.Drawing.Point(8, 78);
            this.textBoxCreator.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCreator.MaxLength = 50;
            this.textBoxCreator.Name = "textBoxCreator";
            this.textBoxCreator.Size = new System.Drawing.Size(369, 23);
            this.textBoxCreator.TabIndex = 10;
            this.textBoxCreator.TextChanged += new System.EventHandler(this.textBoxCreatorName_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonAddContainer);
            this.panel1.Controls.Add(this.buttonSystemViewer);
            this.panel1.Controls.Add(this.buttonCreateSystem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 308);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 100);
            this.panel1.TabIndex = 10;
            // 
            // buttonAddContainer
            // 
            this.buttonAddContainer.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddContainer.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonAddContainer.Location = new System.Drawing.Point(234, 49);
            this.buttonAddContainer.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddContainer.Name = "buttonAddContainer";
            this.buttonAddContainer.Size = new System.Drawing.Size(150, 44);
            this.buttonAddContainer.TabIndex = 1;
            this.buttonAddContainer.Text = "Container Viewer";
            this.buttonAddContainer.UseVisualStyleBackColor = true;
            this.buttonAddContainer.Click += new System.EventHandler(this.buttonAddContainer_Click);
            // 
            // buttonSystemViewer
            // 
            this.buttonSystemViewer.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSystemViewer.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonSystemViewer.Location = new System.Drawing.Point(234, 4);
            this.buttonSystemViewer.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSystemViewer.Name = "buttonSystemViewer";
            this.buttonSystemViewer.Size = new System.Drawing.Size(150, 44);
            this.buttonSystemViewer.TabIndex = 4;
            this.buttonSystemViewer.Text = "System Viewer";
            this.buttonSystemViewer.UseVisualStyleBackColor = true;
            this.buttonSystemViewer.Click += new System.EventHandler(this.buttonSystemViewer_Click);
            // 
            // buttonCreateSystem
            // 
            this.buttonCreateSystem.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateSystem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonCreateSystem.Location = new System.Drawing.Point(2, 4);
            this.buttonCreateSystem.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCreateSystem.Name = "buttonCreateSystem";
            this.buttonCreateSystem.Size = new System.Drawing.Size(228, 89);
            this.buttonCreateSystem.TabIndex = 3;
            this.buttonCreateSystem.Text = "Create Optimized System";
            this.buttonCreateSystem.UseVisualStyleBackColor = true;
            this.buttonCreateSystem.Click += new System.EventHandler(this.buttonCreateSystem_Click);
            // 
            // labelCreatorCount
            // 
            this.labelCreatorCount.AutoSize = true;
            this.labelCreatorCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreatorCount.ForeColor = System.Drawing.SystemColors.Control;
            this.labelCreatorCount.Location = new System.Drawing.Point(310, 104);
            this.labelCreatorCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCreatorCount.Name = "labelCreatorCount";
            this.labelCreatorCount.Size = new System.Drawing.Size(44, 17);
            this.labelCreatorCount.TabIndex = 12;
            this.labelCreatorCount.Text = "0 / 50";
            // 
            // labelSystemNameCount
            // 
            this.labelSystemNameCount.AutoSize = true;
            this.labelSystemNameCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSystemNameCount.ForeColor = System.Drawing.SystemColors.Control;
            this.labelSystemNameCount.Location = new System.Drawing.Point(310, 52);
            this.labelSystemNameCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSystemNameCount.Name = "labelSystemNameCount";
            this.labelSystemNameCount.Size = new System.Drawing.Size(44, 17);
            this.labelSystemNameCount.TabIndex = 13;
            this.labelSystemNameCount.Text = "0 / 50";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Variable Display Semib", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(6, 108);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 21);
            this.label1.TabIndex = 14;
            this.label1.Text = "Select Containers:";
            // 
            // NewSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(386, 408);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelSystemNameCount);
            this.Controls.Add(this.labelCreatorCount);
            this.Controls.Add(this.textBoxCreator);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelCreator);
            this.Controls.Add(this.labelSystemName);
            this.Controls.Add(this.textBoxSystemName);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "NewSystem";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New System";
            this.TopMost = true;
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
        private System.Windows.Forms.Button buttonAddContainer;
        private System.Windows.Forms.Button buttonCreateSystem;
        private System.Windows.Forms.Label labelCreatorCount;
        private System.Windows.Forms.Label labelSystemNameCount;
        private System.Windows.Forms.CheckedListBox checkedListBoxContainers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSystemViewer;
    }
}