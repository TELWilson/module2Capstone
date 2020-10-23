using Capstone.DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            #region call to appsettings.json for configuration
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            #endregion

            string connectionString = configuration.GetConnectionString("Project");

            // Create a new object of type VenueDAO so that we can pass it into the User Interface.
            IVenueDAO venueDAO = new VenueDAO(connectionString);
            // Create a new object of type SpaceDAO so that we can pass it into the User Interface.
            ISpaceDAO spaceDAO = new SpaceDAO(connectionString);
            // Create a new object of type ReservationDAO so that we can pass it into the User Interface.
            IReserveDAO reserveDAO = new ReservationDAO(connectionString);

            UserInterface ui = new UserInterface(connectionString, venueDAO, spaceDAO, reserveDAO);
            ui.Run();

        }
    }
}
