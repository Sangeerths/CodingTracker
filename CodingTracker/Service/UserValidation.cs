using CodingTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.Service
{
    public class UserValidation
    {
        public IEnumerable<TimeSession> ValidateUserInputTime()
        {
            bool isValid = false;

            while (!isValid)
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
                var timeSession = new TimeSession
                {

                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = duration
                };
                isValid = true;
                return new List<TimeSession> { timeSession };
            }
            return null;
        }

        public int ValidateUserInputId()
        {
            bool isValid = false;
            int result = 0;
            while (!isValid)
            {
                Console.WriteLine("Enter the ID");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out result))
                {
                    Console.WriteLine($"Invalid. Please enter a valid integer.");
                    continue;
                }
                isValid = true;
            }
            return result;
        }
    }
}