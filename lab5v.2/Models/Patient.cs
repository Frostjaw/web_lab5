using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5v2.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Doctor_id { get; set; }
        public List<Doctor> temp_doctors { get; set; }
    }
}
