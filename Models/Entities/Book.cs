namespace BookWebApi.Models.Entities;

public class Book : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public string CategoryName { get; set; }
    public double Price { get; set; }
    public int StockAmount { get; set; }
}
