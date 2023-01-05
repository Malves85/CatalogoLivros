﻿using CatalogoLivros.Helpers;
using CatalogoLivros.Models;

namespace CatalogoLivros.Service
{
    public interface IBookService
    {
        Task<MessagingHelper> DeleteBook(int id);
        Task<MessagingHelper<BookDTO>> GetById(int id);
        Task<MessagingHelper<BookDTO>> Update(EditBook editBook);
        Task<MessagingHelper<int>> Create(CreateBook createBook);
        Task<PaginatedList<ListBook>> GetAll(Search search);
        
    }
}

