﻿using Carbook.Application.Features.Mediator.Queries.SocialMediaQueries;
using Carbook.Application.Features.Mediator.Results.SocialMediaResults;
using Carbook.Application.Interfaces;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Application.Features.Mediator.Handlers.SocialMediaHandlers
{

    public class GetServiceQueryHandler : IRequestHandler<GetSocialMediaQuery, List<GetSocialMediaQueryResult>>
    {
        private readonly IRepository<SocialMedia> _repository;
        public GetServiceQueryHandler(IRepository<SocialMedia> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetSocialMediaQueryResult>> Handle(GetSocialMediaQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetSocialMediaQueryResult
            {
                Name = x.Name,
                SocialMediaID = x.SocialMediaID,
                Url = x.Url,
                Icon = x.Icon
            }).ToList();
        }
    }
}