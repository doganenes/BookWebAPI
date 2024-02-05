using AutoMapper;
using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Dtos.ResponseDto;
using BookWebApi.Models.Entities;
using BookWebApi.Repositories;
using BookWebApi.ReturnModels;
using BookWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookWebApi.Controllers
    {   
        [Route("api/[controller]")]
        [ApiController]
        public class BookController : BaseController
        {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var responses = _service.GetAll();
            return ResponseForStatusCode(responses);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery]int id)
        {
            var response = _service.GetById(id);
            return ResponseForStatusCode(response);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody]BookUpdateRequestDto dto)
        {
            var response = _service.Update(dto);
            return ResponseForStatusCode(response);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody]BookAddRequestDto dto)
        {
            var response =  _service.Add(dto);
            return ResponseForStatusCode(response);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery]int id)
        {
            var response = _service.Delete(id);
            return ResponseForStatusCode(response);
        }

        [HttpGet("getalldetails")]
        public IActionResult GetAllDetails()
        {
            var results =  _service.GetAllDetails();
            return ResponseForStatusCode(results);
        }

        [HttpGet("getdetailsbyid")]
        public IActionResult GetDetailsById([FromQuery]int id)
        {
            ReturnModel<BookResponseDto> result = _service.GetDetailsById(id);
            return ResponseForStatusCode(result);
            
        }

        [HttpGet("getbycategoryid")]
        public IActionResult GetByCategoryId([FromQuery]int categoryId)
        {
            var response = _service.GetByCategoryId(categoryId);
            return ResponseForStatusCode(response);
        }

        [HttpGet("getbyauthorid")]
        public IActionResult GetByAuthorId([FromQuery]int AuthorId)
        {
            var response = _service.GetByAuthorId(AuthorId);
            return ResponseForStatusCode(response);
        }

        [HttpGet("getbypricerange")]
        public IActionResult GetByPriceRangeDetails([FromQuery]double min, [FromQuery] double max)
        {
            var responses = _service.GetByPriceRangeDetails(min, max);
            return ResponseForStatusCode(responses);
        }

        [HttpGet("getbytitlecontains")]
        public IActionResult GetByTitleContains([FromQuery]string title)
        {
            var responses = _service.GetByTitleContains(title);
            return ResponseForStatusCode(responses);
        }
    }
}
