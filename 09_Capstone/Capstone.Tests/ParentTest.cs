using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class ParentTest
    {
        private TransactionScope trans;

        protected string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=excelsior_venues;Integrated Security=True";

        [TestInitialize]
        public void Setup()
        {
            trans = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_reservation_insert = 
                    "SET IDENTITY_INSERT reservation ON " +
                    "INSERT INTO reservation (reservation_id, space_id, number_of_attendees, reserved_for, start_date, end_date) " +
                    "VALUES (400, 20, 100, 'Abe Lincoln', '2020-10-25', '2020-10-27'); " +
                    "SET IDENTITY_INSERT reservation OFF;";

                string sql_venue_insert =
                    "SET IDENTITY_INSERT venue ON " +
                    "INSERT INTO venue (id, name, city_id, description) " +
                    "VALUES (267, 'Cigar Party Palace', 1, 'The place to go when you want to have a smoking good time!'); " +
                    "SET IDENTITY_INSERT venue OFF;";

                string sql_space_insert =
                    "SET IDENTITY_INSERT space ON " +
                    "INSERT INTO space (id,venue_id,name,is_accessible,daily_rate,max_occupancy) " +
                    "VALUES (999, 267, 'test place', 1, 1, 1000); " +
                    "SET IDENTITY_INSERT space OFF;";


                SqlCommand cmd = new SqlCommand(sql_reservation_insert, conn);
                int count = cmd.ExecuteNonQuery();

                cmd = new SqlCommand(sql_venue_insert, conn);
                count = cmd.ExecuteNonQuery();

                cmd = new SqlCommand(sql_space_insert, conn);
                count = cmd.ExecuteNonQuery();

                Assert.AreEqual(1, count, "Insert into reservation failed");
            }
        }

        [TestCleanup]
        public void Reset()
        {
            trans.Dispose();
        }

    }
}
