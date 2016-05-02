using System;
using System.Collections;
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
    public partial class Form1 : Form
    {
        ArrayList eventList = new ArrayList(); //list to hold all events in the selected day
        string userID; //holds the userID for the current calendar

        public Form1(string ID)
        {
            InitializeComponent();

            userID = ID; //set the userID for the form upon creation
        } //class constructor

        //runs when the form is loaded, loads the events for the current day
        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime thisDay = DateTime.Today; //get current date and time
            string selectedDate = String.Format("{0:yyyy-MM-dd}", thisDay); //convert the date and time to a string containing the date
            

            eventList.Clear(); //clear the list before beginning

            Event aEvent = new Event(); //create a new event to use its methods
            eventList = aEvent.getEventList(selectedDate); //get a list off all events for the current day
            listBox1.Items.Clear(); //clear the listbox(incase it isn't empty) before adding to it
            
            //iterate through events 
            foreach (Event nextEvent in eventList) {
                string eventIndex = nextEvent.getStartTime() +  "     " + nextEvent.getTitle(); //make a string containing start time and title
                listBox1.Items.Add(eventIndex); //add the string to the listbox
                
            }
            try
            {
                //update boxes with first event of the day
                Event thisEvent = eventList[0] as Event; //get first event from the event list and treat it as an Event
                textBox1.Text = thisEvent.getTitle(); //fill title box with the event title
                textBox2.Text = thisEvent.getDate(); //fill date box with the event date
                textBox3.Text = thisEvent.getStartTime(); //fill start time box with the event start time
                textBox4.Text = thisEvent.getEndTime(); //fill end time box with the event end time
                richTextBox1.Text = thisEvent.getContent(); //fill content box with the event content
            }
            catch (Exception el) { Console.Write(el); }
        }

        //add event form button
        private void button3_Click(object sender, EventArgs e)
        {
            //string sql = "INSERT INTO Events (eventTitle, Users_userID, eventDate, eventTime) VALUES ('" + eventTitle + "','" + userID + "','" + eventDate + "','" + eventTime + "')"; //may be wrong     

            DateTime selectedDay = monthCalendar1.SelectionRange.Start; //get selected 
            string myString = String.Format("{0:yyyy-MM-dd}", selectedDay); //convert the date and time to a string containing the 

            Form addEventForm = new AddEventForm( myString ); //create a new add event form
            addEventForm.Show(); //display the add event form
        }

        //called when a different date is selected on the monthly calendar
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDay = monthCalendar1.SelectionRange.Start; //get selected date
            string selectedDate = String.Format("{0:yyyy-MM-dd}", selectedDay); //convert the date and time to a string containing the date

            eventList.Clear(); //clear the event list

            Event aEvent = new Event(); //create a new event object
            eventList = aEvent.getEventList(selectedDate); //retrieve an list of events
            listBox1.Items.Clear(); //clear the listbox
            //iterate through the list of events
            foreach (Event nextEvent in eventList) {
                string eventIndex = nextEvent.getStartTime() + "     " + nextEvent.getTitle(); //make a string containing start time and title
                listBox1.Items.Add(eventIndex); //add the string to the listbox
            }
            //if there is no events in the list for the day
            if (eventList.Count == 0)
            {
                textBox1.Text = ""; //set the title to empty
                textBox2.Text = ""; //set the date to empty
                textBox3.Text = ""; //start time to empty
                textBox4.Text = ""; //end time to empty
                richTextBox1.Text = ""; //content to empty
                
            }
            //if there are one or more events
            else {
                Event thisEvent = eventList[0] as Event; //get first event from the event list and treat it as an Event
                textBox1.Text = thisEvent.getTitle(); //fill title box with the event title
                textBox2.Text = thisEvent.getDate(); //fill date box with the event date
                textBox3.Text = thisEvent.getStartTime(); //fill start time box with the event start time
                textBox4.Text = thisEvent.getEndTime(); //fill end time box with the event end time
                richTextBox1.Text = thisEvent.getContent(); //fill content box with the event content
            }
        }

        //called when clicking an event in the event list box, updates event detail boxes
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex; //index of selected event from listbox
            Event thisEvent = eventList[selectedIndex] as Event; //treat the item in the event list as a Event
            
            //populate textboxes with the event information
            textBox1.Text = thisEvent.getTitle();
            textBox2.Text = thisEvent.getDate();
            textBox3.Text = thisEvent.getStartTime();
            textBox4.Text = thisEvent.getEndTime();
            richTextBox1.Text = thisEvent.getContent();
        }

        //display monthly events button
        private void button5_Click(object sender, EventArgs e)
        {
            DateTime selectedDay = monthCalendar1.SelectionRange.Start; //get selected date
            int m = selectedDay.Month; //get month component from selected date
            int y = selectedDay.Year; //get year component from selected date

            Form monthlyEventsForm = new MonthlyEventsForm( m, y ); //create a new monthly event form
            monthlyEventsForm.Show(); //display the new monthly event form
        }

        //delete event button
        private void button4_Click(object sender, EventArgs e)
        {

            int selectedIndex = listBox1.SelectedIndex; //index of selected event from listbox

            //if an index has not been selected than selectedIndex will be negative
            //only try to delete the event if it is non-negative
            if(selectedIndex >= 0){
                Event thisEvent = eventList[selectedIndex] as Event; //treat the item in the event list as a Event

                var confirmResult = MessageBox.Show("Are you sure you want to delete this event?",
                                         "Confirm Delete!",
                                         MessageBoxButtons.YesNo);


                //check confirmation message result before deleting
                if (confirmResult == DialogResult.Yes)
                {
                    //perform SQL actions
                    string connStr = "server=brettnapier.com;user=csc340Individual;database=csc340IndividualProject;port=3306;password=cscproject;";
                    MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

                    //attempt to make a connection to the SQL server
                    try
                    {

                        Console.WriteLine("Connecting to MySQL...");

                        conn.Open();

                        string sql = "DELETE FROM Events WHERE eventID=@ID;";

                        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@ID", thisEvent.getID()); //add paramaters to query
                        cmd.ExecuteNonQuery(); //execute the sql commands

                        Console.WriteLine("Deleted the event.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    conn.Close();

                }
            }
            
            
        }

        //save changes on edit button
        private void button2_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text; //get the value from the title box
            string date = textBox2.Text; //get the value from the date box
            string startTime = textBox3.Text; //get the value from the start time box
            string endTime = textBox4.Text; //get the value from the ending time box
            string content = richTextBox1.Text; //get the value from the content box

            int selectedIndex = listBox1.SelectedIndex; //index of selected event from listbox
            Event thisEvent = eventList[selectedIndex] as Event; //treat the item in the event list as a Event


            //perform SQL actions
            string connStr = "server=brettnapier.com;user=csc340Individual;database=csc340IndividualProject;port=3306;password=cscproject;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            //attempt to make a connection to the SQL server
            try
            {

                Console.WriteLine("Connecting to MySQL...");

                conn.Open();

                string sql = "UPDATE Events SET eventTitle=@title, eventDate=@date, eventStartTime=@startTime, eventEndTime=@endTime, eventContent=@content  WHERE eventID=@ID;";
               
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", thisEvent.getID() ); //add paramaters to query
                cmd.Parameters.AddWithValue("@title", title); //add paramaters to query
                cmd.Parameters.AddWithValue("@date", date); //add paramaters to query
                cmd.Parameters.AddWithValue("@startTime", startTime); //add paramaters to query
                cmd.Parameters.AddWithValue("@endTime", endTime); //add paramaters to query
                cmd.Parameters.AddWithValue("@content", content); //add paramaters to query
                cmd.ExecuteNonQuery(); //execute the sql commands

                Console.WriteLine("Done updating event");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
        }

    } 

    
}
