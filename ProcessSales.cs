using System.Windows.Forms;

namespace EmployeeSales
{

    public enum EmployeeType
    {
        Employee,
        Manager,
        Executive
    }


    public partial class Form1 : Form
    {

        private List<Employee> employees = new List<Employee>();
        public Form1()
        {
            InitializeComponent();

            EmBox.Items.Add("Employee");
            EmBox.Items.Add("Manager");
            EmBox.Items.Add("Executive");

             ClearList.Click += ClearList_Click;
            UpdateList.Click += UpdateList_Click;
        }

        private void UpdateList_Click(object sender, EventArgs e)
        {
            UpdateEmployeeList();

        }






                private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AddList_Click(object sender, EventArgs e)
        {

            string name = TxtNameBox.Text;
            double salary = Convert.ToDouble(TxtSalBox.Text);
            double yearsalary = Convert.ToDouble(TxtYearlySalBox.Text);

            EmployeeType type = (EmployeeType)EmBox.SelectedIndex;
            Employee employee = CreateEmployee(type, name, salary, yearsalary);


            employees.Add(employee);
            UpdateEmployeeList();

        }


        private void UpdateEmployeeList()
        {
            LsBox.Items.Clear();

            foreach (Employee employee in employees)
            {
                string selectedRole = EmBox.SelectedItem.ToString();
                LsBox.Items.Add($"{employee.Name} - {selectedRole} - {employee.Salary} - {employee.YearlySalary}");
            }


        }

        private Employee CreateEmployee(EmployeeType type, string name, double salary, double yearlySalary)
        {
            switch (type)
            {
                case EmployeeType.Employee:
                    return new Employee(name, salary, yearlySalary);
                case EmployeeType.Manager:
                    return new Manager(name, salary, yearlySalary);
                case EmployeeType.Executive:
                    return new Executive(name, salary, yearlySalary);
                default:
                    throw new ArgumentException("Invalid employee type.");
            }
        }


        public class Employee
        {
            public string Name { get; set; }
            public double Salary { get; protected set; }
            public double YearlySalary { get; protected set; }

            public Employee(string name, double salary, double yearlySalary)
            {
                Name = name;
                Salary = salary;
                YearlySalary = yearlySalary;
            }


        }



        public class Manager : Employee
        {
            public Manager(string name, double salary, double yearlySalary) : base(name, salary, yearlySalary)
            {
            }
        }

        public class Executive : Manager
        {
            public Executive(string name, double salary, double yearlySalary) : base(name, salary, yearlySalary)
            {
            }
        }

        private void ClearList_Click(object sender, EventArgs e)
        {
            LsBox.Items.Clear();

          
            employees.Clear();
        }
    }
}




// Executive.cs class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSales
{
    internal class Executive : Employee
    {
        public Executive(string name, double salary) : base(name, salary)
        {
        }
        public override string ToString()
        {
            return $"{Name} (Executive) - ${Salary:N2}";
        }
    }
}


// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSales
{


    internal class Manager : Employee
    {
        public List<Employee> Subordinates { get; private set; }

        public Manager(string name, double salary) : base(name, salary)
        {
            Subordinates = new List<Employee>();
        }

        public override void UpdateSalary(double newSalary)
        {
            base.UpdateSalary(newSalary);
            foreach (var subordinate in Subordinates)
            {
                subordinate.UpdateSalary(newSalary);
            }
        }
        public void AddSubordinate(Employee subordinate)
        {
            Subordinates.Add(subordinate);
            subordinate.DirectManager = this;
        }

        public override string ToString()
        {
            return $"{Name} (Manager) - ${Salary:N2}";
        }
    }

}









using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSales
{
    internal class Employee
    {
        public string Name { get; set; }
        public double Salary { get; protected set; }
        public Manager DirectManager { get; set; }
        public Employee(string name, double salery)
        {

            Name = name;
            Salary = salery;

        }

        public virtual void UpdateSalary(double newSalary) 
        {
        
            Salary += newSalary;

        
        }

        public override string ToString()
        {
            return $"{Name} - ${Salary:N2}";

        }
    }

    
   
}














