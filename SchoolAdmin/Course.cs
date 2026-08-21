using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal class Course
    {

        public string Title;

        public ImmutableList<Student> Students
        {
            get
            {
                var builder = ImmutableList.CreateBuilder<Student>();
                foreach (var registration in CourseRegistrations)
                {
                    if (registration.Course == this)
                    {
                        builder.Add(registration.Stud);
                    }
                }
                return builder.ToImmutableList();
            }
        }

        private byte creditPoints;
        public byte CreditPoints
        {
            get { return creditPoints; }
            private set { creditPoints = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
        }
        private static int maxId = 1;
        private static List<Course> allCourses = new List<Course>();
        public static ImmutableList<Course> AllCourses
        {
            get
            {
                return allCourses.ToImmutableList<Course>();
            }
        }

        public ImmutableList<CourseRegistration> CourseRegistrations
        {
            get
            {
                var builder = ImmutableList.CreateBuilder<CourseRegistration>();
                foreach (var registration in CourseRegistration.AllCourseRegistrations)
                {
                    if (registration.Course is Course c && c == this)
                    {
                        builder.Add(registration);
                    }
                }
                return builder.ToImmutableList();

            }
        }

        public Course(string title, byte creditPoints)
        {
            this.Title = title;
            this.id = Course.maxId;
            Course.maxId++;
            this.CreditPoints = creditPoints;
            Course.allCourses.Add(this);
        }
        public Course(string title)
        {
            this.Title = title;
            this.id = Course.maxId;
            Course.maxId++;
            this.CreditPoints = 3; // standaard
            Course.allCourses.Add(this);
        }

        public void ShowOverview()
        {
            Console.WriteLine($"{this.Title}\t({this.Id})\t({this.CreditPoints}stp)");
            foreach (Student student in Students)
            {
                Console.WriteLine($"{student.Name}");
            }
        }
        public static Course SearchCourseById(int id)
        {
            foreach (Course course in AllCourses)
            {
                if (id == course.Id)
                {
                    return course;
                }
            }
            return null;
        }

        public override bool Equals(Object obj)
        {
            if (obj is null)
            {
                return false;
            } else if (!(obj is Course))
            {
                return false;
            }
            else
            {
                return ((Course)obj).Id == this.Id;
            }
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
