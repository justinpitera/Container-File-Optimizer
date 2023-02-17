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
            String message = "Initialize System with above applications, and continue to the System Builder interface.";
            toolTipInfo.SetToolTip(buttonCreateSystem, message);
        }

        private void textBoxSystemName_MouseHover(object sender, EventArgs e)
        {
            String message = "Enter the name of the system to be created.";
            toolTipInfo.SetToolTip(textBoxSystemName, message);
        }

        private void textBoxCreator_MouseHover(object sender, EventArgs e)
        {
            String message = "Enter the name of the person responsible for the system.";
            toolTipInfo.SetToolTip(textBoxCreator, message);
        }

        private void listViewContainers_MouseHover(object sender, EventArgs e)
        {
            String message = "The list of containers to initialize the system with. \n To add more containers, click the 'Add Container' button below. \n To remove the selected container, click the 'Remove Container' button below.";
            toolTipInfo.SetToolTip(listViewContainers, message);
        }

        private void buttonAddContainer_MouseHover(object sender, EventArgs e)
        {
            String message = "Add a new container to the list of containers to initialize the new system with.";
            toolTipInfo.Show(message, buttonAddContainer);
        }
        private void buttonRemoveContainer_MouseHover(object sender, EventArgs e)
        {
            String message = "Remove selected container from the list of containers to initialize the new system with.";
            toolTipInfo.Show(message, buttonRemoveContainer);
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
            labelSystemNameCount.Text = current.ToString() + " / 32";
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
            labelCreatorCount.Text = current.ToString() + " / 32";

            if (current == max)
            {
                labelCreatorCount.ForeColor = Color.Red;
            }
            else
            {
                labelCreatorCount.ForeColor = Form.DefaultForeColor;
            }
        }

        private void buttonRemoveContainer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you would like to remove: " + listViewContainers.SelectedItems.ToString() +  "?", "Confirmation of removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("Removed");
            } else
            {
                // Do nothing
                MessageBox.Show("No changes made");
            }
        }
    }
}
