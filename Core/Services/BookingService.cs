// Services/BookingService.cs
using PoputkeeLite.Core.Models;
using PoputkeeLite.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoputkeeLite.Core.Services
{
    public class BookingService
    {
        public List<Booking> GetAllBookings()
        {
            var bookings = new List<Booking>();
            var lines = DataService.ReadAllLines(DataService.BookingsFilePath);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                bookings.Add(new Booking
                {
                    TripId = int.Parse(parts[0]),
                    PassengerLogin = parts[1],
                    Status = parts[2]
                });
            }
            return bookings;
        }

        public List<Booking> GetUserBookings(string login)
        {
            return GetAllBookings()
                .Where(b => b.PassengerLogin == login)
                .ToList();
        }

        public void CancelBooking(int tripId, string passengerLogin)
        {
            var bookings = GetAllBookings();
            var booking = bookings.FirstOrDefault(b =>
                b.TripId == tripId &&
                b.PassengerLogin == passengerLogin &&
                b.Status == "Active");

            if (booking != null)
            {
                booking.Status = "Canceled";
                SaveAllBookings(bookings);

                // Возвращаем место в поездке
                var tripService = new TripService();
                var trip = tripService.GetAllTrips().FirstOrDefault(t => t.Id == tripId);
                if (trip != null)
                {
                    trip.SeatsAvailable++;
                    tripService.SaveAllTrips(tripService.GetAllTrips());
                }
            }
        }

        public void ArchiveCompletedTrips()
        {
            var trips = new TripService().GetAllTrips();
            var bookings = GetAllBookings();
            var today = DateTime.Today;

            foreach (var booking in bookings.Where(b => b.Status == "Active"))
            {
                var trip = trips.FirstOrDefault(t => t.Id == booking.TripId);
                if (trip != null && trip.Date < today)
                {
                    booking.Status = "Completed";
                }
            }

            SaveAllBookings(bookings);
        }

        private void SaveAllBookings(List<Booking> bookings)
        {
            var lines = bookings.Select(b => $"{b.TripId}|{b.PassengerLogin}|{b.Status}");
            DataService.WriteAllLines(DataService.BookingsFilePath, lines.ToList());
        }
    }
}