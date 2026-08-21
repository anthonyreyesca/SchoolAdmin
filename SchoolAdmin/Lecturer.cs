using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal class Lecturer : Employee
    {
        public Dictionary<Course, double> Courses = new Dictionary<Course, double>();
        public static ImmutableList<Lecturer> AllLecturers
        {
            get
            {
                List<Lecturer> lecturer = new List<Lecturer>();
                foreach (Person p in Person.AllPersons)
                {
                    if (p is Lecturer l)
                    {
                        lecturer.Add(l);
                    }
                }
                return lecturer.ToImmutableList<Lecturer>();
            }
        }

        public Lecturer(string name, DateTime birthDate, Dictionary<string, byte> tasks)
            : base(name, birthDate, tasks)
        {
            foreach (var task in tasks)
            {
                Course course = null;

                // Zoek een bestaande cursus met dezelfde titel (case-insensitive)
                foreach (var existingCourse in Course.AllCourses)
                {
                    if (existingCourse.Title.ToLower() == task.Key.ToLower())
                    {
                        course = existingCourse;
                        break; // gevonden → stop met zoeken
                    }
                }
                
                // Als niet gevonden, maak een nieuwe aan
                if (course == null)
                {
                    course = new Course(task.Key);
                }

                // Voeg toe aan de dictionary van deze lecturer
                this.Courses[course] = task.Value;
            }
        }

        public override double DetermineWorkload()
        {
            double total = 0;
            foreach (var task in this.Tasks)
            {
                total += task.Value;
            }
            return total;
        }
        public override uint CalculateSalary()
        {
            double hoursWorked = this.DetermineWorkload();
            byte yearsWorked = this.Seniority;
            double extraSalary = (yearsWorked / 4) * 120;
            double hourlyRateSalary = (2200 + extraSalary) / 40;
            uint salary = (uint)(hourlyRateSalary * hoursWorked);
            return salary;
        }

        public override string GenerateNameCard()
        {
            string nameCard = string.Empty;
            nameCard = this.Name + "\n" + "Lector voor:";
            foreach (var task in this.Tasks)
            {
                nameCard += "\n" + task.Key;
            }
            return nameCard;
        }

        public override string ToString()
        {
            return base.ToString() +
            "\nAdminitratief personeel";
        }
    }
}
