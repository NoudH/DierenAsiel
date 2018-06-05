using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DierenAsielASP.Models;
using DierenAsielASP.Database;

namespace DierenAsielASP.Controllers
{
    public class AnimalController : Controller
    {
        IDatabase database = new DatabaseManager();

        public IActionResult Index()
        {
            List<AnimalModel> AllAnimals = database.GetAllAnimalsNotReserved();

            return View(AllAnimals);
        }

        [HttpPost]
        public IActionResult Animal(int index)
        {
            AnimalModel model = database.GetAllAnimalsNotReserved()[index];
            return View(model);
        }
    }
}