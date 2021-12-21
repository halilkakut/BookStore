using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
         private readonly IMapper _mapper;
         public int AuthorId { get; set; }

         public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
         {
             _dbContext = context;
             _mapper = mapper;
         }

         public AuthorDetailViewModel Handle()
         {
             var author = _dbContext.Authors.Include(x=>x.Book).Where(author => author.Id == AuthorId).SingleOrDefault();
             if (author is null)
             {
                 throw new InvalidOperationException("Yazar mevcut değil");
             }
             
             AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author); 
            //  vm.PublishDate = book.PublishDate.ToString("dd/MM/yyyy");
            //  vm.PageCount = book.PageCount;
            //  vm.Genre = ((GenreEnum)book.GenreId).ToString();

             return vm;

         }
    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
    }
}