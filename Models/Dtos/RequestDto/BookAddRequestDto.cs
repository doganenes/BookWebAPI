namespace BookWebApi.Models.Dtos.RequestDto
{
    public class BookAddRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public int StockAmount { get; set; }
    }
}
