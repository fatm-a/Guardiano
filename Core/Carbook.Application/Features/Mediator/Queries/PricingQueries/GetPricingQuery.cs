﻿using Carbook.Application.Features.Mediator.Results.PricingResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Application.Features.Mediator.Queries.PricingQueries
{
    public class GetPricingQuery: IRequest<List<GetPricingQueryResult>>
    {
    }
}
