﻿using Carbook.Application.Interfaces.ReviewInterfaces;
using CarBook.Domain.Entities;
using Carbook.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Persistence.Repositories.ReviewRepositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly CarBookContext _context;

        public ReviewRepository(CarBookContext context)
        {
            _context = context;
        }

        public List<Review> GetReviewsByCarId(int carId)
        {
            var values = _context.Reviews.Where(x => x.CarID == carId).ToList();
            return values;
        }
    }
}