﻿using CatalogoLivros.Context;
using CatalogoLivros.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace CatalogoLivros.Service
{
    public class BooksService : IBookService
    {
        private readonly AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetBooks(BooksParameters booksParameters)
        {
            return FindAll()
                .OrderBy(on => on.Title)
                .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
                .Take(booksParameters.PageSize)
                .ToList();
        }
        public IQueryable<Book> FindAll()
        {
            return this._context.Set<Book>();
        }

        /*public async Task<IEnumerable<Book>> GetBooks()
        {
            try
            {   // retorna os livros ordenados por título
                return await _context.Books.OrderBy(t => t.Title).ToListAsync();

                // retorna os livros ordenados por id
                //return await _context.Books.ToListAsync();
            }
            catch
            {
                throw;
            }

        }*/

        public async Task<IEnumerable<Book>> searchBook(BooksParameters booksParameters, string item)
        {
            IEnumerable<Book> books;
            item = item.ToLower();

            return books = await _context.Books
                .Where(n => n.Title.Contains(item) || n.Isbn.ToString().Contains(item) || n.Author.Contains(item) || n.Price.ToString().Contains(item))
                .Skip((booksParameters.PageNumber - 1) * booksParameters.PageSize)
                .Take(booksParameters.PageSize)
                .ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            return book;

        }
        public async Task<Book> InsertBook(string isbn)
        {
            
            var book = await _context.Books.FindAsync(isbn);
            return book;

        }

        public async Task CreateBook(Book book)
        {             
            _context.Books.Add(book);
                await _context.SaveChangesAsync();
                       
        }

        public async Task UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task DeleteBookById(Book book)
        {
            _context.Entry(book).CurrentValues["isDeleted"] = true;
            await _context.SaveChangesAsync();
        }
    }
}
