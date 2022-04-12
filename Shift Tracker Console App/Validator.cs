using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shift_Tracker_Console_App
{
    internal class Validator
    {
        public static bool IsNumberValid(string numberInput)
        {
            if (String.IsNullOrEmpty(numberInput))
            {
                return false;
            }
            if (!Int32.TryParse(numberInput, out int number))
            {
                return false;
            }
            return true;
        }

        public static bool IsStringValid(string stringInput)
        {
            if (String.IsNullOrEmpty(stringInput))
            {
                return false;
            }
            foreach (char c in stringInput)
            {
                if(!char.IsLetter(c) && c != ' ' && c != '/') return false;
            }
            return true;
        }

        public static bool IsDateValid(string dateInput)
        {
            if (String.IsNullOrEmpty(dateInput))
            {
                return false;
            }
            if(!DateTime.TryParse(dateInput, out DateTime date))
            {
                return false;
            }
            return true;
        }
    }
}
