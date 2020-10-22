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

        // SQL Command to get specific Venue Details       WHERE department.department_id = @department_id;";
        private string sqlListVenue = "SELECT v.name, v.description, ci.name, ci.state_abbreviation, ca.name " +
                                        "FROM venue AS v JOIN city AS ci ON ci.id = v.city_id JOIN category_venue AS cv ON v.id = cv.venue_id " +
                                        "JOIN category AS ca ON ca.id = cv.category_id" +
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
                    venue.name = Convert.ToString(reader["name"]);
                    venue.description = Convert.ToString(reader["description"]);
                    //venue.location = Convert.ToString(reader["city.name" + ", " + "city.state_abbreviation"]);
                    venue.categoryName = Convert.ToString(reader["category.name"]);
                }
            }
            return venue;
        }

    }


}
