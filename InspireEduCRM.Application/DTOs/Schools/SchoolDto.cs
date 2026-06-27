using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.DTOs.Schools
{
    public class SchoolDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
        public string PrincipalName {  get; set; } = string.Empty;
        public string PrincipalMobile {  get; set; } = string.Empty;
    }
}
