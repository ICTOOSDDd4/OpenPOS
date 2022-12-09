namespace OpenPOS_Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public string Imagepath { get; set; }

        public Product() { }
        public Product(int id, string name, double price, string description, string imagepath )
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            Imagepath = imagepath;
        }
    }
}
