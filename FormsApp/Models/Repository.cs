namespace FormsApp.Models
{
    public class Repository
    {

        private static readonly List<Product> _products = new();

        private static readonly List<Category> _categories = new();

        static Repository(){

            _categories.Add(new Category {CategoryId = 1, Name ="Telefon"});
            _categories.Add(new Category {CategoryId = 2, Name ="Bilgisayar"});

            _products.Add(new Product {ProductId = 1, Name = "iPhone 14", Price = 40000, IsActive = true, Image = "1.jpg", CategoryID = 1});
            _products.Add(new Product {ProductId = 2, Name = "IPhone 15", Price = 45000, IsActive = true, Image = "2.jpg", CategoryID = 1});
            _products.Add(new Product {ProductId = 3, Name = "IPhone 16", Price = 50000, IsActive = true, Image = "3.jpg", CategoryID = 1});
            _products.Add(new Product {ProductId = 4, Name = "IPhone 17", Price = 60000, IsActive = true, Image = "4.jpg", CategoryID = 1});

            _products.Add(new Product {ProductId = 5, Name = "Macbook Air", Price = 80000, IsActive = true, Image = "5.jpg", CategoryID = 2});
            _products.Add(new Product {ProductId = 6, Name = "Macbook Pro", Price = 90000, IsActive = true, Image = "6.jpg", CategoryID = 2});





        }
        public static List<Product> Products
        {
            get
            {
                return _products;
            }
        }

        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }
    }
}