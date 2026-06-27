using InspireEduCRM.Application.DTOs.Schools;
using InspireEduCRM.Domain.Entities;
using InspireEduCRM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ApplicationDbContext _context;

        public SchoolService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SchoolDto> CreateAsync(CreateSchoolRequest request)
        {
            var school = new School
            {
                Name = request.Name,
                Type = request.Type,
                City = request.City,
                Address = request.Address,
                PrincipalName = request.PrincipalName,
                PrincipalMobile = request.PrincipalMobile,
            };
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();
            return MapToDto(school);
        }

        public async Task<List<SchoolDto>> GetAllAsync()
        {
            return await _context.Schools.Select(s => MapToDto(s)).ToListAsync();
        }

        public async Task<SchoolDto?> GetByIdAsync(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            return school == null ? null : MapToDto(school);
        }

        public async Task<SchoolDto?> UpdateAsync(int id, CreateSchoolRequest request)
        {
            var school = await _context.Schools.FindAsync(id);
            if(school == null) return null;

            school.Name = request.Name;
            school.Type = request.Type;
            school.City = request.City;
            school.Address = request.Address;
            school.PrincipalName = request.PrincipalName;
            school.PrincipalMobile = request.PrincipalMobile;

            await _context.SaveChangesAsync();
            return MapToDto(school);

        }

        private static SchoolDto MapToDto(School school) 
        {
            return new SchoolDto
            {
                Id = school.Id,
                Name = school.Name,
                Type = school.Type,
                City = school.City,
                Address = school.Address,
                PrincipalName = school.PrincipalName,
                PrincipalMobile = school.PrincipalMobile,
            };
        }
    }
}
