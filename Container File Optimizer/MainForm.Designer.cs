namespace Container_File_Optimizer
{
    partial class Main
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
            this.buttonEditContainer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCreateNewSystem
            // 
            this.buttonCreateNewSystem.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateNewSystem.Location = new System.Drawing.Point(10, 125);
            this.buttonCreateNewSystem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCreateNewSystem.Name = "buttonCreateNewSystem";
            this.buttonCreateNewSystem.Size = new System.Drawing.Size(340, 98);
            this.buttonCreateNewSystem.TabIndex = 0;
            this.buttonCreateNewSystem.Text = "New System";
            this.buttonCreateNewSystem.UseVisualStyleBackColor = true;
            this.buttonCreateNewSystem.Click += new System.EventHandler(this.buttonCreateNewSystem_Click);
            // 
            // buttonCreateContainer
            // 
            this.buttonCreateContainer.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateContainer.Location = new System.Drawing.Point(10, 227);
            this.buttonCreateContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCreateContainer.Name = "buttonCreateContainer";
            this.buttonCreateContainer.Size = new System.Drawing.Size(340, 98);
            this.buttonCreateContainer.TabIndex = 1;
            this.buttonCreateContainer.Text = "New Container";
            this.buttonCreateContainer.UseVisualStyleBackColor = true;
            this.buttonCreateContainer.Click += new System.EventHandler(this.buttonCreateContainer_Click);
            // 
            // buttonEditSystem
            // 
            this.buttonEditSystem.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditSystem.Location = new System.Drawing.Point(402, 125);
            this.buttonEditSystem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonEditSystem.Name = "buttonEditSystem";
            this.buttonEditSystem.Size = new System.Drawing.Size(340, 98);
            this.buttonEditSystem.TabIndex = 2;
            this.buttonEditSystem.Text = "System Viewer";
            this.buttonEditSystem.UseVisualStyleBackColor = true;
            this.buttonEditSystem.Click += new System.EventHandler(this.buttonEditSystem_Click);
            // 
            // buttonEditContainer
            // 
            this.buttonEditContainer.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditContainer.Location = new System.Drawing.Point(402, 227);
            this.buttonEditContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonEditContainer.Name = "buttonEditContainer";
            this.buttonEditContainer.Size = new System.Drawing.Size(340, 98);
            this.buttonEditContainer.TabIndex = 3;
            this.buttonEditContainer.Text = "Container Viewer";
            this.buttonEditContainer.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Variable Display", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(349, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "Container File Optimzer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Variable Display", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(93, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(279, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Made with love by Pythons at Rowan University";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Variable Display Semib", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(6, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 31);
            this.label2.TabIndex = 5;
            this.label2.Text = "Create";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Variable Display Semib", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(396, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 31);
            this.label3.TabIndex = 6;
            this.label3.Text = "Modify";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(755, 342);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonEditContainer);
            this.Controls.Add(this.buttonEditSystem);
            this.Controls.Add(this.buttonCreateContainer);
            this.Controls.Add(this.buttonCreateNewSystem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Container File Optimizer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateNewSystem;
        private System.Windows.Forms.Button buttonCreateContainer;
        private System.Windows.Forms.Button buttonEditSystem;
        private System.Windows.Forms.Button buttonEditContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

