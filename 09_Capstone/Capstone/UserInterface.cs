using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Capstone
{
    public class UserInterface
    {
        //ALL Console.ReadLine and WriteLine in this class
        //NONE in any other class

        private string connectionString;

        // Create private objects of the Interfaces
        // The UserInterface constructor creates these objects
        private IVenueDAO venueDAO;
        private ISpaceDAO spaceDAO;
        private IReserveDAO reserveDAO;

        // Constructor for any and all objects of type UserInterface
        public UserInterface(string connectionString, IVenueDAO venueDAO, ISpaceDAO spaceDAO, IReserveDAO reserveDAO)
        {
            this.connectionString = connectionString;
            this.venueDAO = venueDAO;
            this.spaceDAO = spaceDAO;
            this.reserveDAO = reserveDAO;
        }

        /// <summary>
        /// The "main" method of the UserInterface Class.
        /// This method should be called in order to run the actual
        /// menu screen for the User.
        /// </summary>
        public void Run()
        {
            RunMainMenu();
        }

        /// <summary>
        /// This method is the first menu screen to run for the user.
        /// This method will run until the User enters q
        /// TODO: Make this method NOT close out on invalid input?
        /// </summary>
        public void RunMainMenu()
        {
            bool stayInMainMenu = true;

            while (stayInMainMenu)
            {
                DisplayMainMenu();
                //TODO: Check and make sure the user enters a valid input.
                
                string mainMenuUserInput = Console.ReadLine().ToLower();

                switch (mainMenuUserInput)
                {
                    case "1":
                        RunListVenuesMenu();
                        break;
                    case "q":
                        Console.WriteLine("Goodbye!");
                        stayInMainMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input, quitting application");
                        stayInMainMenu = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Displays the Main Menu Options to the User.
        /// </summary>
        public void DisplayMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) List Venues");
            Console.WriteLine("Q) Quit");
        }

        /// <summary>
        /// This method is the second menu that displays out to the User.
        /// Will display Venue Details out to the user if they wish.
        /// </summary>
        public void RunListVenuesMenu()
        {
            Venue VenueDetails = new Venue();

            bool stayInListVenuesMenu = true;

            while (stayInListVenuesMenu)
            {
                // Print out Venues and Quit Option
                DisplayListVenuesMenu();
                // Take in the User's Input
                string ListVenuesMenuUserInput = Console.ReadLine();

                // If the user types in "r" or "R"
                if (ListVenuesMenuUserInput.ToLower().Equals("r"))
                {
                    stayInListVenuesMenu = false;
                    break;
                }
                else
                {
                    // Create a new Venue with the information it needs to display to the User
                    // Needs a check to see if the selected venue is a valid option
                    VenueDetails = venueDAO.ListVenue(int.Parse(ListVenuesMenuUserInput));

                    Console.WriteLine();
                    Console.WriteLine(VenueDetails.name);
                    Console.WriteLine($"Location: {VenueDetails.location}");
                    Console.WriteLine($"Categories: {VenueDetails.categoryName}");
                    Console.WriteLine();
                    Console.WriteLine(VenueDetails.description);
                    Console.WriteLine();

                    // Move to the Venue Details Menu
                    RunVenueDetailsMenu(ListVenuesMenuUserInput, VenueDetails);
                }

            }
        }

        /// <summary>
        /// This method Displays the second menu screen to the user.
        /// When called, it also displays the list of Venues out to the user.
        /// </summary>
        public void DisplayListVenuesMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Which Venue would you like to view?");

            // Prints out all Venues in the Database (And holds the list for testing)
            IList<Venue> listOfVenues = GetAllVenues();

            Console.WriteLine("R) Return to Main Menu");
        }

        /// <summary>
        /// This method gets the list of all venues from the method:
        /// GetVenues().
        /// It then displays each venue name, on its own line, out to the user.
        /// Displays "No Results" if the GetVenues() method returned 0 venues.
        /// The returned IList of venues is used for testing purposes only. Can
        /// be changed to void with no impact on functionality.
        /// </summary>
        /// <returns></returns>
        private IList<Venue> GetAllVenues()
        {
            IList<Venue> venues = venueDAO.GetVenues();

            if (venues.Count > 0)
            {
                foreach (Venue venue in venues)
                {
                    Console.WriteLine(venue.id.ToString() + ") " + venue.name.PadRight(40));
                }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }

            return venues;
        }

        /// <summary>
        /// This method is the third menu to display to the User.
        /// This method displays to the user all spaces that the currently selected venue has
        /// (As long as the user selects the first choice).
        /// TODO: Search for reservation.
        /// </summary>
        /// <param name="ListVenuesMenuUserInput"></param>
        /// <param name="venueDetails"></param>
        public void RunVenueDetailsMenu(string ListVenuesMenuUserInput, Venue venueDetails)
        {
            int venueNumber = int.Parse(ListVenuesMenuUserInput);

            bool stayInVenueDetailsMenu = true;

            while (stayInVenueDetailsMenu)
            {
                DisplayRunVenueDetailsMenu();

                string VenueDetailsMenuUserInput = Console.ReadLine();

                switch (VenueDetailsMenuUserInput)
                {
                    case "1":
                        Console.WriteLine();
                        Console.WriteLine(venueDetails.name + " Spaces");
                        Console.WriteLine();
                        Console.WriteLine("".PadRight(5) + "Name".PadRight(30) + "Open".PadRight(10) + "Close".PadRight(10) + "Daily Rate".PadRight(20) + "Max. Occupancy".PadRight(20));
                        GetAllSpaces(venueNumber);
                        Console.WriteLine();

                        RunViewSpacesMenu(venueNumber);
                        break;
                    case "2":
                        Console.WriteLine("Not Implemented yet!");
                        break;
                    case "r":
                        stayInVenueDetailsMenu = false;
                        break;
                }

            }
        }

        /// <summary>
        /// Displays the third menu out to the user.
        /// </summary>
        public void DisplayRunVenueDetailsMenu()
        {
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1) View Spaces");
            Console.WriteLine("2) Search for Reservation");
            Console.WriteLine("R) Return to Venues");
        }

        /// <summary>
        /// Displays out to the user all of the spaces inside of the currently selected venue.
        /// </summary>
        /// <param name="venueNumber"></param>
        /// <returns></returns>
        private IList<Space> GetAllSpaces(int venueNumber)
        {
            IList<Space> spaces = spaceDAO.GetSpaces(venueNumber);

            if (spaces.Count > 0)
            {
                foreach (Space space in spaces)
                {
                    Console.WriteLine("#" + space.id.ToString().PadRight(4) + space.name.PadRight(30) + space.open_from_string.ToString().PadRight(10) + space.open_to_string.ToString().PadRight(10) + space.daily_rate.ToString("C").PadRight(20) + space.max_occupancy.ToString().PadRight(20));
                }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }

            return spaces;
        }

        /// <summary>
        /// Runs the menu that displays while the spaces are being viewed.
        /// </summary>
        /// <param name="venueNumber"></param>
        public void RunViewSpacesMenu(int venueNumber)
        {
            bool stayInViewSpacesMenu = true;

            while (stayInViewSpacesMenu)
            {
                DisplayViewSpacesMenu(venueNumber);

                string ViewSpacesMenuUserInput = Console.ReadLine();

                switch (ViewSpacesMenuUserInput)
                {
                    case "1":
                        //IList<Reservation> listOfReservations = reserveDAO.GetReservations();
                        RunReserveASpaceMenu(venueNumber);
                        break;
                    case "r":
                        stayInViewSpacesMenu = false;
                        break;
                }

            }
        }

        /// <summary>
        /// Displays the menu options while the spaces are being viewed.
        /// </summary>
        /// <param name="venueNumber"></param>
        public void DisplayViewSpacesMenu(int venueNumber)
        {
            //spaceDAO.GetSpaces(venueNumber);//TODO: SQL that prints out each id, name, open, close, daily rate, max occupancy
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1) Reserve a Space");
            Console.WriteLine("R) Return to Venue Details");
        }

        /// <summary>
        /// The final menu that can be viewed by the user in order to reserve a space.
        /// </summary>
        public void RunReserveASpaceMenu(int venueNumber)
        {
            Console.Write("When do you need the space?: ");
            // Make sure it is in the correct format 01-29-2020
            string userStartDate = Console.ReadLine();
            DateTime dTUserStartDate = DateTime.Parse(userStartDate);

            Console.Write("How many days will you need the space?: ");
            string userDaysWanted = Console.ReadLine();
            int intUserDaysWanted = int.Parse(userDaysWanted);

            Console.Write("How many people will be in attendance?: ");
            string userAttendees = Console.ReadLine();
            int intUserAttendees = int.Parse(userAttendees);

            Console.WriteLine("The following spaces are available based on your needs: ");
            // SQL statement that displays space#, name, daily rate, max occupancy, accessible, total cost
            IList<Reservation> availableSpaces = reserveDAO.CheckAvailability(venueNumber, intUserAttendees, dTUserStartDate, intUserDaysWanted);
            //Make this a method
            Console.WriteLine("Space #".PadRight(10) + "Name".PadRight(30) + "Daily Rate".PadRight(10) + "Max. Occup.".PadRight(10) + "Accessibile?".PadRight(20) + "Total Cost".PadRight(20));
            foreach (Reservation reservation in availableSpaces)
            {
                Console.WriteLine("#" + reservation.reservation_id.ToString().PadRight(9) + reservation.name.PadRight(30) + reservation.daily_rate.ToString("C").PadRight(10) + reservation.max_occup.ToString().PadRight(10) + reservation.is_accessible.ToString().PadRight(20) + reservation.total_cost.ToString("C").PadRight(20));
            }

            Console.Write("Which space would you like to reserve (Enter 0 to cancel)?: ");
            string userReservationChoice = Console.ReadLine();

            Console.Write("Who is this reservation for?: ");
            string userName = Console.ReadLine();

            Console.WriteLine("Thanks for submitting your reservation! The details for your event are listed below: ");
            // SQL statement to insert the reservation
        }
    }
}
