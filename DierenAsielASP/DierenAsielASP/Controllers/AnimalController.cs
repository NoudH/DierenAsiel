using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DierenAsielASP.Models;

namespace DierenAsielASP.Controllers
{
    public class AnimalController : Controller
    {
        public IActionResult Index()
        {
            List<AnimalModel> AllAnimals = Database.DatabaseManager.GetAllAnimalsNotReserved();

            return View(AllAnimals);
        }

        [HttpPost]
        public IActionResult Animal(int index)
        {
            AnimalModel model = Database.DatabaseManager.GetAllAnimalsNotReserved()[index];
            return View(model);
        }
    }
}