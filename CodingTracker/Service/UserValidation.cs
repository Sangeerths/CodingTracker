using CodingTracker.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CodingTracker.Service
{
    public class UserValidation
    {
        public TimeSession ValidateUserInputTime()
        {
            bool isValid = false;

            while (!isValid)
            {

                AnsiConsole.MarkupLine("[Magenta]Enter the start Date (yyyy-MM-dd):[/]");
                StringBuilder startTimeInput = new StringBuilder();
                startTimeInput.Append(Console.ReadLine());
                startTimeInput.Append(" ");
                AnsiConsole.MarkupLine("[Magenta]Enter the Start Time (HH:mm:ss)[/]");
                startTimeInput.Append(Console.ReadLine());
                DateTime startTime;
                if (!DateTime.TryParseExact(startTimeInput.ToString(),"yyyy-MM-dd HH:mm:ss",CultureInfo.InvariantCulture, DateTimeStyles.None,  out startTime))
                {
                   AnsiConsole.MarkupLine("[red]Invalid start time format. Please try again.[/]");
                    continue;
                }
                AnsiConsole.MarkupLine("[Magenta]Enter the end Date (yyyy-MM-dd):[/]");
                StringBuilder endTimeInput = new StringBuilder();
                endTimeInput.Append(Console.ReadLine());
                endTimeInput.Append(" ");
                AnsiConsole.MarkupLine("[Magenta]Enter the end time (HH:mm:ss):[/]");
                endTimeInput.Append(Console.ReadLine());
                DateTime endTime;
                if (!DateTime.TryParseExact(endTimeInput.ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime))
                {
                   AnsiConsole.MarkupLine("[red]Invalid end time format. Please try again.[/]");
                    continue;
                }
                if (endTime <= startTime)
                {
                    AnsiConsole.MarkupLine("[red]End time must be after start time. Please try again.[/]");
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
                return timeSession;
            }
            return null;
        }

        public int ValidateUserInputId()
        {
            bool isValid = false;
            int result = 0;
            while (!isValid)
            {
                AnsiConsole.MarkupLine("[Magenta]Enter the ID[/]");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out result) || result <=0)
                {
                   AnsiConsole.MarkupLine("[red]Invalid. Please enter a valid integer.[/]");
                    continue;
                }
                isValid = true;
            }
            return result;
        }

       public DateTime ValidateDate()
        {
            bool isValid = false;
            DateTime final ;
            while(!isValid)
            {
                AnsiConsole.MarkupLine("[Magenta]Enter the date (yyyy-MM-dd):[/]");
                string input = Console.ReadLine();
                if (!DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out final))
                {
                    AnsiConsole.MarkupLine("[red]Invalid date format. Please try again.[/]");
                    continue;
                }
                isValid = true;
                return final;
            }
            return DateTime.MinValue;

        }
        public int ValidateMonth()
        {
            bool isValid = false;
            while (!isValid)
            {
                AnsiConsole.MarkupLine("[Magenta]Enter the month (1-12):[/]");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int month) || month < 1 || month > 12)
                {
                    AnsiConsole.MarkupLine("[red]Invalid month. Please enter a number between 1 and 12.[/]");
                    continue;
                }
                isValid = true;
                return month;
            }
            return 0;
        }

        public int ValidateYear()
        {
            bool isValid = false;
            while (!isValid)
            {
                AnsiConsole.MarkupLine("[Magenta]Enter the year (e.g., 2023):[/]");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int year) || year < 1)
                {
                    AnsiConsole.MarkupLine("[red]Invalid year. Please enter a valid positive integer.[/]");
                    continue;
                }
                isValid = true;
                return year;
            }
            return 0;
        }
    }
}