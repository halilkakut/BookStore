using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public UpdateAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x=>x.Id == AuthorId);

            if(author is null)
                throw new InvalidOperationException("Yazar bulunamadı!");


            if(_dbContext.Authors.Any(x=>x.Name.ToLower()  == Model.Name.ToLower() && x.Id != AuthorId)){
                throw new InvalidOperationException("Aynı isimli yazar zaten mevcut!");
            }
            author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
            author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? author.Surname : Model.Surname;
            
            _dbContext.SaveChanges();
        }

        public class UpdateAuthorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}