using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Venue
    {
        public int id { get; set; }

        public string name { get; set; }

        public int city_id { get; set; }

        public string description { get; set; }

        public string location { get; set; }

        public string categoryName { get; set; }
    }
}
