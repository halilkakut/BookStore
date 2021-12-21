using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations{
    public class DataGenerator{
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using ( var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth"
                    },
                    new Genre{
                        Name = "Science Fiction"
                    },
                    new Genre{
                        Name = "Romance"
                    }
                );

                context.Authors.AddRange(
                    new Author{
                        Name = "Halil",
                        Surname = "Kakut",
                        BirthDate = new DateTime(1993,11,08)
                    },
                    new Author{
                        Name = "ibrahim",
                        Surname = "Kakut",
                        BirthDate = new DateTime(1993,06,23)
                    },
                    new Author{
                        Name = "Ahmet",
                        Surname = "Kakut",
                        BirthDate = new DateTime(1996,01,08)
                    }
                );


                context.Books.AddRange(
                    new Book{
                        //Id =1,
                        Title = "Lean Startup",
                        GenreId = 1, //personal growth
                        PageCount = 200,
                        PublishDate = new DateTime(2001,06,12)
                    },
                    new Book{
                        //Id =2,
                        Title = "Herland",
                        GenreId = 2, //Science Fiction
                        PageCount = 250,
                        PublishDate = new DateTime(2010,05,12)
                    },
                    new Book{
                        //Id =3,
                        Title = "Dune",
                        GenreId = 2, //Science Fiction
                        PageCount = 540,
                        PublishDate = new DateTime(2010,10,21)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}