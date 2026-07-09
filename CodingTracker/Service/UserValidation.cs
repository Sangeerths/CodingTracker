using CodingTracker.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

                Console.WriteLine("Enter the start Date (yyyy-MM-dd):");
                StringBuilder startTimeInput = new StringBuilder();
                startTimeInput.Append(Console.ReadLine());
                startTimeInput.Append(" ");
                Console.WriteLine("Enter the Start Time (HH:mm:ss)");
                startTimeInput.Append(Console.ReadLine());
                DateTime startTime;
                if (!DateTime.TryParseExact(startTimeInput.ToString(),"yyyy-MM-dd HH:mm:ss",CultureInfo.InvariantCulture, DateTimeStyles.None,  out startTime))
                {
                    Console.WriteLine("Invalid start time format. Please try again.");
                    continue;
                }
                Console.WriteLine("Enter the end Date (yyyy-MM-dd):");
                StringBuilder endTimeInput = new StringBuilder();
                endTimeInput.Append(Console.ReadLine());
                endTimeInput.Append(" ");
                Console.WriteLine("Enter the end time (HH:mm:ss):");
                endTimeInput.Append(Console.ReadLine());
                DateTime endTime;
                if (!DateTime.TryParseExact(endTimeInput.ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime))
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
                if (!int.TryParse(input, out result) || result <=0)
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