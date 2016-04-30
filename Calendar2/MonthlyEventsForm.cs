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
    public partial class MonthlyEventsForm : Form
    {
        ArrayList monthlyEventList = new ArrayList(); //list to hold all events in the selected month

        //constructor for monthly events form
        public MonthlyEventsForm(int m, int y)
        {
            InitializeComponent();

            monthlyEventList.Clear(); //clear the list before beginning
            Event aEvent = new Event(); //create a new event to use its methods
            monthlyEventList = aEvent.getMonthlyEventList( m, y); //get a list off all events for the current month

            listBox1.Items.Clear(); //clear the listbox(incase it isn't empty) before adding to it

            //iterate through events 
            foreach (Event nextEvent in monthlyEventList)
            {
                string eventIndex = nextEvent.getStartTime() + "     " + nextEvent.getTitle(); //make a string containing start time and title
                listBox1.Items.Add(eventIndex); //add the string to the listbox
            }
        }

        private void MonthlyEventsForm_Load(object sender, EventArgs e)
        {

            
        }

        //close button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); //hide the form
        }
    }
}
