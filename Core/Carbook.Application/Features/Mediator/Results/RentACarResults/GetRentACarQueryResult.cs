﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Application.Features.Mediator.Results.RentACarResults
{
    public class GetRentACarQueryResult
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Amount { get; set; }
        public string CoverImageUrl { get; set; }
    }
}