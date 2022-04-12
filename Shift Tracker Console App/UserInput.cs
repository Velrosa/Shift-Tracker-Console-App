using Shift_Tracker_Console_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shift_Tracker_Console_App
{
    internal class UserInput
    {
        ShiftService service = new ShiftService();
        
        private const decimal payRate = 14;
        
        internal void MainMenu()
        {
            Console.Clear();

            Console.WriteLine("\n MAIN MENU\n\n" +
                                " What would you like to do?\n\n" +
                                " Type 0 to Close Application.\n" +
                                " Type 1 to Get all Shift records.\n" +
                                " Type 2 to Get a specific Shift Record.\n" +
                                " Type 3 to Create a Shift record.\n" +
                                " Type 4 to Update a Shift record.\n" +
                                " Type 5 to Delete a Shift record.\n");

            string selector = Convert.ToString(Console.ReadKey(true).KeyChar);

            switch (selector)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    GetShiftsInput();
                    break;
                case "2":
                    GetShiftInput();
                    break;
                case "3":
                    CreateShiftInput();
                    break;
                case "4":
                    UpdateShiftInput();
                    break;
                case "5":
                    DeleteShiftInput();
                    break;
            }
        }
        private void GetShiftsInput()
        {
            Console.Clear();
            Console.WriteLine("\n Displaying All Shift Records... ");
            
            service.GetShifts();
            
            Console.WriteLine("\n Press any key to return... ");
            Console.ReadKey();
        }
        private void GetShiftInput()
        {
            Console.Clear();
            Console.Write("\n Please enter the ID of the Shift you want to view: ");
            string id = Console.ReadLine();

            while (!Validator.IsNumberValid(id))
            {
                Console.Write("Invalid number. Please Try again: ");
                id = Console.ReadLine();
            }

            if (!service.GetShift(id))
            {
                Console.WriteLine("Invalid ID chosen. Press any key to return... ");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("\n Press any key to return... ");
                Console.ReadKey();
            }
        }
        private void CreateShiftInput()
        {
            Shift shift = new Shift();
            
            Console.Clear();
            Console.WriteLine("\n Creating a Shift... ");
            
            Console.Write("\n Start Time DD/MM/YY HH:MM -: ");
            string start = Console.ReadLine();

            while (!Validator.IsDateValid(start))
            {
                Console.Write("Invalid Date. Please Try Again: ");
                start = Console.ReadLine();
            }

            shift.Start = DateTime.Parse(start);

            Console.Write("\n End Time DD/MM/YY HH:MM -: ");
            string end = Console.ReadLine();

            while (!Validator.IsDateValid(end))
            {
                Console.Write("Invalid Date. Please Try Again: ");
                end = Console.ReadLine();
            }

            shift.End = DateTime.Parse(end);

            Console.Write("\n Location: ");
            string location = Console.ReadLine();

            while (!Validator.IsStringValid(location))
            {
                Console.Write("Invalid Entry. Please Try Again: ");
                location = Console.ReadLine();
            }

            shift.Location = location;

            double minutes = (shift.End - shift.Start).TotalMinutes;

            shift.Minutes = (decimal)minutes;

            shift.Pay = shift.Minutes * (payRate/60);

            service.CreateShift(shift);

            Console.WriteLine("\n Press any key to return.");
            Console.ReadKey();
        }
        private void UpdateShiftInput()
        {
            Shift shift = new Shift();

            Console.Clear();            
            service.GetShifts();
            
            Console.WriteLine("\n Updating a Shift... ");

            Console.Write("\n Enter the ID of the Shift record you want to change: ");
            string id = Console.ReadLine();

            while (!Validator.IsNumberValid(id))
            {
                Console.Write("Invalid number. Please Try again: ");
                id = Console.ReadLine();
            }

            shift.ShiftId = Int32.Parse(id);

            Console.Clear();
            if (!service.GetShift(shift.ShiftId.ToString()))
            {
                Console.WriteLine("Invalid ID chosen. Press any key to return... ");
                Console.ReadKey();
                return;
            }
            
            Console.Write("\n Start Time DD/MM/YY HH:MM -: ");
            string start = Console.ReadLine();

            while (!Validator.IsDateValid(start))
            {
                Console.Write("Invalid Date. Please Try Again: ");
                start = Console.ReadLine();
            }
            
            shift.Start = DateTime.Parse(start);

            Console.Write("\n End Time DD/MM/YY HH:MM -: ");
            string end = Console.ReadLine();

            while (!Validator.IsDateValid(end))
            {
                Console.Write("Invalid Date. Please Try Again: ");
                end = Console.ReadLine();
            }

            shift.End = DateTime.Parse(end);

            Console.Write("\n Location: ");
            string location = Console.ReadLine();

            while (!Validator.IsStringValid(location))
            {
                Console.Write("Invalid Entry. Please Try Again: ");
                location = Console.ReadLine();
            }

            shift.Location = location;

            double minutes = (shift.End - shift.Start).TotalMinutes;

            shift.Minutes = (decimal)minutes;

            shift.Pay = shift.Minutes * (payRate / 60);

            Console.WriteLine(shift.ShiftId);

            service.UpdateShift(shift);

            Console.WriteLine("\n Press any key to return.");
            Console.ReadKey();
        }
        private void DeleteShiftInput()
        {
            Shift shift = new Shift();
            
            Console.Clear();

            service.GetShifts();
            Console.Write("\n Type the ID of the record you want to delete: ");
            string id = Console.ReadLine();

            while (!Validator.IsNumberValid(id))
            {
                Console.Write("Invalid number. Please Try again: ");
                id = Console.ReadLine();
            }

            shift.ShiftId = Int32.Parse(id);

            Console.Clear();
            
            if (!service.GetShift(id))
            {
                Console.WriteLine("Invalid ID chosen. Press any key to return... ");
                Console.ReadKey();
                return;
            }

            Console.Write($"\n Are you sure you wish to delete Shift ID: {shift.ShiftId} ? (y or n)");
            string entry = Console.ReadLine();
            if (entry != "y") { return; }

            service.DeleteShift(shift);

            Console.WriteLine("\n Press any key to return.");
            Console.ReadKey();
        }
    }
}
