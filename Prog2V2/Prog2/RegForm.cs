//Name:    Jeremy Brown
//Date:    11/6/2011
//Class:   CIS 199-01
//Program: Program 3
//Purpose: The purpose of this program is to determine the date and time of registration for classes
//         for new and continuing students for the upcoming school semester using a set of arrays.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog2
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        private void findRegTimeBtn_Click(object sender, EventArgs e)
        {
            const float SENIOR_HOURS = 90;    // Min hours for Senior
            const float JUNIOR_HOURS = 60;    // Min hours for Junior
            const float SOPHOMORE_HOURS = 30; // Min hours for Soph.

            char[][] lastNameArray =  {          //Jagged array containing all of the last name ranges for the upper and lower class students that will be registering.
                new char[] { 'D', 'I', 'O', 'T', 'Z' },
                new char[] { 'B', 'D', 'F', 'I', 'L', 'O', 'R', 'T', 'V', 'Z' }};
            string[][] timeArray =  {           //Jagged array containg all of the time ranges for the upper and lower class students that will be registering.
                new string[] { "4:00 PM", "8:00 AM", "10:00 AM", "12:00 PM", "2:00 PM" },
                new string[] { "2:00 PM", "4:00 PM", "8:00 AM", "10:00 AM", "12:00 PM", "2:00 PM", "4:00 PM", "8:00 AM", "10:00 AM", "12:00 PM" }};
            int horizPosition = 0;//Counter that will help determine the horizontal position in the Jagged array that is needed to display the correct time for registering.
            int vertPosition = 0;//Counter that will help determine the vertical position in the Jagged array that is needed to display the correct time for registering.
            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            float creditHours;        // Entered credit hours
            
            creditHours = Convert.ToSingle(creditHrTxt.Text);
            lastNameStr = lastNameTxt.Text;
            lastNameStr = lastNameStr.ToUpper();
            lastNameLetterCh = lastNameStr[0];

            // Juniors and Seniors share same schedule but different days
            if (creditHours >= JUNIOR_HOURS)
            {
                if (creditHours >= SENIOR_HOURS)
                    dateStr = "November 7";
                else // Must be juniors
                    dateStr = "November 8";
                
                while  //While loop that keeps traveling through the array until the lastNameCh is less that the Ch that is in the horizontal pos. in the lastNameArray
                (horizPosition < lastNameArray[0].Length && lastNameLetterCh > lastNameArray[vertPosition][horizPosition])
                    ++horizPosition;

                timeStr = timeArray[vertPosition][horizPosition];
            }
            // Sophomores and Freshmen
            else // Must be soph/fresh
            {
                if (creditHours >= SOPHOMORE_HOURS)
                {
                    // E-R on one day
                    if ((lastNameLetterCh >= 'E') && // >= E and
                        (lastNameLetterCh <= 'R'))  // <= R
                        dateStr = "November 9";
                    else // All other letters on next day
                        dateStr = "November 10";
                }
                else // must be freshman
                {
                    // E-R on one day
                    if ((lastNameLetterCh >= 'E') && // >= E and
                        (lastNameLetterCh <= 'R'))  // <= R
                        dateStr = "November 11";
                    else // All other letters on next day
                        dateStr = "November 14";
                }
                vertPosition = 1;
                while  //While loop that keeps traveling through the array until the lastNameCh is less that the Ch that is in the horizontal pos. in the lastNameArray
      (horizPosition < lastNameArray[1].Length && lastNameLetterCh > lastNameArray[vertPosition][horizPosition])
                    ++horizPosition;

                timeStr = timeArray[vertPosition][horizPosition];
            }

            // This isn't necessary but it is easy to do
            // when you have the letter as a char
            if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                dateTimeLbl.Text = String.Format("{0} at {1}",
                    dateStr, timeStr);
            else
                dateTimeLbl.Text = "Invalid Last Name";
        }
    }
}