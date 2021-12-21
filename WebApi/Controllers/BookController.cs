using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        //GET
         [HttpGet]
         public IActionResult GetBooks()
         {
             //var bookList = _context.Books.OrderBy(x=>x.Id).ToList<Book>();
             GetBooksQuery query = new GetBooksQuery(_context,_mapper);
             var result = query.Handle(); 
             
             return Ok(result);   
        }
    
        //GetById
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            BookDetailViewModel result;
            
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            
            
            return Ok(result);
        }

        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            // try
            // {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                // if(!result.IsValid)
                // {
                //     foreach (var item in result.Errors)
                //     {
                //         Console.WriteLine("Özellik " + item.PropertyName + "- Error Message: " + item.ErrorMessage);
                //     }
                // }else
                // {
                //     
                // }

                
            // }
            // catch (Exception ex)
            // {
                
            //     return BadRequest(ex.Message);
            // }
            
            return Ok();
        }

        //PUT
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
            
            
            UpdateBookCommand command = new UpdateBookCommand(_context);
            
            command.BookId = id;
            command.Model = updatedBook;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            
            
            return Ok();
            
        }

        //Delete
        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            
            return Ok();
        }

    }
}