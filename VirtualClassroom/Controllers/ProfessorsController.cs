using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Controllers
{
    public class ProfessorsController : Controller
    {
        IProfessorServices professorServices = null;

        public ProfessorsController(IProfessorServices professorServices)
        {
            this.professorServices = professorServices;

            this.professorServices.AddProfessor(new Professor
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@gmail.com",
                Activities = new List<Activity>
                {
                    new Activity
                    {
                        Id = 1,
                        Name = "OOP",
                        ActivityType = new ActivityType
                        {
                            Id = 1,
                            Name = "Course"
                        },
                        Description = "Lorem ipsum dolor sit amet, duo ei volutpat voluptaria, his ne vero suscipiantur. Mel sapientem interesset complectitur in",
                        OccurenceDates = new List<ActivityOccurence>
                        {
                            new ActivityOccurence
                            {
                                Id = 1,
                                OccurenceDate = new DateTime()
                            }
                        }
                    },
                    new Activity
                    {
                        Id = 2,
                        Name = "AI",
                        ActivityType = new ActivityType
                        {
                            Id = 2,
                            Name = "Laboratory"
                        },
                        Description = "His ne vero suscipiantur. Mel sapientem interesset complectitur in",
                        OccurenceDates = new List<ActivityOccurence>
                        {
                            new ActivityOccurence
                            {
                                Id = 2,
                                OccurenceDate = new DateTime()
                            }
                        }
                    }
                }
            });
        }
        // GET: Professors
        public ActionResult Index()
        {
            return View();
        }

        // GET: Professors/Activities/5
        public ActionResult Activities(int id)
        {
            var professor = professorServices.GetProfessor(id);
            return View(professor);
        }

        // GET: Professors/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Professors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Professors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Professors/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Professors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Professors/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Professors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}