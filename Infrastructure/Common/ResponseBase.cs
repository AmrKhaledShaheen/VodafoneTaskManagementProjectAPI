using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class ResponseBase
    {
        public int? Id { get; set; }
        public string? Message { get; set; }
        public string? Title { get; set; }
        public bool Succeeded { get; set; }
        //public ResponseCode StatusCode { get; set; }
        public int? recordsFiltered { get; set; }
        public int? TotalPages { get; set; }
        public int? CurrentPage { get; set; }
        public int? recordsTotal { get; set; }
    }
}
