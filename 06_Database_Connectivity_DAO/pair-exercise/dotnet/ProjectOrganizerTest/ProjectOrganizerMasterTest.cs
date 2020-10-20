using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace ProjectOrganizerTest
{
    [TestClass]
    public class ProjectOrganizerMasterTest
    {
        protected string ConnectionString { get; } = "Data Source=.\\sqlexpress;Initial Catalog=EmployeeDB;Integrated Security=True;";

        // Holds the new Employee ID
        protected int NewEmployeeId { get; private set; }

        private TransactionScope transaction;

        // TestInitialize runs EVERY TIME before each test is run
        [TestInitialize]

        /// <summary>
        /// This method sets up before every test
        /// </summary>
        public void SetupTest()
        {
            // Begin the Transaction 
            transaction = new TransactionScope();

            // TODO: Make a test script for our tests
            // Reads in all lines of the test script
            string sql = File.ReadAllText("test-script.sql");

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    this.NewEmployeeId = Convert.ToInt32(reader["newEmployeeId"]);
                }
            }
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }

        protected int GetRowCount(string table)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }
    }
}
