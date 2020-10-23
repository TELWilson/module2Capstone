using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
   public interface IReserveDAO
    {
        IList<Reservation> GetReservations();

        IList<Reservation> CheckAvailability(int venue_id, int numOfAttendees, DateTime start_date, int numOfDays);

        int MakeReservation(int venue_id, int numOfAttendees, DateTime start_date, int numOfDays, int intUserSpaceID, string userName);

        Reservation GetLastReservationDetails(int intUserDaysWanted);
    }
}
