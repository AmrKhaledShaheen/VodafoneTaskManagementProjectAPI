using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        public int Status { get; set; }
        [ForeignKey("VodafoneUser")]
        public string VodafoneUserId { get; set; }

        public VodafoneUser VodafoneUser { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedDate { get; set; }

    }
}
