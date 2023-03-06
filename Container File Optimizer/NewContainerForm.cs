using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


// TO DO:
// Dont allow duplicate files 
// Hashmap for <k> File Name <p> File Path
// Loop through the hash map to add each <k> file_name in db | <p> file_path in db 

namespace Container_File_Optimizer
{
    public partial class NewContainerForm : Form
    {
        string dbPath = "C:\\Users\\ff7fa\\OneDrive\\Desktop\\Containerfile\\Container File Optimizer\\ContainerfileDatabase.mdf";

        public NewContainerForm()
        {
            InitializeComponent();
        }

        private void NewContainer_Load(object sender, EventArgs e)
        {
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog findFiles = new OpenFileDialog();
            findFiles.Multiselect = true;

            findFiles.ShowDialog();

            foreach (String file in findFiles.FileNames)
            {
                if (!checkedListBoxFiles.Items.Contains(file))
                {
                    checkedListBoxFiles.Items.Add(file);
                }
                else
                {
                    MessageBox.Show("Error: file already exists in container: " + file, "Container File Optimizer");
                }


            }
        }

        private void textBoxSystemName_TextChanged(object sender, EventArgs e)
        {

        }


        /*
        *  This Fundction uses SQL commands to add a application to the database 
        */
        private void CreateApp()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbPath + ";Integrated Security=True;";

            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Application (app_name,app_desc) VALUES (@a, @b)", cnn))
            {
                //Execute SQL INSERT
                cmd.Parameters.AddWithValue("@a", textBoxSystemName.Text);
                cmd.Parameters.AddWithValue("@b", textBoxCreator.Text);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
        }

        /*
       *  This Fundction uses SQL commands to add a File to the database 
       */
        private void CreateFile()
        {

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbPath + ";Integrated Security=True;";
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO File (file_name,file_path) VALUES (@a, @b)", cnn))
            {
                //Execute SQL INSERT
                cmd.Parameters.AddWithValue("@a", "value");
                cmd.Parameters.AddWithValue("@b", "value");

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
        }

        /*
         *  This Fundction uses SQL commands to add a connection between a system and an application 
         */
        private void addAppFileConection()
        {

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbPath + ";Integrated Security=True;";
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO AppFile (app_id,file_id) VALUES (@app_id, @file_id)", cnn))
            {
                //Execute SQL INSERT
                cmd.Parameters.AddWithValue("@app_id", "value");
                cmd.Parameters.AddWithValue("@file_id", "value");

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

            }
        }

        private void buttonCreateSystem_Click(object sender, EventArgs e)
        {
            CreateApp();
        }

        private void textBoxCreator_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
