using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.DTOs.Leads
{
    public class LeadDto
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; } = string.Empty;
        public string Stage { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set;}
    }
}