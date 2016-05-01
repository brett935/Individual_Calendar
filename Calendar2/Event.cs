using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar2
{
    class Event
    {
        string eventTitle;
        string eventDate;
        string eventStartTime;
        string eventEndTime;
        string eventContent;

        public Event() { }

        public string getDate() {
            return eventDate;
        }
        public string getTitle() {
            return eventTitle;
        }
        public string getStartTime() {
            return eventStartTime;
        }
        public string getEndTime() {
            return eventEndTime;
        }
        public string getContent() {
            return eventContent;
        }

        //returns list of event objects for selected day
        public ArrayList getEventList(string dateString) {
            ArrayList eventList = new ArrayList(); //create array list to hold events
            DataTable myTable = new DataTable(); //create a data table to store results of sql query
            string connStr = "server=brettnapier.com;user=csc340Individual;database=csc340IndividualProject;port=3306;password=cscproject;";
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
            foreach (DataRow row in myTable.Rows) {
                Event newEvent = new Event(); //create a new Event object for each row in the table
                newEvent.eventTitle = row["eventTitle"].ToString(); //get the event title and store it in the Event object
                newEvent.eventDate = String.Format("{0:yyyy-MM-dd}", row["eventDate"]); //get the date and store it in the Event object and discard the time
                newEvent.eventStartTime = row["eventStartTime"].ToString(); //get the event start time and store it in the Event object
                newEvent.eventEndTime = row["eventEndTime"].ToString(); //get the event end time and store it in the Event object
                newEvent.eventContent = row["eventContent"].ToString(); //get the event content and store it in the Event object
                eventList.Add(newEvent); //add the Event object to a list of Event objects
            }

            Console.WriteLine("Done.");

            return eventList; //return the list of Event objects

        }

        //returns list of event objects for selected month
        public ArrayList getMonthlyEventList(int m, int y) {
            ArrayList eventList = new ArrayList(); //create array list to hold events
            DataTable myTable = new DataTable(); //create a data table to store results of sql query
            string connStr = "server=brettnapier.com;user=csc340Individual;database=csc340IndividualProject;port=3306;password=cscproject;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            String ms = String.Format("{0:00}", m); //convert to two digit string
            String ys = String.Format("{0:00}", y); //convert to two digit string

            //attempt to make a connection to the SQL server
            try
            {

                Console.WriteLine("Connecting to MySQL...");

                conn.Open();

                //string sql = "SELECT * FROM Events WHERE eventDate LIKE '%-04-30' ORDER BY eventStartTime ASC";

                // ----------REPLACE THIS USING SQL PARAMATERS--------------------------------------------------------
                string sql = "SELECT * FROM Events WHERE eventDate LIKE '@y-@m-%' ORDER BY eventStartTime ASC";
                sql =  sql.Replace("@y", ys);
                sql =  sql.Replace("@m", ms);
                //--------------------the problem is that'@y-@m-%' is in single quotes and is treated as a literal----


                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("@y", ys); //add paramaters to query
                //cmd.Parameters.AddWithValue("@m", ms); //add paramaters to query
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
                newEvent.eventTitle = row["eventTitle"].ToString(); //get the event title and store it in the Event object
                newEvent.eventDate = String.Format("{0:yyyy-MM-dd}", row["eventDate"]); //get the date and store it in the Event object and discard the time
                newEvent.eventStartTime = row["eventStartTime"].ToString(); //get the event start time and store it in the Event object
                newEvent.eventEndTime = row["eventEndTime"].ToString(); //get the event end time and store it in the Event object
                newEvent.eventContent = row["eventContent"].ToString(); //get the event content and store it in the Event object
                eventList.Add(newEvent); //add the Event object to a list of Event objects
            }

            Console.WriteLine("Done.");

            return eventList;
        }

    }
}
