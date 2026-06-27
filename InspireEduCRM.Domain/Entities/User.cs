using InspireEduCRM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        // Navigation properties
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        public ICollection<FollowUp> FollowUps { get; set; } = new List<FollowUp>();

    }
}
