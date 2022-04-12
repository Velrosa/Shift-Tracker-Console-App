using System;

namespace Shift_Tracker_Console_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserInput userInput = new UserInput();
            while (true)
            {
                userInput.MainMenu();
            }
        }
    }
}
