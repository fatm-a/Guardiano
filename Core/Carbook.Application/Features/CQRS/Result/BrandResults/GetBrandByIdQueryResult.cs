using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Application.Features.CQRS.Result.BrandResults
{
    public class GetBrandByIdQueryResult
    {
        public int BrandID { get; set; }
        public string Name { get; set; }
    }
}
