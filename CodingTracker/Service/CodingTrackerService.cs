using CodingTracker.Database;
using CodingTracker.Models;
using Spectre.Console;

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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured during the Insertion {ex.Message}");
            }
        }

        public void GetCodingSessions()
        {
            try
            {
                var sessionList = _trackerDB.GetCodingSessions();
                var table = new Table();
                table.Title("[yellow]Habits [/]");
                table.Border(TableBorder.Rounded);
                table.AddColumn(new TableColumn("[bold]ID[/]").Centered());
                table.AddColumn(new TableColumn("[bold]StartTime[/]").Centered());
                table.AddColumn(new TableColumn("[bold]EndTime[/]").Centered());
                table.AddColumn(new TableColumn("[bold]Date[/]").Centered());
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
                Console.WriteLine($"An error occured during the retrieval of coding sessions {ex.Message}");
            }
        }

        public void UpdateCodingSession()
        {
            try
            {
                int id = _userValidation.ValidateUserInputId();
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
                    Console.WriteLine($"Coding session with ID {id} updated successfully.");
                }
                else
                {
                    Console.WriteLine($"No coding session found with ID {id}.");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured during the update of coding sessions {ex.Message}");
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
                    Console.WriteLine($"Coding session with ID {id} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"No coding session found with ID {id}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured during the deletion of coding sessions {ex.Message}");
            }
        }
    }
}
