using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeWork1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket();
            List<Product> products = supermarket.GetProducts();

            supermarket.AddCustomer();
            supermarket.ServeCustomer();

            Console.ReadKey();
        }
    }

    public static class RandomGenerator
    {
        private static Random s_random = new Random();

        public static int Next(int minimum, int maximum) => s_random.Next(minimum, maximum);
    }

    class Supermarket
    {
        private List<Product> _products = new List<Product>();
        private Queue<Customer> _customer = new Queue<Customer>();

        private int _maxCountCustomer = 5;
        private int _minCostProduct = 100;
        private int _maxCostProduct = 300;

        public Supermarket()
        {
            _products.Add(new Product("Онигири", RandomGenerator.Next(_minCostProduct, _maxCostProduct)));
            _products.Add(new Product("Моти", RandomGenerator.Next(_minCostProduct, _maxCostProduct)));
            _products.Add(new Product("Омурайсу", RandomGenerator.Next(_minCostProduct, _maxCostProduct)));
            _products.Add(new Product("Темаки", RandomGenerator.Next(_minCostProduct, _maxCostProduct)));
            _products.Add(new Product("Соевый Соус", RandomGenerator.Next(_minCostProduct, _maxCostProduct)));
            _products.Add(new Product("Соус Тирияки", RandomGenerator.Next(_minCostProduct, _maxCostProduct)));
            _products.Add(new Product("Темаки", RandomGenerator.Next(_minCostProduct, _maxCostProduct)));
            _products.Add(new Product("Рис", RandomGenerator.Next(_minCostProduct, _maxCostProduct)));
        }

        public List<Product> GetProducts()
        {
            return _products.ToList();
        }

        public void ShowProducts()
        {
            Console.WriteLine("Список продуктов: ");

            for (int i = 0; i < _products.Count; i++)
            {
                Product product = _products[i];
                Console.WriteLine($"{i + 1} {product.Name} {product.Price}\n");
            }
        }

        public void AddCustomer()
        {
            int countCustomer = RandomGenerator.Next(0, _maxCountCustomer);

            for (int i = 0; i < countCustomer; i++)
                _customer.Enqueue(new Customer(GetProducts()));

            ServeCustomer();
        }

        public void ServeCustomer()
        {
            ShowProducts();
            Console.WriteLine("\nНажмите на любую клавишу: ");
            Console.ReadKey();
            Console.Clear();

            if (_customer.Count > 0)
            {
                Customer nextCustomer = _customer.Dequeue();

                while (nextCustomer.Money >= 0 && nextCustomer.CanPay())
                {
                    int productIndex = RandomGenerator.Next(0, _products.Count);
                    Product product = _products[productIndex];

                    if (nextCustomer.Money > product.Price)
                    {
                        nextCustomer.PayProduct(product);
                        nextCustomer.RemoveRandomProduct();
                        nextCustomer.ShowProducts();
                    }
                }
            }

            Console.WriteLine("\nПокупатель завершил покупку.\n");
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
            Money = 1000;
            _cart = products;
        }

        public int Money { get; private set; }

        public bool CanPay()
        {
            int totalCost = 0;

            foreach (Product product in _cart)
            {
                totalCost += product.Price;
            }

            return Money <= totalCost;
        }

        public void PayProduct(Product product)
        {
            if (Money >= product.Price)
            {
                _cart.Add(product);
                Money -= product.Price;
            }

            Console.WriteLine($"\nДеньги в кошельке {Money}.");
            ShowProducts();
        }

        public void RemoveRandomProduct()
        {
            for (int i = 0; i < _cart.Count; i++)
            {
                int index = RandomGenerator.Next(0, _cart.Count);
                Product removeProduct = _cart[index];
                _cart.RemoveAt(index);
                Console.WriteLine($"удален продукт из корзины: {removeProduct.Name}.");
            }
        }

        public void ShowProducts()
        {
            Console.WriteLine("\nКорзина покупатля: ");

            for (int i = 0; i < _cart.Count; i++)
                Console.WriteLine($"{i + 1}. {_cart[i].Name}, {_cart[i].Price}.\n");
        }
    }
}
