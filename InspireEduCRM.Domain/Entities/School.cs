using InspireEduCRM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Domain.Entities
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type {  get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
        public string PrincipalName {  get; set; } = string.Empty;
        public string PrincipalMobile {  get; set; } = string.Empty;

        // Navigation properties
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        public Lead? Lead { get; set; } // one school has one lead (nullable because it might not have one yet)
    }
}
