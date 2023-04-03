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
using System.Runtime.InteropServices;
using System.IO;

namespace Container_File_Optimizer
{

    public partial class NewContainer : Form
    {
        //Hardcoded:
        //string dbPath = "C:\\Users\\justi\\Source\\Repos\\justinpitera\\Container-File-Optimizer\\Container File Optimizer\\ContainerfileDatabase.mdf";


        // Not hardcoded
        string connectionString = ConfigurationManager.ConnectionStrings["Container_File_Optimizer.Properties.Settings.ContainerfileDatabaseConnectionString"].ConnectionString;

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

            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Application (app_name,app_desc) VALUES (@a, @b)", cnn))
            {
                //Execute SQL INSERT
                cmd.Parameters.AddWithValue("@a", textBoxContainerName.Text);
                cmd.Parameters.AddWithValue("@b", textBoxContainerDesc.Text);

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
        private void AddAppFileConection(int appID, string filePath)
        {
            string fileName = Regex.Match(filePath, @"(?<=\\)[^\\]*$").Value;
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                int fileID;


                string query = "SELECT file_id FROM [File] WHERE file_name = @file_name AND file_path = @file_path";


                SqlCommand command = new SqlCommand(query, cnn);

                command.Parameters.AddWithValue("@file_name", fileName);
                command.Parameters.AddWithValue("@file_path", filePath);
                fileID = (int)command.ExecuteScalar();


                query = "INSERT INTO AppFile (app_id, file_id) VALUES (@app_id, @file_id)";



                command = new SqlCommand(query, cnn);

                command.Parameters.AddWithValue("@app_id", appID);
                command.Parameters.AddWithValue("@file_id", fileID);

                command.ExecuteNonQuery();
                cnn.Close();


            }
        }

        private int GetAppID()
        {
            int appID = 0;
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT app_id FROM Application WHERE app_name = @app_name AND app_desc = @app_desc";


                SqlCommand command = new SqlCommand(query, cnn);

                command.Parameters.AddWithValue("@app_name", textBoxContainerName.Text);
                command.Parameters.AddWithValue("@app_desc", textBoxContainerDesc.Text);


                appID = (int)command.ExecuteScalar();
                cnn.Close();
                return appID;

            }
        }

        // Create app, then create each file from file list, then create connections in bridge table for apps and files
        private void buttonCreateSystem_Click(object sender, EventArgs e)
        {

            string root = @"C:\Saved_Files";
            // If directory does not exist, create it.
            // TODO: Check overwrite or not?
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            CreateApp();
            //TODO: pull application name from database that was just created, use as a new subfolder of root
            //Directory.CreateDirectory(root + textBoxContainerName.Text);


            // Create files from the list into Database table for Files
            foreach (String filePath in checkedListBoxFiles.CheckedItems)
            {
                CreateFile(filePath);

            }
            // Creating connections between files and app
            foreach (String filePath in checkedListBoxFiles.CheckedItems)
            {
                AddAppFileConection(GetAppID(), filePath);
            }

            // Saves selected files onto the disk
            foreach (String filePath in checkedListBoxFiles.CheckedItems)
            {
                // Calls query to pull file's id
                string fileName = GetFileName(filePath);

                // Copy file data using location INTO new file
                if (File.Exists(filePath))
                {
                    string destinationPath = Path.Combine(root, fileName + Path.GetExtension(filePath));
                    File.Copy(filePath, destinationPath, true); // copies data from filePath to destinationPath
                }
                else
                {
                    MessageBox.Show("File does not exist at filePath: " + filePath);
                }
            }
            this.Close();
        }

        // Returns the fileName of a specific file from the database
        private string GetFileName(string filePath)
        {
            string file_name = "";
            //get SQL connection and Command
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "SELECT file_name FROM [File] WHERE file_path = @file_path";

                SqlCommand command = new SqlCommand(query, cnn);

                command.Parameters.AddWithValue("@file_path", filePath);


                file_name = (string)command.ExecuteScalar();
                cnn.Close();
                return file_name;

            }
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
                }
                else
                {
                    MessageBox.Show("Error: file already exists in container: " + filePath, "Container File Optimizer");
                }
            }
        }
    }
}
