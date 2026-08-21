using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal class StudyProgram
    {
        private string name;
        public string Name
        {
            get { return name; }
        }
        //private List<Course> courses = new List<Course>();
        //public ImmutableList<Course> Courses
        //{
        //    get
        //    {
        //        return courses.ToImmutableList<Course>();
        //    }
        //}

        private Dictionary<List<Course>, byte> courses = new Dictionary<List<Course>, byte>();
        public ImmutableDictionary<List<Course>, byte> Courses
        {
            get
            {
                return courses.ToImmutableDictionary();
            }
        }

        public StudyProgram(string name)
        {
            this.name = name;
        }

        public void ShowOverview()
        {
            Console.WriteLine($"Programma: {Name}");
            foreach (var vkp in Courses.OrderBy(c => c.Value))
            {
                List<Course> _courses = vkp.Key;
                byte semester = vkp.Value;
                Console.WriteLine("Semester: " + semester);
                foreach (var course in _courses)
                {
                    course.ShowOverview();
                }
                Console.WriteLine();
            }
        }
        public static void DemoStudyProgram()
        {
            //bug 1 en oplossing
            //Course communicatie = new Course("Communicatie");
            //Course programmeren = new Course("Programmeren");
            //Course databanken = new Course("Databanken");
            //List<Course> coursesProgrammeren = new List<Course>() { communicatie, programmeren, databanken };
            ////aanpassing na eerste bug: nieuwe courses
            //List<Course> coursesSNB = new List<Course>() { communicatie, programmeren, databanken };
            //StudyProgram programmerenProgram = new StudyProgram("Programmeren");
            //StudyProgram snbProgram = new StudyProgram("Systeem- en netwerkbeheer");
            //programmerenProgram.Courses = coursesProgrammeren;
            //snbProgram.Courses = coursesSNB;
            ////we willen hieronder Databanken schrappen uit het programma SNB
            //snbProgram.Courses.Remove(databanken);
            //programmerenProgram.ShowOverview();
            //snbProgram.ShowOverview();

            //bug 2 en oplossing
            //Course communicatie = new Course("Communicatie");
            //Course programmeren = new Course("Programmeren");
            //Course databanken = new Course("Databanken");
            //List<Course> coursesProgrammeren = new List<Course>() { communicatie, programmeren, databanken };
            //List<Course> coursesSNB = new List<Course>() { communicatie, programmeren, databanken };
            //StudyProgram programmerenProgram = new StudyProgram("Programmeren");
            //StudyProgram snbProgram = new StudyProgram("Systeem- en netwerkbeheer");
            //programmerenProgram.Courses = coursesProgrammeren;
            //snbProgram.Courses = coursesSNB;
            ////we willen hieronder Databanken schrappen uit het programma SNB
            //snbProgram.Courses.Remove(databanken);
            ////voor SNB wordt de titel van de cursus Programmeren veranderd naar "Scripting"
            //snbProgram.Courses[1].Title = "Scripting";
            //programmerenProgram.ShowOverview();
            //snbProgram.ShowOverview();

            //bug 2  oplossing
            Course communicatie = new Course("Communicatie");
            Course programmeren = new Course("Programmeren");
            Course databanken = new Course("Databanken");
            Course scripting = new Course("programmeren");
            List<Course> crsProgrammeren = [communicatie, programmeren, databanken, scripting];
            List<Course> crsSNB = [programmeren, databanken, scripting];
            StudyProgram programmerenProgram = new StudyProgram("Programmeren");
            StudyProgram snbProgram = new StudyProgram("Systeem- en netwerkbeheer");
            //Wijziging van COurses naar courses
            programmerenProgram.courses.Add(crsProgrammeren, 1);
            snbProgram.courses.Add(crsSNB, 1);
            snbProgram.courses.Add([communicatie], 2);
            //voor SNB wordt de titel van de cursus Programmeren veranderd naar "Scripting"
            foreach (var courses in snbProgram.courses.Keys)
            {
                foreach (var course in courses)
                {
                    if (course.Title == "programmeren")
                    {
                        course.Title = "Scripting";
                    }
                }
            }
            programmerenProgram.ShowOverview();
            snbProgram.ShowOverview();
        }
    }
}
