namespace Container_File_Optimizer
{
    partial class SystemViewer
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
            this.systemsList = new System.Windows.Forms.ListBox();
            this.containersList = new System.Windows.Forms.ListBox();
            this.filesList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOpenContainerViewer = new System.Windows.Forms.Button();
            this.buttonShowAppDirectory = new System.Windows.Forms.Button();
            this.buttonNewSystem = new System.Windows.Forms.Button();
            this.buttonDeleteSystem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // systemsList
            // 
            this.systemsList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.systemsList.FormattingEnabled = true;
            this.systemsList.ItemHeight = 15;
            this.systemsList.Location = new System.Drawing.Point(12, 44);
            this.systemsList.Name = "systemsList";
            this.systemsList.Size = new System.Drawing.Size(429, 109);
            this.systemsList.TabIndex = 0;
            this.systemsList.SelectedIndexChanged += new System.EventHandler(this.listBoxSystems_SelectedIndexChanged);
            // 
            // containersList
            // 
            this.containersList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.containersList.FormattingEnabled = true;
            this.containersList.ItemHeight = 15;
            this.containersList.Location = new System.Drawing.Point(12, 237);
            this.containersList.Name = "containersList";
            this.containersList.Size = new System.Drawing.Size(429, 109);
            this.containersList.TabIndex = 1;
            this.containersList.SelectedIndexChanged += new System.EventHandler(this.listBoxContainers_SelectedIndexChanged);
            // 
            // filesList
            // 
            this.filesList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filesList.FormattingEnabled = true;
            this.filesList.ItemHeight = 15;
            this.filesList.Location = new System.Drawing.Point(12, 435);
            this.filesList.Name = "filesList";
            this.filesList.Size = new System.Drawing.Size(429, 109);
            this.filesList.TabIndex = 2;
            this.filesList.SelectedIndexChanged += new System.EventHandler(this.listBoxFiles_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Variable Display", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Systems";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Variable Display", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(6, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Containers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Variable Display", 18F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(10, 397);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 32);
            this.label3.TabIndex = 5;
            this.label3.Text = "Files";
            // 
            // buttonOpenContainerViewer
            // 
            this.buttonOpenContainerViewer.Location = new System.Drawing.Point(12, 351);
            this.buttonOpenContainerViewer.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOpenContainerViewer.Name = "buttonOpenContainerViewer";
            this.buttonOpenContainerViewer.Size = new System.Drawing.Size(134, 43);
            this.buttonOpenContainerViewer.TabIndex = 6;
            this.buttonOpenContainerViewer.Text = "Open Container Viewer";
            this.buttonOpenContainerViewer.UseVisualStyleBackColor = true;
            this.buttonOpenContainerViewer.Click += new System.EventHandler(this.buttonOpenContainerViewer_Click);
            // 
            // buttonShowAppDirectory
            // 
            this.buttonShowAppDirectory.Location = new System.Drawing.Point(307, 9);
            this.buttonShowAppDirectory.Margin = new System.Windows.Forms.Padding(2);
            this.buttonShowAppDirectory.Name = "buttonShowAppDirectory";
            this.buttonShowAppDirectory.Size = new System.Drawing.Size(134, 32);
            this.buttonShowAppDirectory.TabIndex = 8;
            this.buttonShowAppDirectory.Text = "Open Systems Directory";
            this.buttonShowAppDirectory.UseVisualStyleBackColor = true;
            this.buttonShowAppDirectory.Click += new System.EventHandler(this.buttonShowAppDirectory_Click);
            // 
            // buttonNewSystem
            // 
            this.buttonNewSystem.Location = new System.Drawing.Point(12, 158);
            this.buttonNewSystem.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewSystem.Name = "buttonNewSystem";
            this.buttonNewSystem.Size = new System.Drawing.Size(123, 43);
            this.buttonNewSystem.TabIndex = 13;
            this.buttonNewSystem.Text = "New System";
            this.buttonNewSystem.UseVisualStyleBackColor = true;
            this.buttonNewSystem.Click += new System.EventHandler(this.buttonNewSystem_Click);
            // 
            // buttonDeleteSystem
            // 
            this.buttonDeleteSystem.Location = new System.Drawing.Point(139, 157);
            this.buttonDeleteSystem.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDeleteSystem.Name = "buttonDeleteSystem";
            this.buttonDeleteSystem.Size = new System.Drawing.Size(123, 43);
            this.buttonDeleteSystem.TabIndex = 12;
            this.buttonDeleteSystem.Text = "Delete System";
            this.buttonDeleteSystem.UseVisualStyleBackColor = true;
            this.buttonDeleteSystem.Click += new System.EventHandler(this.buttonDeleteSystem_Click);
            // 
            // SystemViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(453, 554);
            this.Controls.Add(this.buttonNewSystem);
            this.Controls.Add(this.buttonDeleteSystem);
            this.Controls.Add(this.buttonShowAppDirectory);
            this.Controls.Add(this.buttonOpenContainerViewer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filesList);
            this.Controls.Add(this.containersList);
            this.Controls.Add(this.systemsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SystemViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Existing Systems";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SystemViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox systemsList;
        private System.Windows.Forms.ListBox containersList;
        private System.Windows.Forms.ListBox filesList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOpenContainerViewer;
        private System.Windows.Forms.Button buttonShowAppDirectory;
        private System.Windows.Forms.Button buttonNewSystem;
        private System.Windows.Forms.Button buttonDeleteSystem;
    }
}