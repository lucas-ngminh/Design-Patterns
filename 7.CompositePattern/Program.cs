using System;
using System.Collections.Generic;

namespace _7.CompositePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee ftEmp0 = new Fulltime("Hoang", "Fulltime Employee", 145000.00);

            Console.WriteLine();
            Console.WriteLine("---Add Employee Then Display---");

            Employee ftEmp1 = new Fulltime("Rachel", "Fulltime Employee", 90000.00);
            Employee ctEmp1 = new Contractor("Lu", "Contractor Employee", 72.00);
            Employee iEmp1 = new Intern("Shawn", "Intern Employee", 15.00, 3, 7);

            ftEmp0.Add(ftEmp1);
            ftEmp0.Add(ctEmp1);
            ftEmp0.Add(iEmp1);

            Employee ftEmp2 = new Fulltime("Yong", "Fulltime Employee", 82000.00);
            Employee ctEmp2 = new Contractor("Lucas", "Contractor Employee", 45.00);

            ftEmp1.Add(ctEmp2);
            ctEmp1.Add(ftEmp2);

            ftEmp0.Display(1);

            Console.WriteLine();
            Console.WriteLine("---Remove Employee Then Display---");

            ftEmp0.Remove(ftEmp1);

            ftEmp0.Display(1);
        }
    }

    public interface IEmployeeComponent
    {
        void Add(Employee e);
        void Remove(Employee e);
        void Display(int depth);
    }

    public abstract class Employee : IEmployeeComponent
    {
        public string Name { get; set; }
        public string Title { get; set; }

        public List<Employee> _children { get; set; } = new List<Employee>();

        public Employee(string name, string title)
        {
            Name = name;
            Title = title;
        }

        public abstract double GetMonthlySalary();

        public void Add(Employee e)
        {
            _children.Add(e);
        }

        public void Remove(Employee e)
        {
            _children.Remove(e);
        }

        public void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + $"{Name} " +
                $"is a {Title} who earns " +
                $"{GetMonthlySalary().ToString("F2")} monthly.");

            // Recursively display child nodes

            foreach (Employee emp in _children)
            {
                emp.Display(depth + 2);
            }
        }
    }

    public class Fulltime : Employee
    {
        public double AnnualRate { get; set; }

        public Fulltime(string name, string title, double annualRate)
            : base(name, title)
        {
            AnnualRate = annualRate;
        }

        public override double GetMonthlySalary()
        {
            return AnnualRate / 12;
        }
    }

    public class Contractor : Employee
    {
        public double HourlyRate { get; set; }

        public Contractor(string name, string title, double hourlyRate)
            : base(name, title)
        {
            HourlyRate = hourlyRate;
        }

        public override double GetMonthlySalary()
        {
            return HourlyRate * 8 * 5 * 4;
        }
    }

    public class Intern : Employee
    {
        public double HourlyRate { get; set; }
        public double WorkingDaysPerWeek { get; set; }
        public double HoursPerDay { get; set; }

        public Intern(string name, string title, double hourlyRate, double workingDaysPerWeek, double hoursPerDay)
            : base(name, title)
        {
            HourlyRate = hourlyRate;
            WorkingDaysPerWeek = workingDaysPerWeek;
            HoursPerDay = hoursPerDay;
        }

        public override double GetMonthlySalary()
        {
            return HourlyRate * HoursPerDay * WorkingDaysPerWeek * 4;
        }
    }
}
