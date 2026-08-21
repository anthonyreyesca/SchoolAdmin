using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal class AdministrativePersonnel : Employee
    {
        public static IImmutableList<AdministrativePersonnel> AllAdministrativePersonnel
        {
            get 
            {
                List<AdministrativePersonnel> administrativePersonnel = new List<AdministrativePersonnel>();
                foreach (Person p in Person.AllPersons)
                {
                    if (p is AdministrativePersonnel adp)
                    {
                        administrativePersonnel.Add(adp);
                    }
                }
                return administrativePersonnel.ToImmutableList<AdministrativePersonnel>();
            }
        }

        public AdministrativePersonnel(string name, DateTime birthDate, Dictionary<string, byte> tasks) : base(name, birthDate, tasks)
        {
        }

        public override double DetermineWorkload()
        {
            double total = 0;
            foreach (var task in Tasks)
            {
                total += task.Value;
            }
            return total;
        }

        public override string GenerateNameCard()
        {
            return $"{this.Name} (ADMINISTRATIE)";
        }

        public override uint CalculateSalary()
        {
            int extraSalary = (this.Seniority / 3) * 75;

            double baseSalary = 2000;       // basis voor fulltime 40 uur
            double hoursWorked = DetermineWorkload();
            double hourlyRate = (baseSalary + extraSalary) / 40;  // 50 per uur

            double totalSalary = (hourlyRate * hoursWorked);
            return (uint)Math.Round(totalSalary);
        }
    }
}
