using InspireEduCRM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Domain.Entities
{
    public class Visit
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public int ContactId { get; set; }
        public int SalesRepId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public InterestLevel InterestLevel { get; set; }

        public School School { get; set; } = null!;
        public Contact Contact { get; set; } = null!;
        public User SalesRep { get; set; }=null!;
        public ICollection<VisitBook> VisitBooks { get; set; } = new List<VisitBook>();

    }
}
