using System;
using System.Collections.Generic;

namespace HomeWork1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket();
            Customer customer = new Customer(supermarket.Products);

            supermarket.AddCustomer(customer);
            supermarket.ServeCustomer(customer);

            Console.ReadKey();
        }
    }

    public static class RandomGenerator
    {
        private static Random _random = new Random();

        public static int Next(int minimum, int maximum) => _random.Next(minimum, maximum);
    }

    class Supermarket
    {
        private List<Product> _products = new List<Product>();
        private Queue<Customer> _customer = new Queue<Customer>();

        private int maxCountCustomer = 10;
        private int minCostProduct = 100;
        private int maxCostProduct = 300;

        public Supermarket()
        {
            _products.Add(new Product("Онигири", RandomGenerator.Next(minCostProduct, maxCostProduct)));
            _products.Add(new Product("Моти", RandomGenerator.Next(minCostProduct, maxCostProduct)));
            _products.Add(new Product("Омурайсу", RandomGenerator.Next(minCostProduct, maxCostProduct)));
            _products.Add(new Product("Темаки", RandomGenerator.Next(minCostProduct, maxCostProduct)));
            _products.Add(new Product("Соевый Соус", RandomGenerator.Next(minCostProduct, maxCostProduct)));
            _products.Add(new Product("Соус Тирияки", RandomGenerator.Next(minCostProduct, maxCostProduct)));
            _products.Add(new Product("Темаки", RandomGenerator.Next(minCostProduct, maxCostProduct)));
            _products.Add(new Product("Рис", RandomGenerator.Next(minCostProduct, maxCostProduct)));
        }

        public List<Product> Products => _products;

        public void ShowProducts()
        {
            Console.WriteLine("Список продуктов: ");

            for (int i = 0; i < _products.Count; i++)
            {
                Product product = _products[i];
                Console.WriteLine($"{i + 1} {product.Name} {product.Price}\n");
            }
        }
        public void AddCustomer(Customer customer)
        {
            int countCustomer = RandomGenerator.Next(0, maxCountCustomer);

            for (int i = 0; i < countCustomer; i++)
            {
                _customer.Enqueue(new Customer(_products));
            }
        }

        public Customer ServeCustomer(Customer customer)
        {
            ShowProducts();

            Console.WriteLine("Нажмите на любую клавишу: ");
            Console.ReadKey();
            Console.Clear();

            while (customer.Money >= 0 && customer.CanPay)
            {
                int productIndex = RandomGenerator.Next(0, _products.Count);
                Product product = _products[productIndex];

                if (customer.Money > product.Price)
                {
                    customer.PayProduct(product);
                    customer.RemoveRandomProduct();
                    Console.WriteLine($"{product.Name} {product.Price}");
                }
            }

            customer.ShowProducts();
            return customer;
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; protected set; }
        public int Price { get; protected set; }
    }

    class Customer
    {
        private List<Product> _cart = new List<Product>();

        public Customer(List<Product> products)
        {
            Money = 500;
            _cart = products;
        }

        public int Money { get; protected set; }
        public bool CanPay => _cart.Count > 0;

        public void PayProduct(Product product)
        {
            if (Money >= product.Price)
            {
                _cart.Add(product);
                Money -= product.Price;
            }

            ShowProducts();
        }

        public void RemoveRandomProduct()
        {
            for (int i = 0; i < _cart.Count; i++)
            {
                int index = RandomGenerator.Next(0, _cart.Count);
                Product removeProduct = _cart[index];
                _cart.RemoveAt(index);
                Console.WriteLine($"удален продукт {removeProduct.Name}.");
            }
        }

        public void ShowProducts()
        {
            Console.WriteLine("\nКорзина покупатля: ");

            foreach (Product product in _cart)
            {
                Console.WriteLine($"{product.Name}, {product.Price}.");
            }
        }
    }
}
