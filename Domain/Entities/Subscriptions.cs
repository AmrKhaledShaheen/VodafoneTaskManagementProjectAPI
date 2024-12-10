using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Subscriptions
    {
        [Key]
        public int Id { get; set; }
        public int ReportTime { get; set; }
        public DateTime StartDate { get; set; }
        public int Frequency { get; set; }
        [ForeignKey("VodafoneUser")]
        public string VodafoneUserId { get; set; }

        public VodafoneUser VodafoneUser { get; set; }

    }
}
