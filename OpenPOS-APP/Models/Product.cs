namespace OpenPOS_APP.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        
        public Product() { }
        public Product(int id, string name, double price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }
    }
}
