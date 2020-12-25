using System;

namespace _9.FacadePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderFacade o = new OrderFacade();
            o.PlaceOrder();
        }
    }

    public class Product
    {
        public void GetProductDetails()
        {
            Console.WriteLine("This is your product details!");
        }
    }

    public class Payment
    {
        public void MakePayment()
        {
            Console.WriteLine("Payment Proceeded Successfully!");
        }
    }

    public class Invoice
    { 
        public void SendInvoice()
        {
            Console.WriteLine("Invoice Sent Successfully!");
        }
    }

    public class OrderFacade
    {
        public void PlaceOrder()
        {
            Console.WriteLine("---Ordering Proccess Started---");
            Product p = new Product();
            p.GetProductDetails();
            Payment pmt = new Payment();
            pmt.MakePayment();
            Invoice inv = new Invoice();
            inv.SendInvoice();
            Console.WriteLine("Order Placed Successfully!");
            Console.WriteLine("---Ordering Proccess Ended---");
        }
    }
}
