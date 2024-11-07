using System;
using System.Collections.Generic;
using System.Linq;

public interface IOrganizationComponent
{
    string Name { get; }
    decimal GetBudget();
    int GetEmployeeCount();
    void Display(int depth = 0);
}


public class Employee : IOrganizationComponent
{
    public string Name { get; private set; }
    public decimal Salary { get; private set; }

    public Employee(string name, decimal salary)
    {
        Name = name;
        Salary = salary;
    }

    public decimal GetBudget() => Salary;
    public int GetEmployeeCount() => 1;

    public void Display(int depth = 0)
    {
        Console.WriteLine(new string('-', depth) + $" {Name}, Salary: {Salary}");
    }
}


public class Department : IOrganizationComponent
{
    private readonly List<IOrganizationComponent> _components = new List<IOrganizationComponent>();
    public string Name { get; private set; }

    public Department(string name)
    {
        Name = name;
    }

    public void Add(IOrganizationComponent component) => _components.Add(component);
    public void Remove(IOrganizationComponent component) => _components.Remove(component);

    public decimal GetBudget() => _components.Sum(c => c.GetBudget());
    public int GetEmployeeCount() => _components.Sum(c => c.GetEmployeeCount());

    public void Display(int depth = 0)
    {
        Console.WriteLine(new string('-', depth) + $" Department: {Name}");
        foreach (var component in _components)
            component.Display(depth + 2);
    }
}


public class Program
{
    public static void Main()
    {
        var department = new Department("Head Office");
        var hr = new Department("HR Department");
        var it = new Department("IT Department");

        hr.Add(new Employee("Alice", 3000));
        hr.Add(new Employee("Bob", 3500));

        it.Add(new Employee("Charlie", 5000));
        it.Add(new Employee("David", 4500));

        department.Add(hr);
        department.Add(it);

        department.Display();
        Console.WriteLine($"Total Budget: {department.GetBudget()}");
        Console.WriteLine($"Total Employees: {department.GetEmployeeCount()}");
    }
}

