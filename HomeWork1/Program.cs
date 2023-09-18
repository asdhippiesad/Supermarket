using System;
using System.Collections.Generic;

namespace HomeWork1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket();
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine();
            }
         
            Console.ReadKey();
        }
    }

    class Supermarket
    {
        private List<Product> _products = new List<Product>();
        private Queue<Customer> _customer = new Queue<Customer>();
        private Random _random = new Random();

        private int maxCountCustomer = 16;

        public Supermarket()
        {
            _products.Add(new Product("Онигири", 50));
            _products.Add(new Product("Моти", 40));
            _products.Add(new Product("Омурайсу", 80));
            _products.Add(new Product("Темаки", 70));
            _products.Add(new Product("Соевый Соус", 90));
            _products.Add(new Product("Соус Тирияки", 60));
            _products.Add(new Product("Темаки", 20));
        }

        public void ShowProduct()
        {
            for (int i = 0; i < _products.Count; i++)
            {
                Product product = _products[i];
                Console.WriteLine($"{i + 1} {product.Name} {product.Price}");
            }
        }

        public void AddCustomer(Customer customer)
        {
            int countCustomer = _random.Next(maxCountCustomer);

            for (int i = 0; i < countCustomer; i++)
            {
                _customer.Enqueue(customer);
            }
        }

        public Customer ServeCustomer(Customer customer)
        {
            while (customer.Money >= 0)
            {
                int productIndex = _random.Next(0, _products.Count);
                Product product = _products[productIndex];

                if (customer.Money >= product.Price)
                {
                    customer.BuyProduct(product);
                    Console.WriteLine($"{product.Name} {product.Price}");
                }
                else
                {
                    customer.RemoveRandomProduct();
                }
            }

            customer.ShowProducts();
            return customer;
        }

        private int GetCustomer()
        {
            int customerCount = _customer.Count;
            int productCount = _products.Count;

            int count = _random.Next(customerCount, productCount);

            for (int i = 0; i < count; i++)
            {
                _products.Add(_products[i]);
            }

            return count;
        }

        private void SellProduct(List<Product> products)
        {
            Customer customer = new Customer(products);

            int productIndex = _random.Next(products.Count);

            for (int i = 0; i < products.Count; i++)
            {
                if (customer.Money < _products[productIndex].Price)
                {
                    Product product = _products[productIndex];
                    customer.BuyProduct(product);
                    products.Add(product);
                }
            }
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
    }

    class Customer
    {
        private List<Product> _customerBasket = new List<Product>();
        private Random _random = new Random();

        public Customer(List<Product> products)
        {
            Money = 500;
            _customerBasket = products;
        }

        public int Money { get; private set; }
        public bool CanPay => _customerBasket.Count > 0;

        public void BuyProduct(Product product)
        {
            _customerBasket.Add(product);
            Money -= product.Price;
        }

        public void RemoveRandomProduct()
        {
            if (Money <= 0)
            {
                int index = _random.Next(0, _customerBasket.Count );
                _customerBasket.RemoveAt(index);
                Console.WriteLine($"удален продукт {_customerBasket.Count}");
            }
        }

        public void ShowProducts()
        {
            foreach (Product product in _customerBasket)
            {
                Console.WriteLine($"Корзина покупателя: {product}");
            }
        }
    }
}
