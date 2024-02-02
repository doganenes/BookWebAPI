using BookWebApi.Models.Dtos.RequestDto;

namespace BookWebApi.Models.Entities;

public class Book : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public string CategoryName { get; set; }
    public double Price { get; set; }
    public int StockAmount { get; set; }

    public static implicit operator Book(BookAddRequestDto dto)
    {
        return new Book()
        {
            Title = dto.Title,
            Description = dto.Description,
            AuthorName = dto.AuthorName,
            CategoryName = dto.CategoryName,
            Price = dto.Price,
            StockAmount = dto.StockAmount,
        };
    }
    public static implicit operator Book(BookUpdateRequestDto dto)
    {
        return new Book()
        {
            Title = dto.Title,
            Description = dto.Description,
            AuthorName = dto.AuthorName,
            CategoryName = dto.CategoryName,
            Price = dto.Price,
            StockAmount = dto.StockAmount,
            Id = dto.Id
        };
    }
}
