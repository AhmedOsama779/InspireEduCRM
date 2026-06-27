using InspireEduCRM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Domain.Entities
{
    public class Lead
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public LeadStage Stage { get; set; } = LeadStage.Lead;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }= DateTime.UtcNow;

        public School School { get; set; }
        public ICollection<FollowUp> FollowUps { get; set; } = new List<FollowUp>();
    }
}
