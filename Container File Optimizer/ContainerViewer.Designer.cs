namespace Container_File_Optimizer
{
    partial class ContainerViewer
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
            this.listBoxContainerViewer = new System.Windows.Forms.ListBox();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonDeleteFile = new System.Windows.Forms.Button();
            this.buttonDeleteContainer = new System.Windows.Forms.Button();
            this.buttonNewContainer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxContainerViewer
            // 
            this.listBoxContainerViewer.FormattingEnabled = true;
            this.listBoxContainerViewer.Location = new System.Drawing.Point(11, 44);
            this.listBoxContainerViewer.Name = "listBoxContainerViewer";
            this.listBoxContainerViewer.Size = new System.Drawing.Size(418, 186);
            this.listBoxContainerViewer.TabIndex = 0;
            this.listBoxContainerViewer.SelectedIndexChanged += new System.EventHandler(this.listBoxContainerViewer_SelectedIndexChanged);
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.Location = new System.Drawing.Point(12, 315);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(421, 160);
            this.listBoxFiles.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Variable Display", 18F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(12, 280);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 32);
            this.label3.TabIndex = 7;
            this.label3.Text = "Files";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Variable Display", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "Containers";
            // 
            // buttonDeleteFile
            // 
            this.buttonDeleteFile.Location = new System.Drawing.Point(11, 480);
            this.buttonDeleteFile.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDeleteFile.Name = "buttonDeleteFile";
            this.buttonDeleteFile.Size = new System.Drawing.Size(123, 43);
            this.buttonDeleteFile.TabIndex = 10;
            this.buttonDeleteFile.Text = "Delete File";
            this.buttonDeleteFile.UseVisualStyleBackColor = true;
            this.buttonDeleteFile.Click += new System.EventHandler(this.buttonDeleteFile_Click);
            // 
            // buttonDeleteContainer
            // 
            this.buttonDeleteContainer.Location = new System.Drawing.Point(139, 235);
            this.buttonDeleteContainer.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDeleteContainer.Name = "buttonDeleteContainer";
            this.buttonDeleteContainer.Size = new System.Drawing.Size(123, 43);
            this.buttonDeleteContainer.TabIndex = 8;
            this.buttonDeleteContainer.Text = "Delete Container";
            this.buttonDeleteContainer.UseVisualStyleBackColor = true;
            this.buttonDeleteContainer.Click += new System.EventHandler(this.buttonDeleteContainer_Click);
            // 
            // buttonNewContainer
            // 
            this.buttonNewContainer.Location = new System.Drawing.Point(12, 235);
            this.buttonNewContainer.Margin = new System.Windows.Forms.Padding(2);
            this.buttonNewContainer.Name = "buttonNewContainer";
            this.buttonNewContainer.Size = new System.Drawing.Size(123, 43);
            this.buttonNewContainer.TabIndex = 11;
            this.buttonNewContainer.Text = "New Container";
            this.buttonNewContainer.UseVisualStyleBackColor = true;
            this.buttonNewContainer.Click += new System.EventHandler(this.buttonNewContainer_Click);
            // 
            // ContainerViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(445, 534);
            this.Controls.Add(this.buttonNewContainer);
            this.Controls.Add(this.buttonDeleteFile);
            this.Controls.Add(this.buttonDeleteContainer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.listBoxContainerViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ContainerViewer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Existing Containers";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ContainerViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxContainerViewer;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonDeleteFile;
        private System.Windows.Forms.Button buttonDeleteContainer;
        private System.Windows.Forms.Button buttonNewContainer;
    }
}