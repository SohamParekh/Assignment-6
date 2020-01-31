using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_6
{
    public class Program
    {
        IList<Employee> employeeList;
        IList<Salary> salaryList;
        public Program()
        {
            

             employeeList = new List<Employee>()
            {
            new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
            new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
            new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
            new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
            new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
            new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
            new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
        };

            salaryList = new List<Salary>() {
            new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
        };
        }

        public static void Main()
        {
            Program program = new Program();

            program.Task1();

            program.Task2();

            program.Task3();
        }

        public void Task1()
        {
            Console.WriteLine("\tEmployee with their Total Salary :\n");
            
            var query1 = employeeList
                            .GroupJoin(salaryList,e => e.EmployeeID,s => s.EmployeeID,
                                        (e, salaryList) => new {
                                            Fname = e.EmployeeFirstName,
                                            Lname = e.EmployeeLastName,
                                            sal = salaryList.Sum(s => s.Amount)
                                        }).OrderBy(e => e.sal);
            
            foreach (var result in query1)
            {
                Console.WriteLine($"Name : {result.Fname,-8} {result.Lname,-15} Salary : {result.sal}");
            }
        }

        public void Task2()
        {
            var query = employeeList.OrderByDescending(e => e.Age)
                                    .Skip(1)
                                    .Join(salaryList,e => e.EmployeeID,s => s.EmployeeID,
                                    (e,s) => new {
                                        FName = e.EmployeeFirstName,
                                        LName = e.EmployeeLastName,
                                        age = e.Age,
                                        salary = s.Amount
                                    });
            Console.WriteLine("\n\n\tEmployee details with 2nd oldest Age : \n");
            foreach (var item in query.Take(1))
            {
                Console.WriteLine($"Name : {item.FName,-8} {item.LName,-10} Age : {item.age,-10} Salary : {item.salary}");
            }
        }

        public void Task3()
        {
            Console.WriteLine("\n\n\tMean of Salary whose Age is Greater than 30 : \n");
            var query1 = employeeList
                            .Where(e => e.Age > 30)
                            .GroupJoin(salaryList, e => e.EmployeeID, s => s.EmployeeID,
                                        (e, salaryList) => new {
                                            Fname = e.EmployeeFirstName,
                                            Lname = e.EmployeeLastName,
                                            average = salaryList.Average(s => s.Amount)
                                        });
            foreach (var result in query1)
            {
                Console.WriteLine($"Name : {result.Fname,-8} {result.Lname,-15} Salary : {result.average:n2}");
            }
        }
    }

    public enum SalaryType
    {
        Monthly,
        Performance,
        Bonus
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public int Age { get; set; }
    }

    public class Salary
    {
        public int EmployeeID { get; set; }
        public int Amount { get; set; }
        public SalaryType Type { get; set; }
    }
}
