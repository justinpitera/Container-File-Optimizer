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
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;

namespace Container_File_Optimizer
{


    // TO DO: 
    // Hash map for 

    
    public partial class NewContainer : Form
    {
        string dbPath = "C:\\Users\\justi\\source\\repos\\Container File Optimizer\\Container File Optimizer\\ContainerfileDatabase.mdf";

  



        public NewContainer()
        {
            InitializeComponent();
        }

        private void NewContainer_Load(object sender, EventArgs e)
        {
 
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
        private void CreateFile(String filePath)
        {

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbPath + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                    connection.Open();
                    string fileName = Regex.Match(filePath, @"(?<=\\)[^\\]*$").Value;

                    string query = "INSERT INTO [File] (file_name, file_path) VALUES (@file_name, @file_path)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@file_name", fileName);
                    command.Parameters.AddWithValue("@file_path", filePath);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

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

        private void button1_AddFile(object sender, EventArgs e)
        {
            OpenFileDialog findFiles = new OpenFileDialog();
            findFiles.Multiselect = true;

            findFiles.ShowDialog();

            foreach (String filePath in findFiles.FileNames)
            {
                if (!checkedListBoxFiles.Items.Contains(filePath))
                {
                    checkedListBoxFiles.Items.Add(filePath);
                    CreateFile(filePath);
                }
                else
                {
                    MessageBox.Show("Error: file already exists in container: " + filePath, "Container File Optimizer");
                }
            }
        }
    }
}
