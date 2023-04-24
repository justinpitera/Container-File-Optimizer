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
            this.buttonDeleteContainer = new System.Windows.Forms.Button();
            this.buttonDeleteFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxContainerViewer
            // 
            this.listBoxContainerViewer.FormattingEnabled = true;
            this.listBoxContainerViewer.ItemHeight = 16;
            this.listBoxContainerViewer.Location = new System.Drawing.Point(20, 63);
            this.listBoxContainerViewer.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxContainerViewer.Name = "listBoxContainerViewer";
            this.listBoxContainerViewer.Size = new System.Drawing.Size(643, 260);
            this.listBoxContainerViewer.TabIndex = 0;
            this.listBoxContainerViewer.SelectedIndexChanged += new System.EventHandler(this.listBoxContainerViewer_SelectedIndexChanged);
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.ItemHeight = 16;
            this.listBoxFiles.Location = new System.Drawing.Point(16, 431);
            this.listBoxFiles.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(643, 244);
            this.listBoxFiles.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Variable Display", 18F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(13, 387);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 40);
            this.label3.TabIndex = 7;
            this.label3.Text = "Files";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Variable Display", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(13, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 40);
            this.label2.TabIndex = 6;
            this.label2.Text = "Containers";
            // 
            // buttonDeleteContainer
            // 
            this.buttonDeleteContainer.Location = new System.Drawing.Point(20, 331);
            this.buttonDeleteContainer.Name = "buttonDeleteContainer";
            this.buttonDeleteContainer.Size = new System.Drawing.Size(164, 53);
            this.buttonDeleteContainer.TabIndex = 8;
            this.buttonDeleteContainer.Text = "Delete Container";
            this.buttonDeleteContainer.UseVisualStyleBackColor = true;
            this.buttonDeleteContainer.Click += new System.EventHandler(this.buttonDeleteContainer_Click);
            // 
            // buttonDeleteFile
            // 
            this.buttonDeleteFile.Location = new System.Drawing.Point(16, 682);
            this.buttonDeleteFile.Name = "buttonDeleteFile";
            this.buttonDeleteFile.Size = new System.Drawing.Size(164, 53);
            this.buttonDeleteFile.TabIndex = 10;
            this.buttonDeleteFile.Text = "Delete File";
            this.buttonDeleteFile.UseVisualStyleBackColor = true;
            this.buttonDeleteFile.Click += new System.EventHandler(this.buttonDeleteFile_Click);
            // 
            // ContainerViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(683, 743);
            this.Controls.Add(this.buttonDeleteFile);
            this.Controls.Add(this.buttonDeleteContainer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.listBoxContainerViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ContainerViewer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Container Viewer";
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
        private System.Windows.Forms.Button buttonDeleteContainer;
        private System.Windows.Forms.Button buttonDeleteFile;
    }
}