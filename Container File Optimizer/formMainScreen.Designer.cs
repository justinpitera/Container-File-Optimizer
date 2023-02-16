namespace Container_File_Optimizer
{
    partial class formMainScreen
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
            this.buttonCreateNewSystem = new System.Windows.Forms.Button();
            this.buttonCreateApplication = new System.Windows.Forms.Button();
            this.buttonEditSystem = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCreateNewSystem
            // 
            this.buttonCreateNewSystem.Location = new System.Drawing.Point(15, 16);
            this.buttonCreateNewSystem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCreateNewSystem.Name = "buttonCreateNewSystem";
            this.buttonCreateNewSystem.Size = new System.Drawing.Size(382, 124);
            this.buttonCreateNewSystem.TabIndex = 0;
            this.buttonCreateNewSystem.Text = "Create New System";
            this.buttonCreateNewSystem.UseVisualStyleBackColor = true;
            this.buttonCreateNewSystem.Click += new System.EventHandler(this.buttonCreateNewSystem_Click);
            // 
            // buttonCreateApplication
            // 
            this.buttonCreateApplication.Location = new System.Drawing.Point(15, 148);
            this.buttonCreateApplication.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCreateApplication.Name = "buttonCreateApplication";
            this.buttonCreateApplication.Size = new System.Drawing.Size(382, 124);
            this.buttonCreateApplication.TabIndex = 1;
            this.buttonCreateApplication.Text = "Create Application";
            this.buttonCreateApplication.UseVisualStyleBackColor = true;
            // 
            // buttonEditSystem
            // 
            this.buttonEditSystem.Location = new System.Drawing.Point(404, 15);
            this.buttonEditSystem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonEditSystem.Name = "buttonEditSystem";
            this.buttonEditSystem.Size = new System.Drawing.Size(382, 124);
            this.buttonEditSystem.TabIndex = 2;
            this.buttonEditSystem.Text = "Edit Existing System";
            this.buttonEditSystem.UseVisualStyleBackColor = true;
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(404, 146);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(382, 124);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Edit Application";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 288);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonEditSystem);
            this.Controls.Add(this.buttonCreateApplication);
            this.Controls.Add(this.buttonCreateNewSystem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.Text = "Container File Optimizer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateNewSystem;
        private System.Windows.Forms.Button buttonCreateApplication;
        private System.Windows.Forms.Button buttonEditSystem;
        private System.Windows.Forms.Button buttonExit;
    }
}

