using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal abstract class Person
    {
        private static uint maxId = 1;
        private int id;
        public int Id
        {
            get { return id; }
        }

        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
        }
        public int Age
        {
            get
            {
                DateTime now = DateTime.Now;
                int numberOfYears = now.Year - this.birthDate.Year;
                if (now.Month < this.birthDate.Month || now.Month == this.birthDate.Month && now.Day < this.birthDate.Day)
                {
                    numberOfYears--;
                }
                return numberOfYears;
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private static List<Person> allPersons = new List<Person>();
        public static ImmutableList<Person> AllPersons
        {
            get { return allPersons.ToImmutableList<Person>(); }
        }

        public Person(string _name, DateTime _birthDate)
        {
            this.id = (int)maxId;
            maxId++;
            this.Name = _name;
            this.birthDate = _birthDate;
            allPersons.Add(this);
        }

        public abstract string GenerateNameCard();
        public abstract double DetermineWorkload();

        public override bool Equals(Object obj)
        {
            if (obj is null)
            {
                return false;
            }
            else if (!(obj is Person))
            {
                return false;
            }
            else
            {
                return ((Person)obj).Id == this.Id;
            }
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

        public override string ToString()
        {
            return "Persoon\n" +
                "-------\n" +
                "Naam: " + this.Name + "\n" +
                "Leeftijd: " + this.Age;
        }

    }
}
