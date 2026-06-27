using InspireEduCRM.Application.DTOs.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.Services.Books
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllAsync();
        Task<BookDto?>GetByIdAsync(int id);
        Task<BookDto> CreateAsync(CreateBookRequest request);
    }
}
