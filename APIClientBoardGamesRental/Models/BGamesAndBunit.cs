﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientBoardGamesRental.Models
{
    public class BGamesAndBunit
    {
        public BGames BGames { get; set; }
        public List<BUnit> BUnit { get; set; }
        public List<BBasket> BBaskets { get; set; }
    }
}
