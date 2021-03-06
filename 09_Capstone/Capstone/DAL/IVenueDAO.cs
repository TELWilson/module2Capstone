﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface IVenueDAO
    {
        IList<Venue> GetVenues();

        Venue ListVenue(int ListVenuesMenuUserInput);
    }
}
