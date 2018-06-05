using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Database
{
    public interface ICaretakingDatabase
    {
        List<Cage> GetAllCages();
        DateTime GetCleaningdate(Cage cage);
        void SetCleanDate(int cageNumber, DateTime value, string employee);
        List<DateTime> GetFeedingDates(Animal animal);
        void SetFeedingDate(Animal animal, DateTime value, Employee employee);
        DateTime GetUitlaatDate(Animal animal);
        void SetUitlaatDate(Animal animal, Employee employee, DateTime date);
        int GetFood(Enums.Foodtype type);
        void AddFood(Enums.Foodtype type, int amount);
    }
}
