using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.DTOs.Contacts
{
    public class ContactDto
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Position { get; set; }= string.Empty;
    }
}
