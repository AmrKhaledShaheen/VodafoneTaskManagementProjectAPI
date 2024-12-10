using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels.SubscriptionModelViews
{
    public class SubscribeModelView
    {
        public TimeSpan ReportTime { get; set; }
        public DateTime StartDate { get; set; }
        public int Frequency { get; set; }
    }
}
