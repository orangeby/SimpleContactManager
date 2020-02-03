using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleContactManager.Models;

namespace SimpleContactManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly PersonDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, PersonDbContext _context)
        {
            _logger = logger;
            this._context = _context;
        }        

        public IActionResult Index()
        {
            var personsList = _context.Persons.ToList();
            return View(personsList);
        }
        
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var person = _context.Persons.Find(Id);            
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person person)
        {            
            _context.Update(person);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int Id)
        {
            var person = _context.Persons.Find(Id);
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Person person)
        {
            _context.Remove(person);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int Id)
        {
            var person = _context.Persons.Find(Id);
            return View(person);
        }












        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult InitializeData()
        {
            _context.Database.EnsureCreated();

            // Look for any students.
            if (_context.Persons.Any())
            {
                return RedirectToAction("Index");   // DB has been seeded
            }

            var persons = new Person[]
            {
            new Person{FirstName="Carson",LastName="Alexander",DateOfBirth=DateTime.Parse("2005-09-01")},
            new Person{FirstName="Meredith",LastName="Alonso",DateOfBirth=DateTime.Parse("2002-09-01")},
            new Person{FirstName="Arturo",LastName="Anand",DateOfBirth=DateTime.Parse("2003-09-01")},
            new Person{FirstName="Gytis",LastName="Barzdukas",DateOfBirth=DateTime.Parse("2002-09-01")},
            new Person{FirstName="Yan",LastName="Li",DateOfBirth=DateTime.Parse("2002-09-01")},
            new Person{FirstName="Peggy",LastName="Justice",DateOfBirth=DateTime.Parse("2001-09-01")},
            new Person{FirstName="Laura",LastName="Norman",DateOfBirth=DateTime.Parse("2003-09-01")},
            new Person{FirstName="Nino",LastName="Olivetto",DateOfBirth=DateTime.Parse("2005-09-01")}
            };
            foreach (Person person in persons)
            {
                _context.Persons.Add(person);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");   //Sample Data created.
        }
    }
}
