﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Application.Features.Mediator.Results.BlogResults
{
   public class GetLast3BlogsWitAuthorsQueryResult
    {
        public int BlogID { get; set; }

        public string Title { get; set; }

        public int AuthorID { get; set; }

        public string CoverImageUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CategoryID { get; set; }

        public string AuthorName { get; set; }

    }
}
