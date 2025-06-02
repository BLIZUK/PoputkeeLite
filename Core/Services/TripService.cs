// Services/TripService.cs
using PoputkeeLite.Core.Models;
using PoputkeeLite.Core.Services;
using PoputkeeLite.Desktop.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoputkeeLite.Core.Services
{
    public class TripService
    {
        public List<Trip> GetAllTrips()
        {
            var trips = new List<Trip>();
            var lines = DataService.ReadAllLines(DataService.TripsFilePath);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                trips.Add(new Trip
                {
                    Id = int.Parse(parts[0]),
                    DriverLogin = parts[1],
                    From = parts[2],
                    To = parts[3],
                    Date = DateTime.Parse(parts[4]),
                    Time = TimeSpan.Parse(parts[5]),
                    SeatsAvailable = int.Parse(parts[6])
                });
            }
            return trips;
        }

        public void AddTrip(Trip trip)
        {
            // Генерация ID
            var trips = GetAllTrips();
            int newId = trips.Count > 0 ? trips.Max(t => t.Id) + 1 : 1;
            trip.Id = newId;

            // Форматируем строку для записи
            string tripLine = $"{trip.Id}|{trip.DriverLogin}|{trip.From}|{trip.To}|{trip.Date:yyyy-MM-dd}|{trip.Time}|{trip.SeatsAvailable}";
            DataService.AppendLine(DataService.TripsFilePath, tripLine);
        }

        public void BookTrip(int tripId, string passengerLogin)
        {
            // Форматируем строку бронирования
            string bookingLine = $"{tripId}|{passengerLogin}|Active";
            DataService.AppendLine(DataService.BookingsFilePath, bookingLine);

            // Уменьшаем количество доступных мест
            var trips = GetAllTrips();
            var trip = trips.FirstOrDefault(t => t.Id == tripId);
            if (trip != null && trip.SeatsAvailable > 0)
            {
                trip.SeatsAvailable--;
                SaveAllTrips(trips);
            }
        }

        public void SaveAllTrips(List<Trip> trips)
        {
            var lines = trips.Select(t => $"{t.Id}|{t.DriverLogin}|{t.From}|{t.To}|{t.Date:yyyy-MM-dd}|{t.Time}|{t.SeatsAvailable}");
            DataService.WriteAllLines(DataService.TripsFilePath, lines.ToList());
        }
    }
}