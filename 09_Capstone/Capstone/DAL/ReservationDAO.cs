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

        //gets list of existing reservations
        private string sqlGetReservations = "SELECT * FROM reservation;";

        //compares users input to space availability
        private string sqlCheckAvailability =
            "SELECT * FROM space " +
            //"LEFT OUTER JOIN reservation ON reservation.space_id = space.id " +
            "WHERE space.venue_id = @VenueID AND space.max_occupancy >= @numOfAttendees AND space.id NOT IN " +
            "(SELECT space.id " +
            "FROM space " +
            "JOIN reservation ON reservation.space_id = space.id " +
            "WHERE space.venue_id = @VenueID " +
            "AND space.max_occupancy >= @numOfAttendees " +
            "AND reservation.start_date <= @start_date AND reservation.end_date >= @start_date);";

        //creates a new reservation based on the users input
        //TODO write SQL statement to insert new reservation
        private string sqlMakeReservation = 
            "INSERT INTO reservation " +
            "(space_id, number_of_attendees, start_date, end_date, reserved_for) " +
            "VALUES(@space_id, @numOfAttendees, @arrival_date, @depart_date, @user_name);";

        // SQL Command to create a new project
        //private string sqlCreateProject = "SET IDENTITY_INSERT project ON; " +
        //                                  "INSERT INTO project(project_id, name, from_date, to_date) " +
        //                                  "VALUES(@project_id, @name, @from_date, @to_date); " +
        //                                  "SET IDENTITY_INSERT project OFF;";

        private string sqlGetLastReservation =
            "SELECT TOP(1) reservation.reservation_id, space.id, reservation.number_of_attendees, " + 
            "reservation.start_date, reservation.end_date, reservation.reserved_for, venue.name AS venueName, " +
            "space.name AS spaceName, space.daily_rate " +
            "FROM reservation " +
            "JOIN space ON space.id = reservation.space_id " +
            "JOIN venue ON venue.id = space.venue_id " +
            "ORDER BY reservation_id DESC;";




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
                    reservation.start_date = Convert.ToDateTime(reader["start_date"]);
                    reservation.end_date = Convert.ToDateTime(reader["end_date"]);
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
                    reservation./*Check*/space_name = Convert.ToString(reader["name"]);
                    reservation.space_id = Convert.ToInt32(reader["id"]);
                    // Start date should be in "2020-01-30" format
                    //reservation.start_date = Convert.ToDateTime(reader["start_date"]);
                    //reservation.end_date = Convert.ToDateTime(reader["end_date"]);
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

        public int MakeReservation(int venue_id, int numOfAttendees, DateTime start_date, int numOfDays, int intUserSpaceID, string userName)
        {
            
            int count = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlMakeReservation, conn);

                //command.Parameters.AddWithValue("@VenueID", venue_id);
                command.Parameters.AddWithValue("@numOfAttendees", numOfAttendees);
                command.Parameters.AddWithValue("@arrival_date", start_date);
                command.Parameters.AddWithValue("@depart_date", start_date.AddDays(numOfDays));
                //command.Parameters.AddWithValue("@numOfDays", numOfDays);
                command.Parameters.AddWithValue("@space_id", intUserSpaceID);
                command.Parameters.AddWithValue("@user_name", userName);

                count = command.ExecuteNonQuery();
                
            }
            return count;
        }

        public Reservation GetLastReservationDetails(int numOfDays)
        {
            Reservation reservation = new Reservation();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlGetLastReservation, conn);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //space_id, number_of_attendees, start_date, end_date, reserved_for
                    reservation.reservation_id = Convert.ToInt32(reader["reservation_id"]);
                    reservation.venue_name = Convert.ToString(reader["venueName"]);
                    reservation.space_name = Convert.ToString(reader["spaceName"]);
                    reservation.reserved_for = Convert.ToString(reader["reserved_for"]);
                    reservation.number_of_attendees = Convert.ToInt32(reader["number_of_attendees"]);
                    reservation.start_date = Convert.ToDateTime(reader["start_date"]);
                    reservation.end_date = Convert.ToDateTime(reader["end_date"]);
                    reservation.total_cost = Convert.ToDecimal(reader["daily_rate"]) * numOfDays;
                    //reservation.reserved_for = Convert.ToString(reader["reserved_for"]);

                }

            }

            return reservation;

        }

        //public int CreateProject(Project newProject)
        //{
        //    int count = 0;

        //    // Create the SQL connection
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {

        //        // Open the SQL connection
        //        conn.Open();

        //        SqlCommand command = new SqlCommand(sqlCreateProject, conn);

        //        // We passed a Department object to the method
        //        // We can call the parameters that we need from the object
        //        command.Parameters.AddWithValue("@name", newProject.Name);
        //        command.Parameters.AddWithValue("@project_id", newProject.ProjectId);
        //        command.Parameters.AddWithValue("@from_date", newProject.StartDate);
        //        command.Parameters.AddWithValue("@to_date", newProject.EndDate);

        //        count = command.ExecuteNonQuery();
        //    }

        //    return count;
        //}
    }
}
