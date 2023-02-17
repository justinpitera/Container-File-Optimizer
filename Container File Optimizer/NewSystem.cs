using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Container_File_Optimizer
{
    public partial class NewSystem : Form
    {
        public NewSystem()
        {
            InitializeComponent();
        }

        private void NewSystem_Load(object sender, EventArgs e)
        {

        }
        private void NewSystem_Load(object sender, FormClosedEventArgs e)
        {
            MainScreen mainScreen = new MainScreen();

            mainScreen.Enabled = true;
        }

        private void buttonCreateSystem_Click(object sender, EventArgs e)
        {
           
        }

        private void ShowToolTip(object sender, EventArgs e)
        {
                String message = "Initiialize System with above applications, and continue to the System Builder interface";
                toolTipInfo.SetToolTip(buttonCreateSystem, message);
        }

        // Failsafe if user accidently closes window to prevent data loss
        private void NewSystem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit? All unsaved changes will be lost.", "Exit", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }

        }
        private void textBoxSystemName_TextChanged(object sender, EventArgs e)
        {
            int current = textBoxSystemName.Text.Length;
            int max = textBoxSystemName.MaxLength;
            labelSystemNameCount.Text = current.ToString() + " /32";
            this.Text = "Create System - " + textBoxSystemName.Text;

            if (current == max)
            {
                labelSystemNameCount.ForeColor = Color.Red;
            }
            else
            {
                labelSystemNameCount.ForeColor = Form.DefaultForeColor;
            }
            if (current == 0) {
                this.Text = "Create System - New System" + textBoxSystemName.Text;
            }
        }

        private void textBoxCreatorName_TextChanged(object sender, EventArgs e)
        {
            int current = textBoxCreator.Text.Length;
            int max = textBoxCreator.MaxLength;
            labelCreatorCount.Text = current.ToString() + " /32";

            if (current == max)
            {
                labelCreatorCount.ForeColor = Color.Red;
            }
            else
            {
                labelCreatorCount.ForeColor = Form.DefaultForeColor;
            }
        }
    }
}
