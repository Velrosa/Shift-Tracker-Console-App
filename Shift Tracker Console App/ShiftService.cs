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
    internal class ShiftService
    {
        private readonly string _apiKey = ConfigurationManager.AppSettings.Get("conString");

        // HTTP GET All the Shift records
        internal void GetShifts()
        {
            using(var client = new RestClient(_apiKey))
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
        internal void GetShift(string id)
        {
            using (var client = new RestClient(_apiKey))
            {
                var request = new RestRequest($"/{id}");
                
                var response = client.ExecuteGetAsync<Shift>(request);

                if(response.Result.StatusCode == HttpStatusCode.OK)
                {
                    List<Shift> shifts = new List<Shift>();
                    shifts.Add(response.Result.Data);

                    TableVisuals.ShowTable(shifts);
                }
                else
                {
                    Console.WriteLine(response.Result.ErrorMessage);
                }
            }
        }

        // Returns true or false if the ID provided belongs to a valid record.
        internal bool CheckShiftId(string id)
        {
            using (var client = new RestClient(_apiKey))
            {
                var request = new RestRequest($"/{id}");

                var response = client.ExecuteGetAsync<Shift>(request);

                if(response.Result.StatusCode == HttpStatusCode.OK)
                {
                    if (response.Result.Data == null) return false;
                    return true;
                }
                else
                {
                    Console.Write(response.Result.ErrorMessage);
                    return false;
                }
            }
        }

        // HTTP POST a Shift record.
        internal void CreateShift(Shift shift)
        {
            using (var client = new RestClient(_apiKey))
            {
                var request = new RestRequest().AddJsonBody(shift);

                var response = client.ExecutePostAsync<Shift>(request);

                if(response.Result.StatusCode == HttpStatusCode.Created)
                {
                    Console.WriteLine($" Shift Record sucessfully created.");
                }
                else
                {
                    Console.WriteLine(response.Result.ErrorMessage);
                }
            }
        }

        // HTTP PUT a Shift record.
        internal void UpdateShift(Shift shift)
        {
            using (var client = new RestClient(_apiKey))
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
        internal void DeleteShift(Shift shift)
        {
            using (var client = new RestClient(_apiKey))
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
