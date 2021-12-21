using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetailQuery;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;
using static WebApi.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET
         [HttpGet]
         public IActionResult GetGenres()
         {
             //var bookList = _context.Books.OrderBy(x=>x.Id).ToList<Book>();
             GetGenresQuery query = new GetGenresQuery(_context,_mapper);
             var result = query.Handle(); 
             
             return Ok(result);   
        }


        [HttpGet("id")]
         public IActionResult GetGenreDetail(int id)
         {
             //var bookList = _context.Books.OrderBy(x=>x.Id).ToList<Book>();
             GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
             query.GenreId =id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);

             var result = query.Handle(); 
             
             return Ok(result);   
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = updateGenre;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        //Delete
        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            
            command.Handle();
            
            return Ok();
        }

    }
}