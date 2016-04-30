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
        string selectedDate = "";


        public Form1()
        {
            InitializeComponent();
        } //class constructor

        //runs when the form is loaded, loads the events for the current day
        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime thisDay = DateTime.Today; //get current date and time
            string myString = String.Format("{0:yyyy-MM-dd}", thisDay); //convert the date and time to a string containing the date
            

            eventList.Clear(); //clear the list before beginning
            //myString = convertDateFormat(myString);

            selectedDate = myString;

            Event aEvent = new Event(); //create a new event to use its methods
            eventList = aEvent.getEventList(myString); //get a list off all events for the current day
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

        //insert button (new button)
        private void button3_Click(object sender, EventArgs e)
        {
            //string sql = "INSERT INTO Events (eventTitle, Users_userID, eventDate, eventTime) VALUES ('" + eventTitle + "','" + userID + "','" + eventDate + "','" + eventTime + "')"; //may be wrong     
        }

        //called when a different date is selected on the monthly calendar
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDay = monthCalendar1.SelectionRange.Start; //get selected date
            string myString = String.Format("{0:yyyy-MM-dd}", selectedDay); //convert the date and time to a string containing the date

            eventList.Clear(); //clear the event list

            selectedDate = myString;

            Event aEvent = new Event(); //create a new event object
            eventList = aEvent.getEventList(myString); //retrieve an list of events
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
      

    } 

    
}
