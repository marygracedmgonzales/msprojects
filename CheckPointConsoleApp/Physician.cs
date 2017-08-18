using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckPointConsoleApp
{
    public class Physician
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public Decimal Weight { get; set; }
        public Decimal Height { get; set; }
        public ContactInfo PhysicianContactInfo { get; set; }
        public List<Specialization> Specialization { get; set; }
    }
}
