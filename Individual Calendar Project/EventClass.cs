using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Calendar_Project
{

    class EventClass
    {

        //constructor
        public EventClass() {
        }
        ~EventClass() {
        }

        string eventTitle;
        string eventDate;
        string eventDetails;
        string eventID;
        const string sqlServerAddress = "brettnapier.com;";
        const string sqlUserName = "";


        //adds event to users calendar
        public void addEvent(string eventTitle, string userID, string eventDate, string eventTime) {
            //Remember to add MySql.Data as a reference into your project.


            string connStr = "server=brettnapier.com;user=csc340Individual;database=csc340IndividualProject;port=3306;password=cscproject;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {

                Console.WriteLine("Connecting to MySQL...");

                conn.Open();

                string sql = "INSERT INTO Events (eventTitle, Users_userID, eventDate, eventTime) VALUES ('" + eventTitle + "','" + userID + "','" + eventDate + "','" + eventTime + "')";

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());

            }

            conn.Close();

            Console.WriteLine("Done.");

        }
        public void saveEvent() {

        }
        public void editEvent(string eventTitle, string eventID, string eventDate, string eventTime) {
            string connStr = "server=brettnapier.com;user=csc340Individual;database=csc340IndividualProject;port=3306;password=cscproject;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {

                Console.WriteLine("Connecting to MySQL...");

                conn.Open();

                string sql = "UPDATE Events SET eventTitle='" + eventTitle + "', eventDate='" + eventDate +"', eventTime='" + eventTime + "' WHERE eventID='" + eventID + "'";

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());

            }

            conn.Close();

            Console.WriteLine("Done.");
        }
        //deletes an event from users calendar, eventID is the ID of the event to delete
        public void deleteEvent(string eventID) {

            string connStr = "server=brettnapier.com;user=csc340Individual;database=csc340IndividualProject;port=3306;password=cscproject;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {

                Console.WriteLine("Connecting to MySQL...");

                conn.Open();

                string sql = "DELETE FROM Events WHERE eventID='" + eventID +"'";

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());

            }

            conn.Close();

            Console.WriteLine("Done.");
        }
        public void viewEvent() { }
        public void viewEventList() {
            string connStr = "server=brettnapier.com;user=csc340Individual;database=csc340IndividualProject;port=3306;password=cscproject;";

            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            try
            {

                Console.WriteLine("Connecting to MySQL...");

                conn.Open();

                string sql = "SELECT * FROM Events WHERE eventDate='" + eventDate +"'";

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());

            }

            conn.Close();

            Console.WriteLine("Done.");
        }

    }
}
