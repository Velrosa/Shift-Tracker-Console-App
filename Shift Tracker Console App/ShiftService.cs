using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Shift_Tracker_Console_App.Models;
using System.Net;
using System.Configuration;

namespace Shift_Tracker_Console_App
{
    public class ShiftService : IShiftService
    {
        private readonly string apiUrl = ConfigurationManager.ConnectionStrings["apiUrl"].ConnectionString;

        // HTTP GET All the Shift records
        public void GetShifts()
        {
            using(var client = new RestClient(apiUrl))
            {
                var request = new RestRequest();

                var response = client.ExecuteGetAsync<List<Shift>>(request);
                if(response.Result.StatusCode == HttpStatusCode.OK)
                {
                    List<Shift> shifts = response.Result.Data;
                    TableVisuals.ShowTable(shifts);
                }
                else
                {
                    Console.WriteLine(response.Result.ErrorMessage);
                }
            }
        }

        // HTTP GET a single Shift record.
        public bool GetShift(string id)
        {
            using (var client = new RestClient(apiUrl))
            {
                var request = new RestRequest($"/{id}");
                
                var response = client.ExecuteGetAsync<Shift>(request);

                if(response.Result.StatusCode == HttpStatusCode.OK)
                {
                    List<Shift> shifts = new List<Shift>();
                    shifts.Add(response.Result.Data);

                    TableVisuals.ShowTable(shifts);
                    return true;
                }
                else if(response.Result.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine($" Shift ID:{id}, was not found.");
                    return false;
                }
                else
                {
                    Console.WriteLine(response.Result.ErrorMessage);
                    return false;
                }
            }
        }

        // HTTP POST a Shift record.
        public void CreateShift(Shift shift)
        {
            using (var client = new RestClient(apiUrl))
            {
                var request = new RestRequest().AddJsonBody(shift);

                var response = client.ExecutePostAsync<Shift>(request);
                
                if(response.Result.StatusCode == HttpStatusCode.Created)
                {
                    List<Shift> shifts = new List<Shift>();
                    shifts.Add(response.Result.Data);

                    Console.Clear();
                    TableVisuals.ShowTable(shifts);
                    Console.WriteLine("\n Shift Record sucessfully created.");
                }
                else
                {
                    Console.WriteLine(response.Result.ErrorMessage);
                }
            }
        }

        // HTTP PUT a Shift record.
        public void UpdateShift(Shift shift)
        {
            using (var client = new RestClient(apiUrl))
            {
                var request = new RestRequest($"?id={shift.ShiftId}").AddJsonBody(shift);

                var response = client.ExecutePutAsync<Shift>(request);

                if(response.Result.StatusCode == HttpStatusCode.NoContent)
                {
                    Console.WriteLine($"\n Shift Record: {shift.ShiftId} sucessfully updated.");
                }
                else if(response.Result.StatusCode == HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"\n Invalid ID's Provided, {shift.ShiftId} has not been updated.");
                }
                else
                {
                    Console.WriteLine(response.Result.ErrorMessage);
                }
            }
        }

        // HTTP DELETE a Shift record.
        public void DeleteShift(Shift shift)
        {
            using (var client = new RestClient(apiUrl))
            {
                var request = new RestRequest($"?id={shift.ShiftId}");

                var response = client.DeleteAsync(request);

                if (response.Result.StatusCode == HttpStatusCode.NoContent)
                {
                    Console.WriteLine($"\n Shift Record: {shift.ShiftId} sucessfully deleted.");
                }
                else if (response.Result.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"\n No record with the ID:{shift.ShiftId} could be found.");
                }
                else
                {
                    Console.WriteLine(response.Result.ErrorMessage);
                }
            }
        }
    }
}
