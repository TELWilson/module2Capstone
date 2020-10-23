using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class VenueDAO : IVenueDAO
    {
        private string connectionString;

        // SQL Command used to get all Venues
        private string sqlGetVenue = "SELECT * FROM venue;";

        // SQL Command to get Venue Details for a specific venue_id
        private string sqlListVenue =
            "SELECT venue.name AS 'venueName', city.name AS 'cityName', city.state_abbreviation, category.name AS 'categoryName', description " +
            "FROM venue " +
            "JOIN city ON city.id = venue.city_id " +
            "LEFT OUTER JOIN category_venue ON category_venue.venue_id = venue.id " +
            "LEFT OUTER JOIN category ON category.id = category_venue.category_id " +
            "WHERE venue.id = @venue_id;";


        // Constructor with connectionString
        public VenueDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Venue> GetVenues()
        {
            IList<Venue> venues = new List<Venue>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlGetVenue, conn);

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    Venue venue = new Venue();

                    venue.id = Convert.ToInt32(reader["id"]);
                    venue.name = Convert.ToString(reader["name"]);
                    venue.city_id = Convert.ToInt32(reader["city_id"]);
                    venue.description = Convert.ToString(reader["description"]);

                    venues.Add(venue);
                }
            }
            return venues;
        }

        /// <summary>
        /// Gets the user's wanted venue name, city, state, categories, and description
        /// </summary>
        /// <param name="ListVenuesMenuUserInput">The venue.id to get information from</param>
        /// <returns>Venue with venue information</returns>
        public Venue ListVenue(int ListVenuesMenuUserInput)
        {
            Venue venue = new Venue();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlListVenue, conn);

                command.Parameters.AddWithValue("@venue_id", ListVenuesMenuUserInput);

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    venue.name = Convert.ToString(reader["venueName"]);
                    venue.description = Convert.ToString(reader["description"]);
                    venue.location = Convert.ToString(reader["cityName"]);
                    venue.location += ", ";
                    venue.location += Convert.ToString(reader["state_abbreviation"]);
                    venue.categoryName += Convert.ToString(reader["categoryName"]);
                    venue.categoryName += "  ";
                }
            }
            return venue;
        }

    }


}
