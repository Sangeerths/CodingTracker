using CodingTracker.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker.Database
{
    internal class TrackerDB
    {
        private readonly string _connectionString;

        public TrackerDB(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Creation()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"
        CREATE TABLE IF NOT EXISTS CodingSessions (
            Id INTEGER PRIMARY KEY AUTOINCREMENT, 
            StartTime TEXT, 
            EndTime TEXT, 
            Duration REAL
        );";
                connection.Open();
                connection.Execute(sql);

            }
        }

        public void InsertCodingSession(CodingSessionModel request)
        {
            string sql = @"INSERT into CodingSessions(StartTime, EndTime, Duration) VALUES (@startTime, @endTime, @duration);";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(sql, request);
            }
        }

        public IEnumerable<List<CodingSessionModel>> GetCodingSessions()
        {
            string sql = @"SELECT * FROM CodingSessions;";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = connection.Query<CodingSessionModel>(sql);
                return new List<List<CodingSessionModel>> { new List<CodingSessionModel>(result) };
            }
        }

        public bool DeleteCodingSession(int id)
        {
            string sql = @"DELETE FROM CodingSessions WHERE Id = @Id;";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                int rowsAffected = connection.Execute(sql, new { Id = id });
                return rowsAffected > 0;
            }
        }

        public bool UpdateCodingSession( CodingSessionModel request)
        {
            string sql = @"UPDATE CodingSessions SET StartTime = @startTime, EndTime = @endTime, Duration = @duration WHERE Id = @Id;";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                int rowsAffected = connection.Execute(sql, new { Id = request.Id, request.startTime, request.endTime, request.duration });
                return rowsAffected > 0;
            }
        }
    }
}
