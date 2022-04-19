using Shift_Tracker_Console_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shift_Tracker_Console_App
{
    public interface IShiftService
    {
        void GetShifts();
        bool GetShift(string id);
        void CreateShift(Shift shift);
        void UpdateShift(Shift shift);
        void DeleteShift(Shift shift);
    }
}
