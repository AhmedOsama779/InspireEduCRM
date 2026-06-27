using InspireEduCRM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Domain.Entities
{
    public class FollowUp
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public int CustomerServiceRepId { get; set; }
        public int? ContactId { get; set; } // nullable - not every follow-up targets a specific contact
        public DateTime FollowUpDate { get; set; }
        public FollowUpType FollowUpType { get; set; }
        public string Summary { get; set; } = string.Empty;
        public string NextAction { get; set; } = string.Empty;

        // Navigation properties
        public Lead Lead { get; set; } = null!;
        public User CustomerServiceRep { get; set; } = null!;
        public Contact? Contact { get; set; }

    }
}
