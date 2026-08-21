using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal abstract class Employee : Person
    {
        private byte seniority;
        public byte Seniority
        {
            get { return seniority; }
            set
            {
                this.seniority = Math.Min((byte)50, value);
            }
        }

        private Dictionary<string, byte> tasks = new Dictionary<string, byte>();
        public ImmutableDictionary<string, byte> Tasks
        {
            get { return tasks.ToImmutableDictionary<string, byte>(); }
        }

        public static ImmutableList<Employee> AllEmployees
        {
            get {
                List<Employee> employees = new List<Employee>();
                foreach (Person p  in Person.AllPersons)
                {
                    if (p is Employee e)
                    {
                        employees.Add(e);
                    }
                }
                return employees.ToImmutableList<Employee>();
            }
        }

        public Employee(string name, DateTime birthDate, Dictionary<string, byte> tasks) : base(name, birthDate)
        {
            if (!(tasks is null))
            {
                foreach (var item in tasks)
                {
                    this.tasks.Add(item.Key, item.Value);
                }
            }
        }

        public abstract uint CalculateSalary();
    }
}
