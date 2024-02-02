using AutoMapper;
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
        // Constructor args Dependency injection
        private readonly BaseDbContext _context;
        private readonly IMapper _mapper;

        BaseDbContext context = new BaseDbContext();

        public BookController(BaseDbContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody]BookAddRequestDto dto)
        {
            Book book = dto;
            _context.Books.Add(book);
            _context.SaveChanges();

            return Ok("Added book successfully.");
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            List<Book> books = _context.Books.ToList();
            return Ok(books);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery]int id)
        {   
            Book book = _context.Books.Find(id);
            return Ok(book);
        }

        [HttpGet("getbystockrange")]
        public IActionResult GetByStockRange([FromQuery]int min,[FromQuery]int max)
        {
            List<Book> books = _context.Books.Where(x => x.StockAmount <max && x.StockAmount> min).ToList();
            return Ok(books);
        }

        [HttpGet("getbypricerange")]
        public IActionResult GetByPriceRange([FromQuery] double min, [FromQuery] double max)
        {
            List<Book> books = _context.Books.Where(x => x.Price < max && x.Price > min).ToList();
            return Ok(books);
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody]BookUpdateRequestDto dto)
        {
            Book? book = _context.Books.Find(dto.Id);
            if (book == null)
            {
                return NotFound();   
            }

            book = dto;

            _context.SaveChanges();
            return Ok(book);
        }
    }
}
