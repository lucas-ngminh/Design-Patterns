using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1.BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //0. Without a Builder
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<html><p>");
            sb.Append(hello);
            sb.Append("</p></html>");
            Console.WriteLine(sb);
            sb.Clear();

            var words = new[] { "hello", "world" };
            sb.Append("<html><ul>");
            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul></html>");
            Console.WriteLine(sb);

            //1. With an HTML Builder
            var pBuilder = new HTMLBuilder("");
            pBuilder.AddChild("p", "hello world");
            Console.WriteLine(pBuilder.ToString());

            var ulBuilder = new HTMLBuilder("ul");
            ulBuilder.AddChild("li", "hello").AddChild("li", "world");
            Console.WriteLine(ulBuilder.ToString());

            //2. Functional Builder
            var person = new PersonBuilder()
                .Called("Lucas")
                .WorkAs("Developer")
                .Build();

            //3. Faceted Builder
            var employee = new EmployeeBuilder()
                .Lives.At("Avenue O")
                      .In("Brooklyn")
                      .WithPostalCode("11333")
                .Works.At("Lucasology")
                      .AsA("Developer")
                      .Earning(100000);
        }
    }

    //1. HTML Builder: 
    //Create a product class - HTMLElement
    public class HTMLElement
    {
        public string Name { get; set; } 

        public string Text { get; set; }

        public List<HTMLElement> Elements = new List<HTMLElement>();
    }

    public class HTMLBuilder
    {
        private const int indentSize = 2;
        private HTMLElement rootElement;

        public HTMLBuilder(string rootName)
        {
            rootElement = new HTMLElement();
            rootElement.Name = rootName;
        }

        public HTMLBuilder AddChild(string name, string text)
        {
            rootElement.Elements.Add(new HTMLElement()
            {
                Name = name,
                Text = text
            });

            return this;
        }

        public string ToStringImpl(HTMLElement root, int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            if(!string.IsNullOrEmpty(root.Name))
                sb.AppendLine($"{i}<{root.Name}>");

            if (!string.IsNullOrEmpty(root.Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(root.Text);
            }

            foreach (var e in root.Elements)
            {
                sb.Append(ToStringImpl(e, string.IsNullOrEmpty(root.Name) ? indent : indent + 1));
            }

            if (!string.IsNullOrEmpty(root.Name))
                sb.AppendLine($"{i}</{root.Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(rootElement, 0);
        }
    }
    //End HTML Builder

    //2. Functional Builder: PersonBuilder
    public class Person
    {
        public string Name, Position;
    }

    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject: new()
    {
        private readonly List<Func<TSubject, TSubject>> actions
            = new List<Func<TSubject, TSubject>>();

        public TSelf Do(Action<TSubject> action)
            => AddAction(action);

        public TSubject Build()
            => actions.Aggregate(new TSubject(), (p, f) => f(p));

        private TSelf AddAction(Action<TSubject> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });
            return (TSelf)this;
        }
    }

    public sealed class PersonBuilder
        : FunctionalBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name)
            => Do(p => p.Name = name);
    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorkAs
            (this PersonBuilder builder, string position)
            => builder.Do(p => p.Position = position);
    }

    ////Without Open-Closed Principle
    //public sealed class PersonBuilder
    //{
    //    private readonly List<Func<Person, Person>> actions
    //        = new List<Func<Person, Person>>();

    //    public PersonBuilder Called(string name)
    //        => Do(p => p.Name = name);

    //    public PersonBuilder WorkAs(string position)
    //        => Do(p => p.Position = position);

    //    public PersonBuilder Do(Action<Person> action)
    //        => AddAction(action);

    //    public Person Build()
    //        => actions.Aggregate(new Person(), (p, f) => f(p));

    //    private PersonBuilder AddAction(Action<Person> action)
    //    {
    //        actions.Add(p =>
    //        {
    //            action(p);
    //            return p;
    //        });
    //        return this;
    //    }
    //}
    //End Functional Builder

    //3. Faceted Builder
    public class Employee
    {
        public string StreetAddress, PostalCode, City;

        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class EmployeeBuilder //facade
    {
        //reference
        protected Employee emp = new Employee();

        public EmployeeAddressBuilder Lives => new EmployeeAddressBuilder(emp);
        public EmployeeJobBuilder Works => new EmployeeJobBuilder(emp);
    }

    public class EmployeeAddressBuilder : EmployeeBuilder
    {
        public EmployeeAddressBuilder(Employee emp)
        {
            this.emp = emp;
        }

        public EmployeeAddressBuilder At(string streetAddress)
        {
            emp.StreetAddress = streetAddress;
            return this;
        }

        public EmployeeAddressBuilder In(string city)
        {
            emp.City = city;
            return this;
        }

        public EmployeeAddressBuilder WithPostalCode(string postalCode)
        {
            emp.PostalCode = postalCode;
            return this;
        }
    }

    public class EmployeeJobBuilder : EmployeeBuilder
    {
        public EmployeeJobBuilder(Employee emp)
        {
            this.emp = emp;
        }

        public EmployeeJobBuilder At(string companyName)
        {
            emp.CompanyName = companyName;
            return this;
        }

        public EmployeeJobBuilder AsA(string position)
        {
            emp.Position = position;
            return this;
        }

        public EmployeeJobBuilder Earning(int amount)
        {
            emp.AnnualIncome = amount;
            return this;
        }
    }
    //End Faceted Builder
}
