using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;
using VirtualClassroom.Models.ProfessorViewModels;

namespace VirtualClassroom.Controllers
{
    public class ProfessorsController : Controller
    {
        IProfessorServices professorServices = null;
        IStudentServices studentServices = null;

        public ProfessorsController(IProfessorServices professorServices, IStudentServices studentServices)
        {
            this.professorServices = professorServices;
            this.studentServices = studentServices;
        }

        // GET: Professors
        public ActionResult Index()
        {
            return View();
        }

        // GET: Professors/Activities/5
        [Route("Professors/{professorId}/Activities")]
        public ActionResult Activities(int professorId)
        {
            Professor professor = professorServices.GetProfessor(professorId);

            return View(professor);
        }

        // GET: Professors/ActivityDetails/5
        [Route("Professors/{professorId}/Activities/{activityId}")]
        public ActionResult ActivityDetails(int professorId, int activityId)
        {
            Professor professor = professorServices.GetProfessor(professorId);
            Activity activity = professorServices.GetActivity(professorId, activityId);
            List<Student> students = studentServices.GetAllStudents().ToList();
            List<Student> activityStudents = new List<Student>(students.FindAll(student => student.Activities.ToList().Contains(activity)));

            if (!activityStudents.Any())
            {
                return View(null);
            }

            ActivityDetailsVM activityDetails = new ActivityDetailsVM
            {
                Id = activity.Id,
                Name = activity.Name,
                Students = new List<ActivityStudentVM>
                {
                    new ActivityStudentVM
                    {
                        Id = activityStudents[0].Id,
                        FirstName = activityStudents[0].FirstName,
                        LastName = activityStudents[0].LastName,
                        ActivityInfo = new ActivityInfoVM
                        {
                            Id = 1,
                            Grade = 6,
                            Presence = true
                        }
                    },
                }
            };

            return View(activityDetails);
        }

        // GET: Professors/5/Details
        [Route("Professors/{professorId}/Details")]
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