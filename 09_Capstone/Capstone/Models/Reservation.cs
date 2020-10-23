using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        public int reservation_id { get; set; }

        public string name { get; set; }

        public int space_id { get; set; }

        public int number_of_attendees { get; set; }

        public int max_occup { get; set; }

        public string start_date { get; set; }

        public string end_date { get; set; }

        public string reserved_for { get; set; }

        public decimal daily_rate { get; set; }

        public decimal total_cost { get; set; }

        public bool is_accessible { get; set; }
    
    }
}
