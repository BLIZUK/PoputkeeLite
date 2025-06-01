// Services/DataService.cs
using System;
using System.Collections.Generic;
using System.IO;

namespace PoputkeeLite.Core.Services
{
    public static class DataService
    {
        private static readonly string DataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        private static readonly string UsersPath = Path.Combine(DataFolder, "users.txt");
        private static readonly string TripsPath = Path.Combine(DataFolder, "trips.txt");
        private static readonly string BookingsPath = Path.Combine(DataFolder, "bookings.txt");

        public static void InitializeDataFiles()
        {
            Directory.CreateDirectory(DataFolder);
            if (!File.Exists(UsersPath)) File.Create(UsersPath).Close();
            if (!File.Exists(TripsPath)) File.Create(TripsPath).Close();
            if (!File.Exists(BookingsPath)) File.Create(BookingsPath).Close();
        }

        public static List<string> ReadAllLines(string filePath)
        {
            return new List<string>(File.ReadAllLines(filePath));
        }

        public static void AppendLine(string filePath, string data)
        {
            File.AppendAllText(filePath, data + Environment.NewLine);
        }

        public static void WriteAllLines(string filePath, List<string> data)
        {
            File.WriteAllLines(filePath, data);
        }

        public static string UsersFilePath => UsersPath;
        public static string TripsFilePath => TripsPath;
        public static string BookingsFilePath => BookingsPath;

        public static string CurrentDataFolder()
        {
            return DataFolder;
        }
    }
}