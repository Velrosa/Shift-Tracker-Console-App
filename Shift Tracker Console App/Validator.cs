using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shift_Tracker_Console_App
{
    internal class Validator
    {
        public static string IsNumberValid(string numberInput)
        {
            while (true)
            {
                bool isValid = true;
                
                if (numberInput == "MENU")
                {
                    return numberInput;
                }
                if (!Int32.TryParse(numberInput, out int number))
                {
                    Console.WriteLine($"\n \"{numberInput}\" is not a valid number.");
                    isValid = false;
                }
                if (!isValid)
                {
                    Console.Write("\n Please enter again: ");
                    numberInput = Console.ReadLine();
                }
                else break;
            }
            return numberInput;
        }

        public static string IsStringValid(string stringInput)
        {
            while (true)
            {
                bool isValid = true;

                if (stringInput == "MENU")
                {
                    return stringInput;
                }
                if (String.IsNullOrEmpty(stringInput))
                {
                    Console.WriteLine("\n Empty input is not valid.");
                }
                foreach (char c in stringInput)
                {
                    if (!char.IsLetter(c) && c != ' ' && c != '/')
                    {
                        Console.WriteLine($" \"{c}\" is not a valid character.");
                        isValid = false;
                    }
                }
                if (!isValid)
                {
                    Console.Write("\n Please enter again: ");
                    stringInput = Console.ReadLine();
                }
                else break;
            }
            return stringInput;
        }

        public static string IsDateValid(string dateInput)
        {
            while (true)
            {
                bool isValid = true;

                if (dateInput == "MENU")
                {
                    return dateInput;
                }                
                if (!DateTime.TryParse(dateInput, out DateTime date))
                {
                    Console.WriteLine($"\n \"{dateInput}\" is not a valid Date.");
                    isValid = false;
                }
                if (!isValid)
                {
                    Console.Write("\n Please enter again: ");
                    dateInput = Console.ReadLine();
                }
                else break;
            }
            return dateInput;
        }
    }
}
