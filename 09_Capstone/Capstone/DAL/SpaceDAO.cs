using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace Capstone.DAL
{
    public class SpaceDAO : ISpaceDAO
    {
        private string connectionString;

        // SQL Command that selects all columns from the matching venue id
        private string sqlGetSpaces = "SELECT * FROM space WHERE @ListVenuesMenuUserInput = space.venue_id ";

        // Constructor with connectionString
        public SpaceDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Gets all of the available spaces for the currently selected venue
        /// </summary>
        /// <param name="ListVenuesMenuUserInput">The selected venue id</param>
        /// <returns>An iList of all the spaces in the venue</returns>
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
                    space.open_from_string = NumToMonth(Convert.ToString(reader["open_from"]));
                    space.open_to_string = NumToMonth(Convert.ToString(reader["open_to"]));
                    space.daily_rate = Convert.ToDecimal(reader["daily_rate"]);
                    space.max_occupancy = Convert.ToInt32(reader["max_occupancy"]);

                    spaces.Add(space);
                }
            }
            return spaces;
        }

        /// <summary>
        /// Converts the number given into the corresponding Month in
        /// three letter format
        /// </summary>
        /// <param name="numMonth">Number of the month to convert</param>
        /// <returns>The converted month</returns>
        public string NumToMonth(string numMonth)
        {
            switch (numMonth)
            {
                case "1":
                    numMonth = "Jan.";
                    break;
                case "2":
                    numMonth = "Feb.";
                    break;
                case "3":
                    numMonth = "Mar.";
                    break;
                case "4":
                    numMonth = "Apr.";
                    break;
                case "5":
                    numMonth = "May.";
                    break;
                case "6":
                    numMonth = "Jun.";
                    break;
                case "7":
                    numMonth = "Jul.";
                    break;
                case "8":
                    numMonth = "Aug.";
                    break;
                case "9":
                    numMonth = "Sep.";
                    break;
                case "10":
                    numMonth = "Oct.";
                    break;
                case "11":
                    numMonth = "Nov.";
                    break;
                case "12":
                    numMonth = "Dec.";
                    break;
                default:
                    break;
            }

            return numMonth;
        }
    }
}
