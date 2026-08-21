using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal class CourseRegistration
    {
        private Course course;
        public Course Course
        {
            get { return course; }
            set { course = value; }
        }
        private byte? result;
        public byte? Result
        {
            get
            {
                return result;
            }
            set
            {
                if (!(value is null) && !(value > 20))
                {
                    result = value;
                }
            }
        }

        private static List<CourseRegistration> allCourseRegistrations = new List<CourseRegistration>();

        public static ImmutableList<CourseRegistration> AllCourseRegistrations
        {
            get
            {
                return allCourseRegistrations.ToImmutableList<CourseRegistration>();
            }
        }

        private Student stud;

        public Student Stud
        {
            get
            {
                return stud;
            }
        }

        public CourseRegistration(Course course, byte? result, Student stud)
        {
            this.Course = course;
            this.Result = result;
            this.stud = stud;
            allCourseRegistrations.Add(this);
        }

    }
}
