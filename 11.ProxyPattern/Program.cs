using System;

namespace _11.ProxyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            Console.WriteLine("Making Transaction without a Proxy:");
            Console.WriteLine("A client needs to go to bank in person and make transaction.");
            Console.WriteLine("A bank representative needs to validate your access by verifying your information.");
            Bank bank = new Bank();
            client.ClientCode(bank);
            Console.WriteLine("You leave the bank in person.");

            Console.WriteLine();

            Console.WriteLine("Making Transaction with a Proxy:");
            Console.WriteLine("Client: Executing the same client code with a proxy:");
            BankProxy proxy = new BankProxy(bank);
            client.ClientCode(proxy);
        }
    }

    public class Client
    {
        public void ClientCode(IBank bank)
        {
            // ...

            bank.MakeTransaction();

            // ...
        }
    }

    public interface IBank
    {
        void MakeTransaction();
    }

    public class Bank : IBank
    {
        public void MakeTransaction()
        {
            Console.WriteLine("Bank Making Transaction:");
            Console.WriteLine("Start Transaction");
            Console.WriteLine("Making Transaction ...");
            Console.WriteLine("Bank Ending Transaction");
        }
    }

    class BankProxy : IBank
    {
        private Bank _realBank;

        public BankProxy(Bank realBank)
        {
            this._realBank = realBank;
        }

        public void MakeTransaction()
        {
            if (this.CheckAccess())
            {
                this._realBank.MakeTransaction();

                this.LogAccess();
            }
        }

        public bool CheckAccess()
        {
            // Some real checks should go here.
            Console.WriteLine("Proxy: Checking access prior to firing a real request.");

            return true;
        }

        public void LogAccess()
        {
            Console.WriteLine("Proxy: Logging the time of request.");
        }
    }
}
