﻿using System.Data.SqlClient;

namespace Container_File_Optimizer
{
    partial class NewContainer
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
            this.textBoxContainerName = new System.Windows.Forms.TextBox();
            this.textBoxContainerDesc = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCreateSystem = new System.Windows.Forms.Button();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.labelSystemNameCount = new System.Windows.Forms.Label();
            this.labelCreatorCount = new System.Windows.Forms.Label();
            this.buttonAddFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBoxFiles = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCreator
            // 
            this.labelCreator.AutoSize = true;
            this.labelCreator.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreator.Location = new System.Drawing.Point(5, 56);
            this.labelCreator.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCreator.Name = "labelCreator";
            this.labelCreator.Size = new System.Drawing.Size(83, 17);
            this.labelCreator.TabIndex = 16;
            this.labelCreator.Text = "Description:";
            // 
            // labelSystemName
            // 
            this.labelSystemName.AutoSize = true;
            this.labelSystemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSystemName.Location = new System.Drawing.Point(5, 1);
            this.labelSystemName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSystemName.Name = "labelSystemName";
            this.labelSystemName.Size = new System.Drawing.Size(114, 17);
            this.labelSystemName.TabIndex = 15;
            this.labelSystemName.Text = "Container Name:";
            // 
            // textBoxContainerName
            // 
            this.textBoxContainerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxContainerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxContainerName.Location = new System.Drawing.Point(8, 22);
            this.textBoxContainerName.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.textBoxContainerName.MaxLength = 32;
            this.textBoxContainerName.Name = "textBoxContainerName";
            this.textBoxContainerName.Size = new System.Drawing.Size(369, 23);
            this.textBoxContainerName.TabIndex = 14;
            this.textBoxContainerName.TextChanged += new System.EventHandler(this.textBoxSystemName_TextChanged);
            // 
            // textBoxContainerDesc
            // 
            this.textBoxContainerDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxContainerDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxContainerDesc.Location = new System.Drawing.Point(8, 77);
            this.textBoxContainerDesc.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.textBoxContainerDesc.MaxLength = 255;
            this.textBoxContainerDesc.Name = "textBoxContainerDesc";
            this.textBoxContainerDesc.Size = new System.Drawing.Size(369, 23);
            this.textBoxContainerDesc.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonCreateSystem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 326);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 64);
            this.panel1.TabIndex = 18;
            // 
            // buttonCreateSystem
            // 
            this.buttonCreateSystem.Location = new System.Drawing.Point(8, 3);
            this.buttonCreateSystem.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.buttonCreateSystem.Name = "buttonCreateSystem";
            this.buttonCreateSystem.Size = new System.Drawing.Size(368, 53);
            this.buttonCreateSystem.TabIndex = 3;
            this.buttonCreateSystem.Text = "Add Container";
            this.buttonCreateSystem.UseVisualStyleBackColor = true;
            this.buttonCreateSystem.Click += new System.EventHandler(this.buttonCreateSystem_Click);
            // 
            // labelSystemNameCount
            // 
            this.labelSystemNameCount.AutoSize = true;
            this.labelSystemNameCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSystemNameCount.Location = new System.Drawing.Point(310, 48);
            this.labelSystemNameCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSystemNameCount.Name = "labelSystemNameCount";
            this.labelSystemNameCount.Size = new System.Drawing.Size(44, 17);
            this.labelSystemNameCount.TabIndex = 21;
            this.labelSystemNameCount.Text = "0 / 32";
            // 
            // labelCreatorCount
            // 
            this.labelCreatorCount.AutoSize = true;
            this.labelCreatorCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreatorCount.Location = new System.Drawing.Point(310, 103);
            this.labelCreatorCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCreatorCount.Name = "labelCreatorCount";
            this.labelCreatorCount.Size = new System.Drawing.Size(52, 17);
            this.labelCreatorCount.TabIndex = 20;
            this.labelCreatorCount.Text = "0 / 255";
            // 
            // buttonAddFile
            // 
            this.buttonAddFile.Location = new System.Drawing.Point(8, 131);
            this.buttonAddFile.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.buttonAddFile.Name = "buttonAddFile";
            this.buttonAddFile.Size = new System.Drawing.Size(367, 25);
            this.buttonAddFile.TabIndex = 22;
            this.buttonAddFile.Text = "Add File..";
            this.buttonAddFile.UseVisualStyleBackColor = true;
            this.buttonAddFile.Click += new System.EventHandler(this.button1_AddFile);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 103);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Add file(s) to container";
            // 
            // checkedListBoxFiles
            // 
            this.checkedListBoxFiles.FormattingEnabled = true;
            this.checkedListBoxFiles.Location = new System.Drawing.Point(8, 161);
            this.checkedListBoxFiles.Name = "checkedListBoxFiles";
            this.checkedListBoxFiles.Size = new System.Drawing.Size(368, 154);
            this.checkedListBoxFiles.TabIndex = 25;
            // 
            // NewContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 390);
            this.Controls.Add(this.checkedListBoxFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAddFile);
            this.Controls.Add(this.labelCreator);
            this.Controls.Add(this.labelSystemName);
            this.Controls.Add(this.textBoxContainerName);
            this.Controls.Add(this.textBoxContainerDesc);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelSystemNameCount);
            this.Controls.Add(this.labelCreatorCount);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "NewContainer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New Container";
            this.Load += new System.EventHandler(this.NewContainer_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelCreator;
        private System.Windows.Forms.Label labelSystemName;
        private System.Windows.Forms.TextBox textBoxContainerName;
        private System.Windows.Forms.TextBox textBoxContainerDesc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCreateSystem;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Label labelSystemNameCount;
        private System.Windows.Forms.Label labelCreatorCount;
        private System.Windows.Forms.Button buttonAddFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBoxFiles;
    }
}