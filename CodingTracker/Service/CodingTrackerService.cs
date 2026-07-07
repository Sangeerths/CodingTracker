using CodingTracker.Database;
using CodingTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.Service
{
    internal class CodingTrackerService
    {
        private readonly TrackerDB _trackerDB;

        public CodingTrackerService(TrackerDB trackerDB)
        {
            _trackerDB = trackerDB;
        }

        public void CreateTables ()
        {
            _trackerDB.Creation();
        }

        public void InsertCodingSession(CodingSessionModel request)
        {
           
        }
    }
}
