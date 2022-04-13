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
        
        private readonly decimal payRate = 14;   // PayRate (per hour) used to calcuate pay from hours worked
                
        // Main Menu interface
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

            Console.Clear();
            Console.WriteLine(" Type MENU to return.");

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
                default:
                    Console.Write(" Invalid Entry.");
                    break;
            }

            Console.WriteLine("\n Press any key to return... ");
            Console.ReadKey();
        }
        
        // Get all shifts from API.
        private void GetShiftsInput()
        {
            Console.WriteLine("\n Displaying All Shift Records... ");
            
            service.GetShifts();
        }
        
        // Get one shift from API.
        private void GetShiftInput()
        {
            Console.WriteLine("Displaying a single Shift record... ");
            
            Console.Write("\n Please enter the ID of the Shift you want to view: ");
            string id = Validator.IsNumberValid(Console.ReadLine());
            if (id == "MENU") return;

            if (!service.CheckShiftId(id))
            {
                Console.WriteLine($"\n ID:{id} does not exist.");
            }
            else
            {
                Console.Clear();
                service.GetShift(id);
            }
        }
        // Create a shift.
        private void CreateShiftInput()
        {
            Shift shift = new Shift();
            
            Console.WriteLine("\n Creating a Shift... ");
            
            Console.Write("\n Start Time DD/MM/YY HH:MM -: ");
            string start = Validator.IsDateValid(Console.ReadLine());
            if(start == "MENU")  return; 
            shift.Start = DateTime.Parse(start);

            Console.Write("\n End Time DD/MM/YY HH:MM -: ");
            string end = Validator.IsDateValid(Console.ReadLine());
            if (end == "MENU")  return; 
            shift.End = DateTime.Parse(end);

            CalculateMinutesAndPay(shift);
            if (shift.Minutes == 0) return;

            Console.Write("\n Location: ");
            string location = Validator.IsStringValid(Console.ReadLine());
            if (location == "MENU")  return; 
            shift.Location = location;

            service.CreateShift(shift);
        }
        // Update a shift.
        private void UpdateShiftInput()
        {
            Shift shift = new Shift();

            Console.WriteLine("\n Updating a Shift...");
            service.GetShifts();

            Console.Write("\n Enter the ID of the Shift record you want to change: ");
            string id = Validator.IsNumberValid(Console.ReadLine());
            if (id == "MENU")  return; 

            shift.ShiftId = Int32.Parse(id);

            Console.Clear();
            
            // Check if the ID is valid or null.
            if (!service.CheckShiftId(shift.ShiftId.ToString()))
            {
                Console.WriteLine($"\n ID:{id} does not exist.");
            }
            else
            {
                service.GetShift(id); 
                
                Console.Write("\n Start Time DD/MM/YY HH:MM -: ");
                string start = Validator.IsDateValid(Console.ReadLine());
                if (start == "MENU") return;
                shift.Start = DateTime.Parse(start);

                Console.Write("\n End Time DD/MM/YY HH:MM -: ");
                string end = Validator.IsDateValid(Console.ReadLine());
                if (end == "MENU") return;
                shift.End = DateTime.Parse(end);

                CalculateMinutesAndPay(shift);
                if (shift.Minutes == 0) return;

                Console.Write("\n Location: ");
                string location = Validator.IsStringValid(Console.ReadLine());
                if (location == "MENU") return;
                shift.Location = location;

                service.UpdateShift(shift);
            }
        }
        
        // Delete a shift.
        private void DeleteShiftInput()
        {
            Shift shift = new Shift();
            
            Console.WriteLine("Deleting a Shift record... ");
            service.GetShifts();
            
            Console.Write("\n Type the ID of the record you want to delete: ");
            string id = Validator.IsNumberValid(Console.ReadLine());
            if (id == "MENU")  return; 
            shift.ShiftId = Int32.Parse(id);

            Console.Clear();
            
            // Check if the ID is valid or null.
            if (!service.CheckShiftId(id))
            {
                Console.WriteLine($"\n ID:{id} does not exist.");
            }
            else
            {
                service.GetShift(id);
                
                Console.Write($"\n Are you sure you wish to delete Shift ID: {shift.ShiftId} ? (y or n)");
                string entry = Console.ReadLine();
                if (entry != "y") return;

                service.DeleteShift(shift);
            }
        }

        private void CalculateMinutesAndPay(Shift shift)
        {
            if (shift.End < shift.Start)
            {
                Console.WriteLine("\n Invalid Timespan between dates provided. Press any key to return... ");
                Console.ReadKey();
                return;
            }
            double minutes = (shift.End - shift.Start).TotalMinutes;

            shift.Minutes = (decimal)minutes;

            shift.Pay = shift.Minutes * (payRate / 60);
        }
    }
}
