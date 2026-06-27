using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.DTOs.Visits
{
    public class VisitDto
    {
        public int Id { get; set; }
        public int SchoolID { get; set; }
        public int ContactId { get; set; }
        public int SalesRepId { get; set; }
        public DateTime VisitDate { get; set; } 
        public string Notes { get; set; } = string.Empty;
        public string InterestLevel { get; set; } = string.Empty;
        public List<int> BookIds { get; set; } = new();
    }
}
