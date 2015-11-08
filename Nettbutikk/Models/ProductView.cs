namespace Nettbutikk.Models
{
    public class ProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get;  set; }
        public string CategoryName { get; set; }
        public List<ImageView> Images { get; set; }
    }
}