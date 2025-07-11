﻿using Carbook.Application.Features.Mediator.Queries.TagCloudQueries;
using Carbook.Application.Features.Mediator.Results.TagCloudResults;
using Carbook.Application.Interfaces;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Application.Features.Mediator.Handlers.TagCloudHandlers
{
    public class GetPricingByIdQueryHandler : IRequestHandler<GetTagCloudByIdQuery, GetTagCloudByIdQueryResult>
    {
        private readonly IRepository<TagCloud> _repository;
        public GetPricingByIdQueryHandler(IRepository<TagCloud> repository)
        {
            _repository = repository;
        }

        public async Task<GetTagCloudByIdQueryResult> Handle(GetTagCloudByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetTagCloudByIdQueryResult
            {
                TagCloudID = values.TagCloudID,
                Title = values.Title,
                BlogID = values.BlogID
            };
        }
    }
}