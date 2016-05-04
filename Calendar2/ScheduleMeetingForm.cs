﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendar2
{
    public partial class ScheduleMeetingForm : Form
    {
        public ScheduleMeetingForm()
        {
            InitializeComponent();

            ArrayList eventList = new ArrayList(); //create array list to hold events
            DataTable myTable = new DataTable(); //create a data table to store results of sql query
            string connStr = "server=brettnapier.com;user=csc340Group;database=csc340GroupProject;port=3306;password=cscproject;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            //attempt to make a connection to the SQL server
            try
            {

                Console.WriteLine("Connecting to MySQL...");

                conn.Open();

                string sql = "SELECT * FROM Events WHERE eventDate=@myDate ORDER BY eventStartTime ASC";

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@myDate", dateString); //add paramaters to query
                MySql.Data.MySqlClient.MySqlDataAdapter myAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
                myAdapter.Fill(myTable); //fill the data table with results
                Console.WriteLine("Table is ready");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();

            //iterate through retrieved tables and process the elements
            foreach (DataRow row in myTable.Rows)
            {
                Event newEvent = new Event(); //create a new Event object for each row in the table
                newEvent.eventID = row["eventID"].ToString(); //get the event id and store it in the Event object
                newEvent.eventTitle = row["eventTitle"].ToString(); //get the event title and store it in the Event object
                newEvent.eventDate = String.Format("{0:yyyy-MM-dd}", row["eventDate"]); //get the date and store it in the Event object and discard the time
                newEvent.eventStartTime = row["eventStartTime"].ToString(); //get the event start time and store it in the Event object
                newEvent.eventEndTime = row["eventEndTime"].ToString(); //get the event end time and store it in the Event object
                newEvent.eventContent = row["eventContent"].ToString(); //get the event content and store it in the Event object
                eventList.Add(newEvent); //add the Event object to a list of Event objects
            }

            Console.WriteLine("Done.");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}