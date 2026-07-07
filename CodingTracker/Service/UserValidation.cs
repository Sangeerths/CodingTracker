using CodingTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.Service
{
    public class UserValidation
    {
        public IEnumerable<CodingSessionModel> ValidateUserInput(string operation)
        {
            bool isValid = false;

            while (!isValid)
            {
                if (operation == "Insert")
                {
                    Console.WriteLine("Enter the start time (yyyy-MM-dd HH:mm:ss):");
                    string startTimeInput = Console.ReadLine();
                    DateTime startTime;
                    if (!DateTime.TryParse(startTimeInput, out startTime))
                    {
                        Console.WriteLine("Invalid start time format. Please try again.");
                        continue;
                    }
                    Console.WriteLine("Enter the end time (yyyy-MM-dd HH:mm:ss):");
                    string endTimeInput = Console.ReadLine();
                    DateTime endTime;
                    if (!DateTime.TryParse(endTimeInput, out endTime))
                    {
                        Console.WriteLine("Invalid end time format. Please try again.");
                        continue;
                    }
                    if (endTime <= startTime)
                    {
                        Console.WriteLine("End time must be after start time. Please try again.");
                        continue;
                    }
                    float duration = (float)(endTime - startTime).TotalHours;
                    var codingSession = new CodingSessionModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        startTime = startTime,
                        endTime = endTime,
                        duration = duration
                    };
                    isValid = true;
                    return new List<CodingSessionModel> { codingSession };
                }

                else
                {
                    Console.WriteLine("Invalid operation. Please enter 'Insert' to add a coding session.");
                }
            }
            return null;
        }
    }
}