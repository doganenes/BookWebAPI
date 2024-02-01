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
        public IActionResult Add([FromBody]Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();

            return Ok("Added book successfully.");
        }
    }
}
