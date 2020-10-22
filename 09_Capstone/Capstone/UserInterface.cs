﻿using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class UserInterface
    {
        //ALL Console.ReadLine and WriteLine in this class
        //NONE in any other class

        private string connectionString;

        private IVenueDAO venueDAO;

        public UserInterface(string connectionString, IVenueDAO venueDAO)
        {
            this.connectionString = connectionString;
            this.venueDAO = venueDAO;
        }

        public void Run()
        {
            // Test Purposes
            Console.WriteLine("Reached the User Interface.");
            RunMainMenu();
        }

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

        public void DisplayMainMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) List Venues");
            Console.WriteLine("Q) Quit");
        }

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

        public void RunListVenuesMenu()
        {
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
                    // Needs a check to see if the selected venue is a valid option
                    Venue VenueDetails = venueDAO.ListVenue(int.Parse(ListVenuesMenuUserInput));

                    Console.WriteLine(VenueDetails.name);
                    Console.WriteLine($"Location: {VenueDetails.location}");
                    Console.WriteLine($"Categories: {VenueDetails.categoryName}");
                    Console.WriteLine();
                    Console.WriteLine(VenueDetails.description);
                    Console.WriteLine();
                }

            }
        }

        public void DisplayListVenuesMenu()
        {
            Console.WriteLine("Which Venue would you like to view?");

            // Prints out all Venues in the Database (And holds the list for testing)
            IList<Venue> listOfVenues = GetAllVenues();

            Console.WriteLine("R) Return to Main Menu");
        }

        public void RunVenueDetailsMenu()
        {
            bool stayInVenueDetailsMenu = true;

            while (stayInVenueDetailsMenu)
            {
                DisplayRunVenueDetailsMenu();

                string VenueDetailsMenuUserInput = Console.ReadLine();

                switch (VenueDetailsMenuUserInput)
                {
                    case "1":
                        RunViewSpacesMenu();
                        break;
                    case "2":
                        break;
                    case "r":
                        stayInVenueDetailsMenu = false;
                        break;
                }

            }
        }

        public void DisplayRunVenueDetailsMenu()
        {
            //TODO:     sql statement that prints out venue name, location, categories, and description
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1) View Spaces");
            Console.WriteLine("2) Search for Reservation");
            Console.WriteLine("R) Return to Venues");
        }

        public void RunViewSpacesMenu()
        {
            bool stayInViewSpacesMenu = true;

            while (stayInViewSpacesMenu)
            {
                DisplayViewSpacesMenu();

                string ViewSpacesMenuUserInput = Console.ReadLine();

                switch (ViewSpacesMenuUserInput)
                {
                    case "1":
                        RunReserveASpaceMenu();
                        break;
                    case "r":
                        stayInViewSpacesMenu = false;
                        break;
                }

            }
        }

        public void DisplayViewSpacesMenu()
        {
            //TODO: SQL that prints out each id, name, open, close, daily rate, max occupancy
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("1) Reserve a Space");
            Console.WriteLine("R) Return to Venue Details");
        }

        public void RunReserveASpaceMenu()
        {
            Console.Write("When do you need the space?: ");
            string userStartDate = Console.ReadLine();

            Console.Write("How many days will you need the space?: ");
            string userDaysWanted = Console.ReadLine();

            Console.Write("How many people will be in attendance?: ");
            string userAttendees = Console.ReadLine();

            Console.Write("The following spaces are available based on your needs: ");
            // SQL statement that displays space#, name, daily rate, max occupancy, accessible, total cost

            Console.Write("Which space would you like to reserve (Enter 0 to cancel)?: ");
            string userReservationChoice = Console.ReadLine();

            Console.Write("Who is this reservation for?: ");
            string userName = Console.ReadLine();

            Console.WriteLine("Thanks for submitting your reservation! The details for your event are listed below: ");
        }


    }
}