using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using RestSharp;
using Shift_Tracker_Console_App.Models;
using System.Text.Json;
using Newtonsoft.Json;

namespace Shift_Tracker_Console_App
{
    internal class ShiftService
    {
        internal void GetShifts()
        {
            var client = new RestClient("http://localhost:26214/api/Shifts");
            var request = new RestRequest();
            try
            {
                var response = client.GetAsync<List<Shift>>(request);

                List<Shift> shifts = response.Result;

                TableVisuals.ShowTable(shifts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal bool GetShift(string id)
        {
            var client = new RestClient("http://localhost:26214/api/Shifts");
            var request = new RestRequest($"/{id}");
            try
            {
                var response = client.GetAsync<Shift>(request);

                List<Shift> shifts = new List<Shift>();
                if (response.Result != null)
                {
                    shifts.Add(response.Result);
                    TableVisuals.ShowTable(shifts);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        internal void CreateShift(Shift shift)
        {
            var client = new RestClient("http://localhost:26214/api/Shifts");
            var request = new RestRequest().AddJsonBody(shift);
            try
            {
                client.PostAsync<Shift>(request);
                Console.WriteLine($"Shift Record sucessfully created.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        internal void UpdateShift(Shift shift)
        {
            var client = new RestClient("http://localhost:26214/api/Shifts");
            var request = new RestRequest($"?id={shift.ShiftId}").AddJsonBody(shift);
            try
            {
                client.PutAsync<Shift>(request);
                Console.WriteLine($"Shift Record: {shift.ShiftId} sucessfully updated.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        internal void DeleteShift(Shift shift)
        {
            var client = new RestClient("http://localhost:26214/api/Shifts");
            var request = new RestRequest($"?id={shift.ShiftId}");
            try
            {
                client.DeleteAsync<Shift>(request);
                Console.WriteLine($"Shift Record: {shift.ShiftId} sucessfully deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}


//List<Shift> delist = JsonConvert.DeserializeObject<List<Shift>>(response.Result.Content);

//List<Shift> shifts = JsonSerializer.DeserializeAsync<List<Shift>>(response.Result);
