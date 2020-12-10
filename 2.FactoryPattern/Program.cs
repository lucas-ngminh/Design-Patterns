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
            VehicleFactory vf = new VehicleFactory();
            Vehicle truckVehicle = vf.NewTruck();
            truckVehicle.Delivery();

            Vehicle shipVehicle = vf.NewShip();
            shipVehicle.Delivery();

            Vehicle airplaneVehicle = vf.NewAirplane();
            airplaneVehicle.Delivery();
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

    public abstract class Vehicle
    {
        public abstract string Type { get; set;  }

        public abstract void Delivery();
    }

    public class TruckVehicle : Vehicle
    {
        public TruckVehicle()
        {
            Type = VehicleType.Truck.ToString();
        }

        public override string Type { get; set; }

        public override void Delivery()
        {
            Console.WriteLine($"{Type} is used for delivery.");
        }
    }

    public class ShipVehicle : Vehicle
    {
        public ShipVehicle()
        {
            Type = VehicleType.Ship.ToString();
        }

        public override string Type { get; set; }

        public override void Delivery()
        {
            Console.WriteLine($"{Type} is used for delivery.");
        }
    }

    public class AirplaneVehicle : Vehicle
    {
        public AirplaneVehicle()
        {
            Type = VehicleType.Airplane.ToString();
        }

        public override string Type { get; set; }

        public override void Delivery()
        {
            Console.WriteLine($"{Type} is used for delivery.");
        }
    }

    public interface IVehicleFactory
    {
        Vehicle NewTruck();
        Vehicle NewShip();
        Vehicle NewAirplane();
    }

    public class VehicleFactory : IVehicleFactory
    {
        public Vehicle NewAirplane()
        {
            return new AirplaneVehicle();
        }

        public Vehicle NewShip()
        {
            return new ShipVehicle();
        }

        public Vehicle NewTruck()
        {
            return new TruckVehicle();
        }
    }
    //End 1. Factory Pattern
}
