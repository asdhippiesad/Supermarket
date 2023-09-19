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

        public static int Next(int maximum) => _random.Next(maximum);
    }

    class Supermarket
    {
        private List<Product> _products = new List<Product>();
        private Queue<Customer> _customer = new Queue<Customer>();

        private int maxCountCustomer = 16;

        public Supermarket()
        {
            _products.Add(new Product("Онигири", 100));
            _products.Add(new Product("Моти", 100));
            _products.Add(new Product("Омурайсу", 80));
            _products.Add(new Product("Темаки", 150));
            _products.Add(new Product("Соевый Соус", 90));
            _products.Add(new Product("Соус Тирияки", 200));
            _products.Add(new Product("Темаки", 200));
        }

        public List<Product> Products => _products;

        public void ShowProducts()
        {
            for (int i = 0; i < _products.Count; i++)
            {
                Product product = _products[i];
                Console.WriteLine($"{i + 1} {product.Name} {product.Price}");
            }
        }
        public void AddCustomer(Customer customer)
        {
            int countCustomer = RandomGenerator.Next(maxCountCustomer);

            for (int i = 0; i < countCustomer; i++)
            {
                _customer.Enqueue(customer);
            }
        }

        public Customer ServeCustomer(Customer customer)
        {
            while (customer.Money >= 0)
            {
                int productIndex = RandomGenerator.Next(_products.Count);
                Product product = _products[productIndex];

                if (customer.Money >= product.Price)
                {
                    customer.PayProduct(product);
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
        private List<Product> _cart = new List<Product>();

        public Customer(List<Product> products)
        {
            Money = 500;
            _cart = products;
        }

        public int Money { get; private set; }
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
            if (Money <= 0 && _cart.Count > 0)
            {
                int index = RandomGenerator.Next(_cart.Count);
                Product removeProduct = _cart[index];
                _cart.RemoveAt(index);
                Money += removeProduct.Price;
                Console.WriteLine($"удален продукт {_cart.Count}.");
            }
        }

        public void ShowProducts()
        {
            foreach (Product product in _cart)
            {
                Console.WriteLine($"Корзина покупателя: {product.Name}, {product.Price}.");
            }
        }
    }
}
