using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Logic
{
    public interface ICaretakingLogic
    {
        DateTime GetUitlaatDate(Animal a);
        void SetUitlaatDate(Animal animal, Employee employee, DateTime date);
        List<Cage> GetAllCages();
        Cage GetCage(int cageNumber);
        void SetCleanDate(int cageNumber, DateTime value, string employee);
        DateTime GetFeedingDate(Animal animal);
        void SetFeedingDate(Animal animal, DateTime value, Employee employee);
    }
}
