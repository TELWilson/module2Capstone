using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationDAO : IReserveDAO
    {
        private string connectionString;

        
        private string sqlGetReservations = "SELECT * FROM reservation;";

        //TODO: Add SQL to check for available spaces
        private string sqlCheckAvailability =
            "SELECT * FROM space " +
            "WHERE space.venue_id = @VenueID AND space.max_occupancy >= @numOfAttendees AND space.id NOT IN " +
            "(SELECT space.id " +
            "FROM space " +
            "JOIN reservation ON reservation.space_id = space.id " +
            "WHERE space.venue_id = @VenueID " +
            "AND space.max_occupancy >= @numOfAttendees " +
            "AND reservation.start_date <= @start_date AND reservation.end_date >= @start_date);";       



        public ReservationDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Gets an IList of all current reservations
        public IList<Reservation> GetReservations()
        {
            IList<Reservation> reservations = new List<Reservation>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlGetReservations, conn);

                //command.Parameters.AddWithValue("@ListVenuesMenuUserInput", ListVenuesMenuUserInput);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Reservation reservation = new Reservation();

                    reservation.reservation_id = Convert.ToInt32(reader["reservation_id"]);
                    reservation.space_id = Convert.ToInt32(reader["space_id"]);
                    reservation.start_date = Convert.ToString(reader["start_date"]);
                    reservation.end_date = Convert.ToString(reader["end_date"]);
                    //reservation.reserved_for = Convert.ToString(reader["reserved_for"]);

                    reservations.Add(reservation);
                }

            }

            return reservations;
        }

        public IList<Reservation> CheckAvailability(int venue_id, int numOfAttendees, DateTime start_date, int numOfDays)
        {
            IList<Reservation> availableSpaces = new List<Reservation>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlCheckAvailability, conn);

                command.Parameters.AddWithValue("@VenueID", venue_id);
                command.Parameters.AddWithValue("@numOfAttendees", numOfAttendees);
                command.Parameters.AddWithValue("@start_date", start_date);
                command.Parameters.AddWithValue("@numOfDays", numOfDays);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Reservation reservation = new Reservation();

                    //reservation.reservation_id = Convert.ToInt32(reader["reservation_id"]);
                    reservation.name = Convert.ToString(reader["name"]);
                    reservation.space_id = Convert.ToInt32(reader["id"]);
                    // Start date should be in "01-30-2020" format
                    reservation.start_date = Convert.ToString(reader["open_from"]);
                    reservation.end_date = Convert.ToString(reader["open_to"]);
                    //reservation.reserved_for = Convert.ToString(reader["reserved_for"]);
                    reservation.is_accessible = Convert.ToBoolean(reader["is_accessible"]);
                    reservation.max_occup = Convert.ToInt32(reader["max_occupancy"]);
                    reservation.daily_rate = Convert.ToDecimal(reader["daily_rate"]);
                    reservation.total_cost = Convert.ToDecimal(reader["daily_rate"]) * numOfDays;

                    availableSpaces.Add(reservation);

                }
            }
            return availableSpaces;
        }
    }
}
