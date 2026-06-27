using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Domain.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile {  get; set; } = string.Empty ;
        public string Position { get; set; } = string.Empty;
        // Navigation properties
        public School School { get; set; }
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        public ICollection<FollowUp> FollowUps { get; set; } = new List<FollowUp>();

    }
}
