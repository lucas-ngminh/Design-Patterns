using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _4.SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Intance;
            var user = db.GetUserByName("Lucas");
            Console.WriteLine(user.ToString());

            var db2 = SingletonDatabase.Intance;
            var user2 = db2.GetUserByName("Eric");
            Console.WriteLine(user2.ToString());
        }
    }

    public interface IDatabase
    {
        User GetUserByName(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private string database;

        private SingletonDatabase()
        {
            Console.Write("Initializing database");
            Console.WriteLine();

            database = "[ { \"Name\": \"Lucas\", \"Age\": 29 }, { \"Name\": \"Eric\", \"Age\": 28 } ]";
        }

        private static SingletonDatabase intance = new SingletonDatabase();

        public static SingletonDatabase Intance = intance;

        public User GetUserByName(string name)
        {
            List<User> users = JsonConvert.DeserializeObject<List<User>>(database);
            return users.Where(u => u.Name == name).FirstOrDefault();
        }
    }

    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }
}
