﻿using Carbook.Application.Features.Mediator.Queries.StatisticsQueries;
using Carbook.Application.Features.Mediator.Results.StatisticsResults;
using Carbook.Application.Interfaces.StatisticsInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Application.Features.Mediator.Handlers.StatisticsHandlers
{
    public class GetCarBrandAndModelByRentPriceDailyMaxQueryHandler : IRequestHandler<GetCarBrandAndModelByRentPriceDailyMaxQuery, GetCarBrandAndModelByRentPriceDailyMaxQueryResult>
    {
        private readonly IStatisticsRepository _repository;

        public GetCarBrandAndModelByRentPriceDailyMaxQueryHandler(IStatisticsRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCarBrandAndModelByRentPriceDailyMaxQueryResult> Handle(GetCarBrandAndModelByRentPriceDailyMaxQuery request, CancellationToken cancellationToken)
        {
            var value = _repository.GetCarBrandAndModelByRentPriceDailyMax();
            return new GetCarBrandAndModelByRentPriceDailyMaxQueryResult
            {
                CarBrandAndModelByRentPriceDailyMax = value
            };
        }
    }
}