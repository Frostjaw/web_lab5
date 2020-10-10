using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5v2.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
        public int Hospital_id { get; set; }
        public List<Hospital> temp_hospitals { get; set; }
    }
}
