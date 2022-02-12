using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_SQLite.Entity
{
    class PersonalTransaction
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public double Money { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Category { get; set; }


    }
}
