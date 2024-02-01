using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Entities;
using BookWebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        BaseDbContext context = new BaseDbContext();
        
        [HttpPost("add")]
        public IActionResult Add([FromBody]BookAddRequestDto dto)
        {
            Book book = new Book()
            {
                AuthorName = dto.AuthorName,
                Price = dto.Price,
                StockAmount = dto.StockAmount,
                CategoryName = dto.CategoryName,
                Title = dto.Title,
                Description = dto.Description,
            };

            context.Books.Add(book);
            context.SaveChanges();

            return Ok("Added book successfully.");
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            List<Book> books = context.Books.ToList();
            return Ok(books);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery]int id)
        {
            Book book = context.Books.Find(id);
            return Ok(book);
        }

        [HttpGet("getbystockrange")]
        public IActionResult GetByStockRange([FromQuery]int min,[FromQuery]int max)
        {
            List<Book> books = context.Books.Where(x => x.StockAmount <max && x.StockAmount> min).ToList();
            return Ok(books);
        }

        [HttpGet("getbypricerange")]
        public IActionResult GetByPriceRange([FromQuery] double min, [FromQuery] double max)
        {
            List<Book> books = context.Books.Where(x => x.Price < max && x.Price > min).ToList();
            return Ok(books);
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody]BookUpdateRequestDto dto)
        {
            Book? book = context.Books.Find(dto.Id);
            if (book == null)
            {
                return NotFound();   
            }
            book.Title = dto.Title;
            book.CategoryName = dto.CategoryName;
            book.AuthorName = dto.AuthorName;
            book.Description = dto.Description;
            book.Price = dto.Price;
            book.StockAmount = dto.StockAmount;

            context.SaveChanges();
            return Ok(book);
        }
    }
}
