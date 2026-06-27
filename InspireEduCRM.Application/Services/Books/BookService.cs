using InspireEduCRM.Application.DTOs.Books;
using InspireEduCRM.Domain.Entities;
using InspireEduCRM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.Services.Books
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BookDto> CreateAsync(CreateBookRequest request)
        {
            var book = new Book
            {
                Title = request.Title,
                Subject = request.Subject,
                Grade = request.Grade,
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return MapToDto(book);
        }

        public async Task<List<BookDto>> GetAllAsync()
        {
            return await _context.Books.Select(b => MapToDto(b)).ToListAsync();
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            return book == null ? null : MapToDto(book); 
        }

        private static BookDto MapToDto(Book book) 
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Subject = book.Subject,
                Grade = book.Grade,
            };
        }
    }
}
