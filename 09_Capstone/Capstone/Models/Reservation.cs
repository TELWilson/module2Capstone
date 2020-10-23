using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        public int reservation_id { get; set; }

        public string venue_name { get; set; }

        public int space_id { get; set; }

        public string space_name { get; set; }

        public int number_of_attendees { get; set; }

        public int max_occup { get; set; }

        //Changed to String. Effects ReservationDAO Lines 78/79/116/117/176/177
        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        public string reserved_for { get; set; }

        public decimal daily_rate { get; set; }

        public decimal total_cost { get; set; }

        public bool is_accessible { get; set; }
    
    }
}
