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

    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}
