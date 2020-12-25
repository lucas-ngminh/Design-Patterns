using System;

namespace _8.DecoratorPattern
{
class Program
{
    static void Main(string[] args)
    {
        //1. The original system doesn't allow to order customized Pizza
        Console.WriteLine("Ordering original Pizzas");
        Pizza pPizza = new Peperonni();
        Console.WriteLine(pPizza.ToString());

        Pizza sPizza = new Sausage();
        Console.WriteLine(sPizza.ToString());

        Pizza mPizza = new Mushroom();
        Console.WriteLine(mPizza.ToString());
        Console.WriteLine();

        //2. After enhancing the system, can order customized pizza now
        Console.WriteLine("Ordering customized Pizzas");
        Pizza myPizza = new Peperonni();
        Pizza mushroom = new MushroomTopping(myPizza);
        Pizza extraCheaseMushroom = new ExtraCheeseTopping(mushroom);
        Console.WriteLine(extraCheaseMushroom.ToString());
    }
}

    //1. Existing Classes
    public abstract class Pizza
    {
        public string Name { get; set; }
        protected double Price { get; set; }

        public virtual double GetPrice()
        {
            return Price;
        }

        public override string ToString()
        {
            return $"This { Name } pizza cost ${ GetPrice() }";
        }
    }

    public class Peperonni : Pizza
    {
        public Peperonni()
        {
            Name = "Peperonni";
            Price = 8.99;
        }
    }

    public class Sausage : Pizza
    {
        public Sausage()
        {
            Name = "Sausage";
            Price = 9.99;
        }
    }

    public class Mushroom : Pizza
    {
        public Mushroom()
        {
            Name = "Mushroom";
            Price = 6.99;
        }
    }

    //2. Adding feature for customizing Pizza
    public abstract class ToppingsDecorator : Pizza
    {
        public Pizza BasePizza { get; set; }

        public ToppingsDecorator(Pizza pizzaToDecorate)
        {
            BasePizza = pizzaToDecorate;
        }

        public override double GetPrice()
        {
            return (BasePizza.GetPrice() + this.Price);
        }
    }

    public class ExtraCheeseTopping : ToppingsDecorator
    {
        public ExtraCheeseTopping(Pizza pizzaToDecorate)
            : base(pizzaToDecorate)
        {
            Name = $"Extra Cheese {pizzaToDecorate.Name}";
            Price = 0.99;
        }
    }

    public class MushroomTopping : ToppingsDecorator
    {
        public MushroomTopping(Pizza pizzaToDecorate)
            : base(pizzaToDecorate)
        {
            Name = $"Mushroom {pizzaToDecorate.Name}";
            Price = 1.49;
        }
    }

    public class PepperoniTopping : ToppingsDecorator
    {
        public PepperoniTopping(Pizza pizzaToDecorate)
            : base(pizzaToDecorate)
        {
            Name = $"Pepperoni {pizzaToDecorate.Name}";
            Price = 1.49;
        }
    }

    public class SausageTopping : ToppingsDecorator
    {
        public SausageTopping(Pizza pizzaToDecorate)
            : base(pizzaToDecorate)
        {
            Name = $"Sausage {pizzaToDecorate.Name}";
            Price = 1.49;
        }
    }
}
