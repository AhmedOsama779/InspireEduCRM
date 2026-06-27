using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.DTOs.FollowUps
{
    public class CreateFollowUpRequest
    {
        public int LeadId { get; set; }
        public int? ContactId { get; set; }
        public DateTime FollowUpDate { get; set; }
        public string FollowUpType { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string NextAction { get; set; } = string.Empty;
    }
}
