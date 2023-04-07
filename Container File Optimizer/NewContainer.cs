using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
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
            string fileName = Path.GetFileName(filePath);
            string fileType = Path.GetExtension(filePath);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO [File] (file_name, file_path, file_type) VALUES (@file_name, @file_path, @file_type)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@file_name", fileName);
                if (FileHasNullBytes(filePath) && fileType != ".so")
                {
                    command.Parameters.AddWithValue("@file_path", filePath);
                    command.Parameters.AddWithValue("@file_type", ".bin");
                    int rowsAffected = command.ExecuteNonQuery();
                }
                else
                {
                    command.Parameters.AddWithValue("@file_path", filePath);
                    command.Parameters.AddWithValue("@file_type", fileType);
                    int rowsAffected = command.ExecuteNonQuery();
                }


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

            CreateApp();
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
            this.Close();
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

            for (int i = 0; i < checkedListBoxFiles.Items.Count; i++)
            {
                checkedListBoxFiles.SetItemChecked(i, true);
            }
        }

        // Used to determine if file is an executable or not
        public static bool FileHasNullBytes(string filePath)
        {
            // Open the file as a binary stream
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int byteValue;
                // Read the file byte by byte
                while ((byteValue = fileStream.ReadByte()) != -1)
                {
                    // Check if the byte value is 0 (a null byte)
                    if (byteValue == 0)
                    {
                        // If a null byte is found, return true
                        return true;
                    }
                }
            }
            // If no null bytes are found, return false
            return false;
        }





    }
}
