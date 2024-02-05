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

    public void Add(BookAddRequestDto dto)
    {

        bool bookIsPresent = _context.Books.Any(x =>x.Title == dto.Title);
        BookTitleUnique(dto.Title);

        Book book = _mapper.Map<Book>(dto);
        _context.Books.Add(book);
        _context.SaveChanges(); 
    }

    public void Delete(int id)
    {
        BookIsPresent(id);
        Book? book = _context.Books.Find(id);
        _context.Books.Remove(book);
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

    public List<BookResponseDto> GetAllDetails()
    {
        List<Book> books = _context.Books.Include(x => x.Author).Include(x=>x.Category).ToList();

        List<BookResponseDto> responses = _mapper.Map<List<BookResponseDto>>(books);
        return responses;
    }

    public List<BookResponseDto> GetByAuthorId(int AuthorId)
    {
     List<Book> books = _context.Books.Include(x => x.Author).Include(x => x.Category).Where(x=>x.AuthorId == AuthorId).ToList();

        List<BookResponseDto> responses = _mapper.Map<List<BookResponseDto>>(books);
        return responses;

    }

    public List<BookResponseDto> GetByCategoryId(int categoryId)
    {
        List<Book> books = _context.Books.Include(x => x.Author).Include(x => x.Category).Where(x => x.CategoryId == categoryId).ToList();

        List<BookResponseDto> responses = _mapper.Map<List<BookResponseDto>>(books);
        return responses;

    }

    public ReturnModel<Book> GetById(int id)
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

    public List<BookResponseDto> GetByPriceRangeDetails(double min, double max)
    {

        List<Book> books = _context.Books.Include(x => x.Author).Include(x => x.Category).Where(x => x.Price <=max && x.Price >= min).ToList();

        List<BookResponseDto> responses = _mapper.Map<List<BookResponseDto>>(books);
        return responses;

    }

    public List<BookResponseDto> GetByTitleContains(string title)
    {
        List<Book> books = _context.Books.Include(x => x.Author).Include(x => x.Category).Where(x => x.Title.Contains(title)).ToList();

        List<BookResponseDto> responses = _mapper.Map<List<BookResponseDto>>(books);
        return responses;
    }

    public BookResponseDto GetDetailsById(int id)
    {

        BookIsPresent(id);
        Book? book = _context.Books.Include(x=>x.Author).Include(x=>x.Category).SingleOrDefault(x=>x.Id == id);

        BookResponseDto response = _mapper.Map<BookResponseDto>(book);
        return response;

    }

    public void Update(BookUpdateRequestDto dto)
    {
        BookIsPresent(dto.Id); 
       
        Book updatedBook = _mapper.Map<Book>(dto);  
        _context.Books.Update(updatedBook);
        _context.SaveChanges();
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
