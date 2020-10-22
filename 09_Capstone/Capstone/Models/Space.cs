using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Space
    {
        public int id { get; set; }

        public int venue_id { get; set; }

        public string name { get; set; }

        public bool is_accessible { get; set; }

        public int open_from { get; set; }

        public int open_to { get; set; }

        public decimal daily_rate { get; set; }

        public int max_occupancy { get; set; }
    }
}
