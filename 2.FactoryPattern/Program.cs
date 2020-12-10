using System;

namespace _2.FactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //0. Without Factory Pattern
            Console.WriteLine("---Without Factory Pattern---");
            Truck truck = new Truck();
            truck.Delivery();

            Ship ship = new Ship();
            ship.Delivery();
            Console.WriteLine();

            //1. Factory Pattern
            Console.WriteLine("---Factory Pattern---");
            var truck1 = VehicleFactory.NewTruck();
            truck1.Delivery();

            var ship1 = VehicleFactory.NewShip();
            ship1.Delivery();

            var airplane = VehicleFactory.NewAirplane();
            airplane.Delivery();
        }
    }

    //0. Without Factory Pattern
    public class Truck
    {
        public string Type { get; set; }

        public Truck()
        {
            Type = "Truck";
        }

        public void Delivery()
        {
            Console.WriteLine($"{Type} is used for delivery.");
        }
    }

    //When the company requires to add Ships as a new vehicle type 
    // because customers prefer sea shipping method
    public class Ship
    {
        public string Type { get; set; }

        public Ship()
        {
            Type = "Ship";
        }

        public void Delivery()
        {
            Console.WriteLine($"{Type} is used for delivery.");
        }
    }
    //End 0. Without Factory Pattern

    //1. Factory Pattern
    public enum VehicleType
    {
        Truck,
        Ship,
        Airplane
    }

    public class Vehicle
    {
        public string Type { get; set; }

        public Vehicle(VehicleType type)
        {
            Type = type.ToString();
        }

        public void Delivery()
        {
            Console.WriteLine($"{Type} is used for delivery.");
        }
    }

    public class VehicleFactory
    { 
        public static Vehicle NewTruck()
        {
            return new Vehicle(VehicleType.Truck);
        }

        public static Vehicle NewShip()
        {
            return new Vehicle(VehicleType.Ship);
        }

        public static Vehicle NewAirplane()
        {
            return new Vehicle(VehicleType.Airplane);
        }
    }
    //End 1. Factory Pattern
}
