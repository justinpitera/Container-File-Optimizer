using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using System.Threading;

namespace Container_File_Optimizer
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonCreateNewSystem_Click(object sender, EventArgs e)
        {
            NewSystem newSystem = new NewSystem();
            newSystem.ShowDialog();
        }

        private void buttonEditSystem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openSystemFile = new OpenFileDialog();
            openSystemFile.Filter = "Container File Optimizer files (*.cfo)|*.cfo";
            openSystemFile.InitialDirectory = Application.StartupPath;
            openSystemFile.ShowDialog();
            MessageBox.Show("Opening: " + openSystemFile.FileName);
        }

        private void buttonCreateContainer_Click(object sender, EventArgs e)
        {
            NewContainer newContainer = new NewContainer();
            newContainer.ShowDialog();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {

        }
    }
}