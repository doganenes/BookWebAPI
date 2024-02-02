namespace BookWebApi.Models.Entities
{
    public class Category : Entity
    {
        public String Name { get; set; }
        public List<Book> Books {  get; set; }
    }
}
