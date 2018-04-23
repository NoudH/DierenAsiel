using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel
{
    public class Cage
    {
        public int cageNumber;
        public DateTime lastCleaningdate;
        public List<Animal> animals = new List<Animal>();

        public override string ToString()
        {
            return cageNumber.ToString();
        }
    }
}
