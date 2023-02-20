namespace Container_File_Optimizer
{
    partial class MainScreen
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
            this.buttonCreateContainer = new System.Windows.Forms.Button();
            this.buttonEditSystem = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // buttonCreateNewSystem
            // 
            this.buttonCreateNewSystem.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateNewSystem.Location = new System.Drawing.Point(13, 13);
            this.buttonCreateNewSystem.Name = "buttonCreateNewSystem";
            this.buttonCreateNewSystem.Size = new System.Drawing.Size(340, 99);
            this.buttonCreateNewSystem.TabIndex = 0;
            this.buttonCreateNewSystem.Text = "Create New System";
            this.buttonCreateNewSystem.UseVisualStyleBackColor = true;
            this.buttonCreateNewSystem.Click += new System.EventHandler(this.buttonCreateNewSystem_Click);
            // 
            // buttonCreateContainer
            // 
            this.buttonCreateContainer.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateContainer.Location = new System.Drawing.Point(13, 118);
            this.buttonCreateContainer.Name = "buttonCreateContainer";
            this.buttonCreateContainer.Size = new System.Drawing.Size(340, 99);
            this.buttonCreateContainer.TabIndex = 1;
            this.buttonCreateContainer.Text = "Create Container";
            this.buttonCreateContainer.UseVisualStyleBackColor = true;
            this.buttonCreateContainer.Click += new System.EventHandler(this.buttonCreateContainer_Click);
            // 
            // buttonEditSystem
            // 
            this.buttonEditSystem.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditSystem.Location = new System.Drawing.Point(358, 13);
            this.buttonEditSystem.Name = "buttonEditSystem";
            this.buttonEditSystem.Size = new System.Drawing.Size(340, 99);
            this.buttonEditSystem.TabIndex = 2;
            this.buttonEditSystem.Text = "Edit System";
            this.buttonEditSystem.UseVisualStyleBackColor = true;
            this.buttonEditSystem.Click += new System.EventHandler(this.buttonEditSystem_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.Location = new System.Drawing.Point(358, 118);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(340, 99);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Edit Container";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 228);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonEditSystem);
            this.Controls.Add(this.buttonCreateContainer);
            this.Controls.Add(this.buttonCreateNewSystem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Container File Optimizer - PRESENTATION BUILD";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateNewSystem;
        private System.Windows.Forms.Button buttonCreateContainer;
        private System.Windows.Forms.Button buttonEditSystem;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

