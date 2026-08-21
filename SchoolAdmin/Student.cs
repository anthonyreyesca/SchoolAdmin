using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SchoolAdmin
{
    internal class Student : Person
    {
        public static ImmutableList<Student> AllStudents
        {
            get 
            {
                List<Student> students = new List<Student>();
                foreach (Person p in Person.AllPersons)
                {
                    if (p is Student s)
                    {
                        students.Add(s);
                    }
                }
                return students.ToImmutableList<Student>();
            }
        }

        private Dictionary<DateTime, string> studentFile = new Dictionary<DateTime, string>();
        public ImmutableDictionary<DateTime, string> StudentFile
        {
            get
            {
                return studentFile.ToImmutableDictionary<DateTime, string>();
            }
        }

        public ImmutableList<CourseRegistration> CourseRegistrations
        {
            get
            {
                var builder = ImmutableList.CreateBuilder<CourseRegistration>();
                foreach (CourseRegistration courseRegistration in CourseRegistration.AllCourseRegistrations)
                {
                    if (courseRegistration.Stud is Student s && s == this)
                    {
                        builder.Add(courseRegistration);
                    }
                }
                return builder.ToImmutableList();
            }
        }

        public ImmutableList<Course> Courses
        {
            get
            {
                var builder = ImmutableList.CreateBuilder<Course>();
                foreach (CourseRegistration registration in CourseRegistrations)
                {
                    if (registration.Course != null && !builder.Contains(registration.Course))
                    {
                        builder.Add(registration.Course);
                    }
                }
                return builder.ToImmutableList();
            }
        }

        public Student(string name, DateTime birthdate) : base(name, birthdate)
        {
        }
        public override string GenerateNameCard()
        {
            return $"{this.Name} (STUDENT)";
        }

        public override double DetermineWorkload()
        {
            double total = 0;
            foreach (CourseRegistration course in CourseRegistrations)
            {
                if (course is not null)
                {
                    total += 10;
                }
            }
            return total;
        }

        public void RegisterCourseResult(Course course, byte? result)
        {
            if (result > 20)
            {
                Console.WriteLine("Ongeldig cijfer!");
            }
            else
            {
                CourseRegistration newCourseresult = new CourseRegistration(course, result, this);
            }
        }
        public double Average()
        {
            double totaal = 0;
            int counter = 0;
            foreach (CourseRegistration item in CourseRegistrations)
            {
                if (!(item.Result is null))
                {
                    totaal += (byte)item.Result;
                    counter++;
                }
            }
            return totaal / counter;
        }

        public void ShowOverview()
        {
            Console.WriteLine($"{this.Name} ({this.Age} jaar)");
            Console.WriteLine($"Werkbelasting: {DetermineWorkload()} uren");
            Console.WriteLine("Cijferrapport");
            Console.WriteLine("*************");
            foreach (CourseRegistration item in CourseRegistrations)
            {
                Console.WriteLine($"{item.Course.Title}:\t{item.Result}");
            }
            Console.WriteLine($"Gemiddelde:\t{this.Average():F1}\n");
        }

        public override string ToString()
        {
            return base.ToString() + 
                "\nStudent";
        }
    }
}
