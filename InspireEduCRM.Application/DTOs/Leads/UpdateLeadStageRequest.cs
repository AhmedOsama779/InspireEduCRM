using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.DTOs.Leads
{
    public class UpdateLeadStageRequest
    {
        public string Stage { get; set; } = string.Empty; // e.g. "Qualified", "Won", "Lost"
    }
}
