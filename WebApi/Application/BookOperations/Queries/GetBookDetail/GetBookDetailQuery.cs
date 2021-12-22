using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
         private readonly BookStoreDbContext _dbContext;
         private readonly IMapper _mapper;
         public int BookId { get; set; }

         public GetBookDetailQuery(BookStoreDbContext dbContext,IMapper mapper)
         {
             _dbContext = dbContext;
             _mapper = mapper;
         }

         public BookDetailViewModel Handle()
         {
             var book = _dbContext.Books.Include(x=>x.Genre).Include(x=>x.Author).Where(book => book.Id == BookId).SingleOrDefault();
             if (book is null)
             {
                 throw new InvalidOperationException("Kitap mevcut değil");
             }
             
             BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book); //new BookDetailViewModel();
            //  vm.Title = book.Title;
            //  vm.PublishDate = book.PublishDate.ToString("dd/MM/yyyy");
            //  vm.PageCount = book.PageCount;
            //  vm.Genre = ((GenreEnum)book.GenreId).ToString();

             return vm;

         }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public string PublishDate { get; set; }
        public string Author { get; set; }
    }
}