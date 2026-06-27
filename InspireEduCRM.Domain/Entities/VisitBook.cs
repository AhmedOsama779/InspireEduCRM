using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Domain.Entities
{
    public class VisitBook
    {
        public int VisitId { get; set; }
        public int BookId { get; set; }

        public Visit Visit { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}
