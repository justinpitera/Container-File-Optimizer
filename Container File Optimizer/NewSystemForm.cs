﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration;

namespace Container_File_Optimizer
{

    public partial class NewSystemForm : Form
    {
        string databasePath = Application.StartupPath + "\\ContainerfileDatabase.mdf";

        public NewSystemForm()
        {
            InitializeComponent();
        }

        private void NewSystem_Load(object sender, EventArgs e)
        {
            // To Do : 
            // Make it so the NewSystem form shows up and this form hides, then when newsystem closes this form reappears
        }
        private void NewSystem_Load(object sender, FormClosedEventArgs e)
        {

        }

        private void buttonCreateSystem_Click(object sender, EventArgs e)
        {

            CreateSystem();
            EditSystemForm systemBuilderForm = new EditSystemForm();
            systemBuilderForm.Show();
            this.Close();
            
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
                MessageBox.Show("Removed: " + listViewContainers.SelectedItems.ToString());
            } else
            {
                // Do nothing
                MessageBox.Show("No changes made");
            }
        }


        /*
         *  This Fundction uses SQL commands to add a system to the database 
         */
        private void CreateSystem()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + databasePath + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string system_name = textBoxSystemName.Text;
                string system_creator = textBoxCreator.Text;
                int version_number = 1;

                // Check if a system with the same system_name and system_creator already exists
                string query = "SELECT COUNT(*) FROM System WHERE system_name = @system_name AND system_creator = @system_creator";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@system_name", system_name);
                command.Parameters.AddWithValue("@system_creator", system_creator);

                int count = (int)command.ExecuteScalar();

                // If a system with the same system_name and system_creator already exists, increment the version_number number
                if (count > 0)
                {
                    query = "SELECT MAX(version_number) FROM System WHERE system_name = @system_name AND system_creator = @system_creator";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@system_name", system_name);
                    command.Parameters.AddWithValue("@system_creator", system_creator);
                    version_number = count + 1;
                }

                // Add the new system to the Systems table
                query = "INSERT INTO System (system_name, system_creator, version_number) VALUES (@system_name, @system_creator, @version_number)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@system_name", system_name);
                command.Parameters.AddWithValue("@system_creator", system_creator);
                command.Parameters.AddWithValue("@version_number", version_number);

                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();
            }

        }

        /*
         *  This Fundction uses SQL commands to add a connection between a system and an application 
         */
        private void AddSysAppConection()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + databasePath + ";Integrated Security=True;";
            //get SQL connection and Command
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = new SqlCommand("INSERT INTO SysApp (system_id,app_id) VALUES (@system_id, @app_id)", sqlConnection))
            {
                //Execute SQL INSERT
                sqlCommand.Parameters.AddWithValue("@system_id", "value");
                sqlCommand.Parameters.AddWithValue("@app_id", "value");

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

            }
        }

        private int SystemCount(SqlConnection cnn)
        {
            //get SQL connection and Command
           
          
                SqlCommand cmd = new SqlCommand("SELECT count(*) FROM System" +
                                                                                     "WHERE system_name = @currSystem AND system_creator = @currCreator", cnn);
            
                cmd.Parameters.AddWithValue("@currSystem",textBoxSystemName);
                cmd.Parameters.AddWithValue("@currCreator", textBoxSystemName);

                int count = 0;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) { 
                        count++;
                    }
                }

                return count;
        }

        private void buttonAddContainer_Click(object sender, EventArgs e)
        {

        }
    }
}
