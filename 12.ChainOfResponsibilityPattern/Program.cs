using System;
using System.Collections.Generic;

namespace _12.ChainOfResponsibilityPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Order> orders = new List<Order>() { 
                new Order(),
                new Order()
                {
                    IsAuthenticated = true
                },
                new Order()
                {
                    Cart = "Orange, Banana, Pork",
                    IsAuthenticated = true
                },
                new Order()
                {
                    Cart = "Samsung Galaxy S9",
                    IsAuthenticated = true,
                    Address = "Brooklyn, NY"
                },
                new Order()
                {
                    Cart = "Macbook Pro",
                    IsAuthenticated = true,
                    Address = "Brooklyn, NY",
                    Payment = "Credit Card"
                }
            };

            var authentication = new AuthenticationHandler();
            var cart = new CartHandler();
            var address = new AddressHandler();
            var payment = new PaymentHandler();

            authentication.SetNext(cart).SetNext(address).SetNext(payment);

            Client.ClientCode(authentication, orders);
            Console.WriteLine();
        }
    }
    class Client
    {
        // The client code is usually suited to work with a single handler. In
        // most cases, it is not even aware that the handler is part of a chain.
        public static void ClientCode(AbstractHandler handler, List<Order> orders)
        {
            foreach (var o in orders)
            {
                Console.WriteLine($"Client: Processing Order ID: {o.ID}");

                var result = handler.Handle(o);

                if (result != null)
                {
                    Console.Write($"   {result}");
                }
                else
                {
                    Console.WriteLine($"   Order {o.ID} is processed successfully!");
                }
                Console.WriteLine($"\n");
            }
        }
    }

    public class Order
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Cart { get; set; } = null;
        public string Address { get; set; } = null;
        public string Payment { get; set; } = null;
        public bool IsAuthenticated { get; set; } = false;
    }

    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        string Handle(Order order);
    }

    abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;

            // Returning a handler from here will let us link handlers in a
            // convenient way like this:
            // monkey.SetNext(squirrel).SetNext(dog);
            return handler;
        }

        public virtual string Handle(Order order)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(order);
            }
            else
            {
                return null;
            }
        }
    }

    class AuthenticationHandler : AbstractHandler
    {
        public override string Handle(Order order)
        {
            if (!order.IsAuthenticated)
            {
                return $"Authentication: The request is not valid.";
            }
            else
            {
                Console.WriteLine($"   Authentication: Passed Authentication Handler.");
                return base.Handle(order);
            }
        }
    }

    class CartHandler : AbstractHandler
    {
        public override string Handle(Order order)
        {
            if (string.IsNullOrEmpty(order.Cart))
            {
                return $"Cart: The cart is empty.";
            }
            else
            {
                Console.WriteLine($"   Cart: Passed Cart Handler.");
                return base.Handle(order);
            }
        }
    }

    class AddressHandler : AbstractHandler
    {
        public override string Handle(Order order)
        {
            if (string.IsNullOrEmpty(order.Address))
            {
                return $"Address: The address is not provided.";
            }
            else
            {
                Console.WriteLine($"   Authentication: Passed Address Handler.");
                return base.Handle(order);
            }
        }
    }

    class PaymentHandler : AbstractHandler
    {
        public override string Handle(Order order)
        {
            if (string.IsNullOrEmpty(order.Payment))
            {
                return $"Payment: The payment method is not provided.";
            }
            else
            {
                Console.WriteLine($"   Payment: Passed Payment Handler.");
                return base.Handle(order);
            }
        }
    }
}
