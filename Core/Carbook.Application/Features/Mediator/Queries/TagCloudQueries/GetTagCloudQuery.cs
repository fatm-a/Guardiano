﻿using Carbook.Application.Features.Mediator.Results.TagCloudResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Application.Features.Mediator.Queries.TagCloudQueries
{

    public class GetTagCloudQuery : IRequest<List<GetTagCloudQueryResult>>
    {
    }
}
