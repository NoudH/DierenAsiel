using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel
{
    public class Employee
    {
        public enum Gender
        {
            Male = 0,
            Female = 1
        }

        public string name;
        public int age;
        public string address;
        public string phoneNumber;
        public Gender gender;

        public override string ToString()
        {
            return name;
        }
    }
}
