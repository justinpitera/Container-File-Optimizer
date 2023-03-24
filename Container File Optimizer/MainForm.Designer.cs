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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Example System 1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Systems", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Example Container 2");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Example Container 1");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Containers", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this.buttonCreateNewSystem = new System.Windows.Forms.Button();
            this.buttonCreateContainer = new System.Windows.Forms.Button();
            this.buttonEditSystem = new System.Windows.Forms.Button();
            this.buttonEditContainer = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCreateNewSystem
            // 
            this.buttonCreateNewSystem.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateNewSystem.Location = new System.Drawing.Point(361, 102);
            this.buttonCreateNewSystem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCreateNewSystem.Name = "buttonCreateNewSystem";
            this.buttonCreateNewSystem.Size = new System.Drawing.Size(255, 80);
            this.buttonCreateNewSystem.TabIndex = 0;
            this.buttonCreateNewSystem.Text = "Create New System";
            this.buttonCreateNewSystem.UseVisualStyleBackColor = true;
            this.buttonCreateNewSystem.Click += new System.EventHandler(this.buttonCreateNewSystem_Click);
            // 
            // buttonCreateContainer
            // 
            this.buttonCreateContainer.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateContainer.Location = new System.Drawing.Point(361, 272);
            this.buttonCreateContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCreateContainer.Name = "buttonCreateContainer";
            this.buttonCreateContainer.Size = new System.Drawing.Size(255, 80);
            this.buttonCreateContainer.TabIndex = 1;
            this.buttonCreateContainer.Text = "Create Container";
            this.buttonCreateContainer.UseVisualStyleBackColor = true;
            this.buttonCreateContainer.Click += new System.EventHandler(this.buttonCreateContainer_Click);
            // 
            // buttonEditSystem
            // 
            this.buttonEditSystem.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditSystem.Location = new System.Drawing.Point(361, 187);
            this.buttonEditSystem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonEditSystem.Name = "buttonEditSystem";
            this.buttonEditSystem.Size = new System.Drawing.Size(255, 80);
            this.buttonEditSystem.TabIndex = 2;
            this.buttonEditSystem.Text = "Edit Existing System";
            this.buttonEditSystem.UseVisualStyleBackColor = true;
            this.buttonEditSystem.Click += new System.EventHandler(this.buttonEditSystem_Click);
            // 
            // buttonEditContainer
            // 
            this.buttonEditContainer.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditContainer.Location = new System.Drawing.Point(361, 358);
            this.buttonEditContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonEditContainer.Name = "buttonEditContainer";
            this.buttonEditContainer.Size = new System.Drawing.Size(255, 80);
            this.buttonEditContainer.TabIndex = 3;
            this.buttonEditContainer.Text = "Edit Container";
            this.buttonEditContainer.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Variable Display", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "Container File Optimzer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Variable Display Semib", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(358, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Get Started";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Variable Display Semib", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(4, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 26);
            this.label3.TabIndex = 6;
            this.label3.Text = "Recent Files";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(9, 102);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextBox1.Multiline = false;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(271, 28);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Segoe UI Variable Display", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(9, 134);
            this.treeView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node2";
            treeNode1.Text = "Example System 1";
            treeNode2.Name = "Node0";
            treeNode2.Text = "Systems";
            treeNode3.Name = "Node4";
            treeNode3.Text = "Example Container 2";
            treeNode4.Name = "Node5";
            treeNode4.Text = "Example Container 1";
            treeNode5.Name = "Node1";
            treeNode5.Text = "Containers";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode5});
            this.treeView1.Size = new System.Drawing.Size(271, 305);
            this.treeView1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Variable Display", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(70, 40);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(236, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Made with love by Pythons at Rowan University";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(629, 448);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonEditContainer);
            this.Controls.Add(this.buttonEditSystem);
            this.Controls.Add(this.buttonCreateContainer);
            this.Controls.Add(this.buttonCreateNewSystem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label4;
    }
}

