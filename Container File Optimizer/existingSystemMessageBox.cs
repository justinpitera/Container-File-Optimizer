using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Container_File_Optimizer
{
    public partial class existingSystemMessageBox : Form
    {
        public existingSystemMessageBox()
        {
            InitializeComponent();
        }

        private void btnCreateNewVersion_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void existingSystemMessageBox_Load(object sender, EventArgs e)
        {

        }
    }
}
