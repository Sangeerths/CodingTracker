using CodingTracker.Database;
using CodingTracker.Models;
using Spectre.Console;
using System.Diagnostics;

namespace CodingTracker.Service
{
    public class CodingTrackerService
    {
        private readonly TrackerDB _trackerDB;
        private readonly UserValidation _userValidation = new UserValidation();

        public CodingTrackerService(TrackerDB trackerDB)
        {
            _trackerDB = trackerDB;
        }

        public void CreateTables()
        {
            _trackerDB.Creation();
        }

        public void InsertCodingSession()
        {
            try
            {
                TimeSession times = (TimeSession)_userValidation.ValidateUserInputTime();
                _trackerDB.InsertCodingSession(times);
                AnsiConsole.MarkupLine("[green]Coding session inserted successfully![/]");
                

            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]An error occured during the Insertion {ex.Message}[/]");
            }
        }

        public void GetCodingSessions()
        {
            try
            {
                var sessionList = _trackerDB.GetCodingSessions();
                var table = new Table();
                table.Title("[yellow]Coding Sessions [/]");
                table.Border(TableBorder.Rounded);
                table.AddColumn(new TableColumn("[bold]ID[/]").Centered());
                table.AddColumn(new TableColumn("[bold]StartTime[/]").Centered());
                table.AddColumn(new TableColumn("[bold]EndTime[/]").Centered());
                table.AddColumn(new TableColumn("[bold]Duration(hrs)[/]").Centered());
                foreach (var list in sessionList)
                {
                    foreach (var session in list)
                    {
                        table.AddRow(
                            session.Id.ToString(),
                            session.startTime.ToString(" HH:mm:ss"),
                            session.endTime.ToString("HH:mm:ss"),
                            session.duration.ToString()
                        );
                    }
                }
                AnsiConsole.Write(table);


            }
            catch (Exception ex)
            {
               AnsiConsole.MarkupLine($"[red]An error occured during the retrieval of coding sessions {ex.Message}[/]");
            }
        }

        public void UpdateCodingSession()
        {
            try
            {
               
                int id = _userValidation.ValidateUserInputId();
                while (!_trackerDB.CodingSessionExists(id))
                {
                    AnsiConsole.MarkupLine($"[red]Invalid ID provided. Please enter a valid ID.[/]");
                    id = _userValidation.ValidateUserInputId();
                    Console.Clear();
                }
                TimeSession times = (TimeSession)_userValidation.ValidateUserInputTime();
                CodingSessionModel sessionModel = new CodingSessionModel
                {
                    Id = id.ToString(),
                    startTime = times.StartTime,
                    endTime = times.EndTime,
                    duration = times.Duration
                };
                bool success = _trackerDB.UpdateCodingSession(sessionModel);
                if (success)
                {
                   AnsiConsole.MarkupLine($"[green]Coding session with ID {id} updated successfully.[/]");
                }
                else
                {
                   AnsiConsole.MarkupLine($"[red]No coding session found with ID {id}.[/]");
                }


            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]An error occured during the update of coding sessions {ex.Message}[/]");
            }
        }

        public void DeleteCodingSession()
        {
            try
            {
                int id = _userValidation.ValidateUserInputId();
                bool success = _trackerDB.DeleteCodingSession(id);
                if (success)
                {
                    AnsiConsole.MarkupLine($"[green]Coding session with ID {id} deleted successfully.[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red]No coding session found with ID {id}.[/]");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]An error occured during the deletion of coding sessions {ex.Message}[/]");
            }
        }

        public void TimeCodingSession()
        {
            AnsiConsole.MarkupLine("[yellow]Press SPACEBAR to Start/Stop.[/]");
            AnsiConsole.MarkupLine("[blue]Press ESC to Exit.[/]");

            Stopwatch stopwatch = new Stopwatch();
            bool isRunning = false;
            TimeSession session = new TimeSession();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        if (!isRunning)
                        {
                            stopwatch.Restart();
                            session.StartTime = DateTime.Now;
                            isRunning = true;

                            AnsiConsole.MarkupLine("[green]Session Started.[/]");
                        }
                        else
                        {
                            stopwatch.Stop();
                            session.EndTime = DateTime.Now;
                            isRunning = false;

                            TimeSpan duration = session.EndTime - session.StartTime;

                            AnsiConsole.MarkupLine("[red]Session Stopped.[/]");
                            AnsiConsole.MarkupLine($"[cyan]Duration:[/] {duration:hh\\:mm\\:ss}");

                            

                            break;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        if (isRunning)
                        {
                            stopwatch.Stop();
                            session.EndTime = DateTime.Now;

                            TimeSpan duration = session.EndTime - session.StartTime;

                            AnsiConsole.MarkupLine("[yellow]Session cancelled.[/]");
                            AnsiConsole.MarkupLine($"Duration: {duration:hh\\:mm\\:ss}");
                            return;
                        }

                        break;
                    }
                }

                Thread.Sleep(50);
            }
            string choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("[yellow]Do you want to save this session?[/]")
                .AddChoices(new[] { "Yes", "No" }));
            if(choice == "Yes")
            {
                _trackerDB.InsertCodingSession(session);
                AnsiConsole.MarkupLine("[green]Coding session saved successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Coding session not saved.[/]");
            }
        }
    }
}
