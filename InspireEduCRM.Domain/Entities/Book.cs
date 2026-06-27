using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string Subject {  get; set; } = string.Empty;
        // Navigation property for many-to-many
        public ICollection<VisitBook> VisitBooks { get; set; } = new List<VisitBook>();
    }
}
