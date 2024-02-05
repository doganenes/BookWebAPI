using AutoMapper;
using BookWebApi.Exceptions;
using BookWebApi.Models.Dtos.RequestDto;
using BookWebApi.Models.Dtos.ResponseDto;
using BookWebApi.Models.Entities;
using BookWebApi.Repositories;
using BookWebApi.ReturnModels;
using BookWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookWebApi.Services.Concrete;
public class BookService : IBookService
{
    private readonly BaseDbContext _context;
    private readonly IMapper _mapper;

    public BookService(BaseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ReturnModel<NoData> Add(BookAddRequestDto dto)
    {

        try
        {
            bool bookIsPresent = _context.Books.Any(x => x.Title == dto.Title);
            BookTitleUnique(dto.Title);

            Book book = _mapper.Map<Book>(dto);
            _context.Books.Add(book);
            _context.SaveChanges();
            return new ReturnModel<NoData>()
            {
                Success = true,
                Message = "The book was added",
                StatusCode = HttpStatusCode.Created
            };
        }
        catch (BusinessException ex)
        {

            return new ReturnModel<NoData>()
            {
                Success = false,
                Message = ex.Message,
                StatusCode = HttpStatusCode.BadRequest  
            };
        }
    }

    public ReturnModel<NoData> Delete(int id)
    {
        try
        {
            BookIsPresent(id);
            Book? book = _context.Books.Find(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
            return new ReturnModel<NoData>()
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = $"Id : {id} book was deleted",
            };
        }
        catch (NotFoundException ex)
        {

            return new ReturnModel<NoData>()
            {
                Success = false,
                Message = ex.Message,
                StatusCode = HttpStatusCode.NotFound
            };
        }
    }

    public ReturnModel<List<Book>> GetAll()
    {
        List<Book>books = _context.Books.ToList();
        return new ReturnModel<List<Book>>()
        {
            Data = books,
            Message = "Books are listed!",
            Success = true,
            StatusCode = HttpStatusCode.OK
        };
    }

    public ReturnModel <List<BookResponseDto>> GetAllDetails()
    {
        List<Book> books = _context.Books.Include(x => x.Author).Include(x=>x.Category).ToList();

        List<BookResponseDto> responses = _mapper.Map<List<BookResponseDto>>(books);
        return new ReturnModel<List<BookResponseDto>>()
        {
            Data = responses,
            Message = "All details listed!",
            Success = true,
            StatusCode= HttpStatusCode.OK
        };
    }

    public ReturnModel<List<BookResponseDto>> GetByAuthorId(int AuthorId)
    {
     List<Book> books = _context.Books.Include(x => x.Author).Include(x => x.Category).Where(x=>x.AuthorId == AuthorId).ToList();

        List<BookResponseDto> responses = _mapper.Map<List<BookResponseDto>>(books);
        return new ReturnModel<List<BookResponseDto>>()
        {
            Data = responses,
            Message = $"The book has author id : {AuthorId} are listed!",
            Success = true,
            StatusCode= HttpStatusCode.OK
        };
    }

    public ReturnModel<List<BookResponseDto>> GetByCategoryId(int categoryId)
    {
        List<Book> books = _context.Books.Include(x => x.Author).Include(x => x.Category).Where(x => x.CategoryId == categoryId).ToList();

        List<BookResponseDto> responses = _mapper.Map<List<BookResponseDto>>(books);
        return new ReturnModel<List<BookResponseDto>>()
        {
            Data = responses,
            Message = $"The product has categoryId : {categoryId} was getting!",
            Success = true,
            StatusCode= HttpStatusCode.OK
        };
    }

    public ReturnModel<Book> GetById(int id)
    {
        try
        {
            BookIsPresent(id);
            Book? book = _context.Books.Find(id);
            return new ReturnModel<Book>()
            {
                Data = book,
                Message = $"The book has id : {id} are getting",
                Success = true,
                StatusCode = HttpStatusCode.OK,
            };
        }
        
        catch (NotFoundException ex)
        {
            return new ReturnModel<Book>()
            {
                Message = ex.Message,
                Success = false,
                StatusCode = HttpStatusCode.NotFound,
            };
        }
    }

    public ReturnModel<List<BookResponseDto>> GetByPriceRangeDetails(double min, double max)
    {

        List<Book> books = _context.Books.Include(x => x.Author).Include(x => x.Category).Where(x => x.Price <=max && x.Price >= min).ToList();

        List<BookResponseDto> responses = _mapper.Map<List<BookResponseDto>>(books);
        return new ReturnModel<List<BookResponseDto>>()
        {
            Data = responses,
            Message = $"The books are listed which have price range : ({min} - {max})!",
            Success =true,
            StatusCode = HttpStatusCode.OK
        };
    }

    public ReturnModel<List<BookResponseDto>> GetByTitleContains(string title)
    {
        List<Book> books = _context.Books.Include(x => x.Author).Include(x => x.Category).Where(x => x.Title.Contains(title)).ToList();

        List<BookResponseDto> responses = _mapper.Map<List<BookResponseDto>>(books);
        return new ReturnModel<List<BookResponseDto>>()
        {
            Data = responses,
            Message = $"The books are listed which have matched title:  {title}"
        };
    }

    public ReturnModel<BookResponseDto> GetDetailsById(int id)
    {
        try
        {
            BookIsPresent(id);
            Book? book = _context.Books.Include(x => x.Author).Include(x => x.Category).SingleOrDefault(x => x.Id == id);

            BookResponseDto response = _mapper.Map<BookResponseDto>(book);
            return new ReturnModel<BookResponseDto>()
            {
                Data = response,
                Message = $"All details by id : {id} listed!",
                Success = true,
                StatusCode = HttpStatusCode.OK
            };

        }
        catch (NotFoundException ex)
        {
            return new ReturnModel<BookResponseDto>()
            {
                Message = ex.Message,
                Success = false,
                StatusCode = HttpStatusCode.NotFound,
            };
        }
        
    }

    public ReturnModel<NoData> Update(BookUpdateRequestDto dto)
    {
        try
        {
            BookIsPresent(dto.Id);

            Book updatedBook = _mapper.Map<Book>(dto);
            _context.Books.Update(updatedBook);
            _context.SaveChanges();
            return new ReturnModel<NoData>()
            {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = $"The book which has id: {dto.Id} was updated!"
            };
        }
        catch (NotFoundException ex)
        {
            return new ReturnModel<NoData>()
            {
                Success = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = ex.Message
            };
        }
    }

    private void BookIsPresent(int id)
    {
        var book = _context.Books.Any(x => x.Id == id);

        if(book == false)
        {
            throw new NotFoundException($"id : {id} book not found!");
        }
    }

    private void BookTitleUnique(string title)
    {

        bool bookIsPresent = _context.Books.Any(x => x.Title == title);

        if (bookIsPresent)
        {
            throw new BusinessException($"The book has {title} is already registered!");
        }

    }
}
