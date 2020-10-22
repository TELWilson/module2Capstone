using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class SpaceDAO : ISpaceDAO
    {
        private string connectionString;

        private string sqlGetSpaces = "SELECT * FROM space WHERE @ListVenuesMenuUserInput = space.venue_id ";


        // Constructor with connectionString
        public SpaceDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }





        public IList<Space> GetSpaces(int ListVenuesMenuUserInput)
        {
            IList<Space> spaces = new List<Space>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sqlGetSpaces, conn);

                command.Parameters.AddWithValue("@ListVenuesMenuUserInput", ListVenuesMenuUserInput);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Space space = new Space();

                    space.id = Convert.ToInt32(reader["id"]);
                    space.name = Convert.ToString(reader["name"]);
                    //space.venue_id = Convert.ToInt32(reader["venue_id"]);
                    //space.is_accessible = Convert.ToBoolean(reader["is_accessible"]);
                    if (space.open_from != 0)
                    {
                        space.open_from = Convert.ToInt32(reader["open_from"]);
                    }
                    if (space.open_to != 0)
                    {

                        space.open_to = Convert.ToInt32(reader["open_to"]);
                    }
                    space.daily_rate = Convert.ToDecimal(reader["daily_rate"]);
                    space.max_occupancy = Convert.ToInt32(reader["max_occupancy"]);

                    spaces.Add(space);
                }
            }
            return spaces;
        }
    }
}
