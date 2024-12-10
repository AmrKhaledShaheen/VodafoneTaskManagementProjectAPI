using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.TaskModelViews
{
    public class FilterTaskModelView
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Status { get; set; }
        public int? FilterBy { get; set; }


        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? SearchText { get; set; }


    }
}
