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
            this.buttonDeleteContainer = new System.Windows.Forms.Button();
            this.buttonDeleteFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxContainerViewer
            // 
            this.listBoxContainerViewer.FormattingEnabled = true;
            this.listBoxContainerViewer.Location = new System.Drawing.Point(12, 12);
            this.listBoxContainerViewer.Name = "listBoxContainerViewer";
            this.listBoxContainerViewer.Size = new System.Drawing.Size(483, 251);
            this.listBoxContainerViewer.TabIndex = 0;
            this.listBoxContainerViewer.SelectedIndexChanged += new System.EventHandler(this.listBoxContainerViewer_SelectedIndexChanged);
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.Location = new System.Drawing.Point(12, 298);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(483, 251);
            this.listBoxFiles.TabIndex = 1;
            // 
            // buttonDeleteContainer
            // 
            this.buttonDeleteContainer.Location = new System.Drawing.Point(12, 269);
            this.buttonDeleteContainer.Name = "buttonDeleteContainer";
            this.buttonDeleteContainer.Size = new System.Drawing.Size(111, 23);
            this.buttonDeleteContainer.TabIndex = 2;
            this.buttonDeleteContainer.Text = "Delete Container";
            this.buttonDeleteContainer.UseVisualStyleBackColor = true;
            this.buttonDeleteContainer.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonDeleteFile
            // 
            this.buttonDeleteFile.Location = new System.Drawing.Point(12, 555);
            this.buttonDeleteFile.Name = "buttonDeleteFile";
            this.buttonDeleteFile.Size = new System.Drawing.Size(111, 23);
            this.buttonDeleteFile.TabIndex = 3;
            this.buttonDeleteFile.Text = "Delete File";
            this.buttonDeleteFile.UseVisualStyleBackColor = true;
            // 
            // ContainerViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 597);
            this.Controls.Add(this.buttonDeleteFile);
            this.Controls.Add(this.buttonDeleteContainer);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.listBoxContainerViewer);
            this.Name = "ContainerViewer";
            this.Text = "ContainerViewer";
            this.Load += new System.EventHandler(this.ContainerViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxContainerViewer;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Button buttonDeleteContainer;
        private System.Windows.Forms.Button buttonDeleteFile;
    }
}