using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;

namespace SchoolAdmin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Wat wil je doen?");
                Console.WriteLine("1. DemonstreerStudenten uitvoeren");
                Console.WriteLine("2. DemonstreerCursussen uitvoeren");
                Console.WriteLine("3. Student uit tekstformaat inlezen");
                Console.WriteLine("4. DemonstreerStudieProgramma uitvoeren");
                Console.WriteLine("5. DemoAdministrativePersonnel");
                Console.WriteLine("6. DemoLecturers");
                Console.WriteLine("7. Student toevoegen");
                Console.WriteLine("8. Cursus toevoegen");
                Console.WriteLine("9. VakInschrijving toevoegen");
                Console.WriteLine("10. Inschrijvingsgegevens toenen");
                int keuze = Convert.ToInt32(Console.ReadLine());
                switch (keuze)
                {
                    case 1:
                        DemoStudents();
                        break;
                    case 2:
                        DemoCourses();
                        break;
                    case 3:
                        ReadTextFormatStudent();
                        break;
                    case 4:
                        StudyProgram.DemoStudyProgram();
                        break;
                    case 5:
                        DemoAdministrativePersonnel();
                        break;
                    case 6:
                        DemoLecturers();
                        break;
                    case 7:
                        AddStudent();
                        break;
                    case 8:
                        AddCourse();
                        break;
                    case 9:
                        AddCourseRegistration();
                        break;
                    case 10:
                        ShowCourseRegistrations();
                        break;
                    default:
                        break;
                }
            }

        }

        public static void DemoStudents()
        {
            Course communicatie = new Course("Communicatie");
            Course programmeren = new Course("Programmeren");
            Course webtechnologie = new Course("Webtechnologie");
            Course databanken = new Course("Databanken");

            Student said = new Student("Said Aziz", new DateTime(2000, 6, 1));
            said.RegisterCourseResult(communicatie, 12);
            said.RegisterCourseResult(programmeren, null);
            said.RegisterCourseResult(webtechnologie, 13);
            said.ShowOverview();

            Student mieke = new Student("Mieke Vermeulen", new DateTime(1998, 1, 1));
            mieke.RegisterCourseResult(communicatie, 13);
            mieke.RegisterCourseResult(programmeren, 16);
            mieke.RegisterCourseResult(databanken, 14);
            mieke.ShowOverview();
        }
        public static void DemoCourses()
        {
            Student said = new Student("Said Aziz", new DateTime(2000, 6, 1));
            Student mieke = new Student("Mieke Vermeulen", new DateTime(1998, 1, 1));

            Course communicatie = new Course("Communicatie", 6);
            Course programmeren = new Course("Programmeren");
            Course webtechnologie = new Course("Webtechnologie");
            Course databanken = new Course("Databanken");


            said.RegisterCourseResult(communicatie, 12);
            said.RegisterCourseResult(programmeren, null);
            said.RegisterCourseResult(webtechnologie, 13);

            mieke.RegisterCourseResult(communicatie, 13);
            mieke.RegisterCourseResult(programmeren, 16);
            mieke.RegisterCourseResult(databanken, 14);

            communicatie.ShowOverview();
            programmeren.ShowOverview();
            webtechnologie.ShowOverview();
            databanken.ShowOverview();
        }
        public static void ReadTextFormatStudent()
        {
            Console.WriteLine("Geef de tekstvoorstelling van 1 student in csv-formaat:");
            string csv = Console.ReadLine();
            string[] data = csv.Split(";");
            int day = Convert.ToInt32(data[1]);
            int month = Convert.ToInt32(data[2]);
            int year = Convert.ToInt32(data[3]);
            Student newStudent = new Student(data[0], new DateTime(year, month, day));
            for (int i = 4; i < data.Length; i += 2)
            {
                string courseInput = data[i];
                Course course = new Course(courseInput);
                if (!(Course.AllCourses.Contains(course)))
                {
                    byte result = Convert.ToByte(data[i + 1]);
                    newStudent.RegisterCourseResult(course, result);
                }
                else
                {
                    Course newCourse = new Course(courseInput);
                    byte result = Convert.ToByte(data[i + 1]);
                    newStudent.RegisterCourseResult(newCourse, result);

                }
            }
            newStudent.ShowOverview();
        }

        public static void DemoAdministrativePersonnel()
        {
            string name = "ahmed";
            Dictionary<string, byte> tasks = new Dictionary<string, byte>
            {
                {"roostering", 10},
                {"correspondentie", 10},
                {"animatie", 10}
            };
            AdministrativePersonnel ahmed = new AdministrativePersonnel(name, new DateTime(1998, 2, 4), tasks);
            ahmed.Seniority = 3;
            foreach (var personnel in AdministrativePersonnel.AllAdministrativePersonnel)
            {
                Console.WriteLine(personnel.GenerateNameCard());
                Console.WriteLine(personnel.CalculateSalary());
                Console.WriteLine(personnel.DetermineWorkload());
            }
        }

        public static void DemoLecturers()
        {
            Dictionary<string, byte> tasks = new Dictionary<string, byte>
            {
                {"Economie", 3 },
                {"Statistiek", 3 },
                {"Analystische Meetkunde", 4 }
            };
            Lecturer anna = new Lecturer("Anna", new DateTime(1975, 6, 12), tasks);
            anna.Seniority = 9;
            foreach (var lecturer in Lecturer.AllLecturers)
            {
                Console.WriteLine(lecturer.GenerateNameCard());
                Console.WriteLine(lecturer.CalculateSalary());
                Console.WriteLine(lecturer.DetermineWorkload());
            }

            foreach (var p in Person.AllPersons)
            {
                Console.WriteLine(p.ToString());
            }

        }

        public static void AddStudent()
        {
            Console.WriteLine("Naam van de student?");
            string studName = Console.ReadLine();
            Console.WriteLine("Geboortedatum van de student bv.1/1/1997");
            string studBirth = Console.ReadLine();
            Student newStudent = new Student(studName, DateTime.Parse(studBirth));
        }
        public static void AddCourse()
        {
            Console.WriteLine("Titel van de cursus?");
            string courseName = Console.ReadLine();
            Console.WriteLine("Aantal studiepunten?");
            byte amountOfStudPoints = Convert.ToByte(Console.ReadLine());
            Course newCourse = new Course(courseName, amountOfStudPoints);
        }
        public static void AddCourseRegistration()
        {
            Student selectedStud;
            Course selectedCrs;
            int stdNr;
            int crsNr;
            Console.WriteLine("Welke student?");
            for (int i = 0; i < Student.AllStudents.Count; i++)
            {
                Student student = Student.AllStudents[i];
                Console.WriteLine($"{i+1} {student.Name}");
            }
            stdNr = Convert.ToInt32(Console.ReadLine());
            selectedStud = Student.AllStudents[stdNr-1];
            Console.WriteLine("Welke cursus?");
            for (int i = 0; i < Course.AllCourses.Count; i++)
            {
                Course course = Course.AllCourses[i];
                Console.WriteLine($"{i+1} {course.Title}");
            }
            crsNr = Convert.ToInt32(Console.ReadLine());
            selectedCrs = Course.AllCourses[crsNr - 1];
            Console.WriteLine("Wil je een resultaat toekennen? Ja of Nee");
            string yn = Console.ReadLine();
            if (yn.ToLower() == "ja")
            {
                Console.WriteLine("Wat is het resultaat?");
                byte result = Convert.ToByte(Console.ReadLine());
                CourseRegistration newRegistration = new CourseRegistration(selectedCrs, result, selectedStud);
            }
            else if(yn.ToLower() == "nee"){
                CourseRegistration courseRegistration = new CourseRegistration(selectedCrs, null, selectedStud);
            }
        }
        public static void ShowCourseRegistrations()
        {
            foreach (var registration in CourseRegistration.AllCourseRegistrations)
            {
                Console.WriteLine($"{registration.Stud.Name} ingeschreven voor {registration.Course.Title}");
            }
        }
    }
}
