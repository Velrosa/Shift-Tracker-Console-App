using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;

namespace Shift_Tracker_Console_App
{
    internal class TableVisuals
    {
        public static void ShowTable<T>(List<T> tableData) where T : class
        {
            try
            {
                ConsoleTableBuilder.From(tableData).ExportAndWriteLine();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("\n No Records to display. \n");
            }

        }
    }
}
