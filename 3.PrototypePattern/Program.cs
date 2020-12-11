using System;

namespace _3.PrototypePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //0. Without Prototype
            Console.WriteLine("---Without Prototype---");
            Person person1 = new Person("Lucas", new Address("Avenue O", "Brooklyn", 11204));

            Person person2 = person1;
            person2.Name = "Eric";

            Console.WriteLine(person1.ToString());
            Console.WriteLine(person2.ToString());
            //End 0. Without Prototype
            Console.WriteLine();

            //1. With Prototype using ICloneable
            Console.WriteLine("---With Prototype using ICloneable---");
            PersonWithICloneable personWithICloneable1 = new PersonWithICloneable("Lucas", new AddressWithICloneable("Avenue O", "Brooklyn", 11204));

            PersonWithICloneable personWithICloneable2 = (PersonWithICloneable)personWithICloneable1.Clone();
            personWithICloneable2.Name = "Eric";
            personWithICloneable2.Address.Street = "Sethlow";

            Console.WriteLine(personWithICloneable1.ToString());
            Console.WriteLine(personWithICloneable2.ToString());
            Console.WriteLine();
            //End 1. With Prototype using ICloneable

            //2. With Prototype using Copy Constructor
            Console.WriteLine("---With Prototype using ICloneable---");
            PersonWithCopyConstructor personWithCopyConstructor1 = new PersonWithCopyConstructor("Lucas", new AddressWithCopyConstructor("Avenue O", "Brooklyn", 11204));

            PersonWithCopyConstructor personWithCopyConstructor2 = new PersonWithCopyConstructor(personWithCopyConstructor1);
            personWithCopyConstructor2.Name = "Eric";
            personWithCopyConstructor2.Address.Street = "Sethlow";

            Console.WriteLine(personWithCopyConstructor1.ToString());
            Console.WriteLine(personWithCopyConstructor2.ToString());
            Console.WriteLine();
            //End 2. With Prototype using Copy Constructor
        }
    }

    //0. Without Prototype
    public class Person
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public Person(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Address: {Address.Street}, {Address.City} - {Address.PostalCode.ToString()}";
        }
    }

    public class Address
    {
        public string Street { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public Address(string street, string city, int postalCode)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
        }
    }
    //End 0. Without Prototype

    //1. Using ICloneable 
    public class PersonWithICloneable : ICloneable
    {
        public string Name { get; set; }

        public AddressWithICloneable Address { get; set; }

        public PersonWithICloneable(string name, AddressWithICloneable address)
        {
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Address: {Address.Street}, {Address.City} - {Address.PostalCode.ToString()}";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class AddressWithICloneable : ICloneable
    {
        public string Street { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public AddressWithICloneable(string street, string city, int postalCode)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    //End 1.Using ICloneable

    //2. Using Copy Constructor
    public class PersonWithCopyConstructor
    {
        public string Name { get; set; }

        public AddressWithCopyConstructor Address { get; set; }

        public PersonWithCopyConstructor(string name, AddressWithCopyConstructor address)
        {
            Name = name;
            Address = address;
        }

        public PersonWithCopyConstructor(PersonWithCopyConstructor personWithCopyConstructor)
        {
            Name = personWithCopyConstructor.Name;
            Address = new AddressWithCopyConstructor(personWithCopyConstructor.Address);
        }

        public override string ToString()
        {
            return $"Name: {Name}, Address: {Address.Street}, {Address.City} - {Address.PostalCode.ToString()}";
        }
    }

    public class AddressWithCopyConstructor
    {
        public string Street { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public AddressWithCopyConstructor(string street, string city, int postalCode)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
        }

        public AddressWithCopyConstructor(AddressWithCopyConstructor address)
        {
            Street = address.Street;
            City = address.City;
            PostalCode = address.PostalCode;
        }
    }
    //End 2. Using Copy Constructor
}
