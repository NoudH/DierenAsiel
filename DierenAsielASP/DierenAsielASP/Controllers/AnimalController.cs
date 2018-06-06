using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DierenAsielASP.Models;
using DierenAsielASP.Database;
using DierenAsiel.Logic;
using DierenAsiel;

namespace DierenAsielASP.Controllers
{
    public class AnimalController : Controller
    {
        IDatabase database = new DatabaseManager();

        public IActionResult Index()
        {
            return View(database.GetAllAnimalsNotReserved());
        }

        [HttpPost]
        public IActionResult Animal(int index)
        {            
            AnimalModel model = database.GetAllAnimalsNotReserved()[index];
            return View(model);
        }
    }
}