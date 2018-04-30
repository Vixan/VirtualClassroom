using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;
using VirtualClassroom.Models.StudentViewModels;
using System.Collections.Generic;
using System.Linq;

namespace VirtualClassroom.Controllers
{
    public class StudentsController : Controller
    {
        IProfessorServices professorServices = null;
        IStudentServices studentServices = null;
        IActivityServices activityServices = null;

        public StudentsController(IProfessorServices professorServices, IStudentServices studentServices, IActivityServices activityServices)
        {
            this.professorServices = professorServices;
            this.studentServices = studentServices;
            this.activityServices = activityServices;
        }

        // GET: Students
        public ActionResult Index()
        {
            return View();
        }

        // GET: Students/5/Activities
        [Route("Students/{studentId}/Activities")]
        public ActionResult Activities(int studentId)
        {
            Student student = studentServices.GetStudent(studentId);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/5/Activities/1
        [Route("Students/{studentId}/Activities/{activityId}")]
        public ActionResult ActivityInfo(int studentId, int activityId)
        {
            Student student = studentServices.GetStudent(studentId);
            Activity activity = studentServices.GetActivity(studentId, activityId);

            var activityInfo = student.ActivityInfos.ToList().Find(actInfo => actInfo.ActivityId == activityId);

            ActivityInfoVM activityDetails = new ActivityInfoVM
            {
                Id = activity.Id,
                Name = activity.Name,
                OccurenceDate = activityInfo.OccurenceDate.OccurenceDate,
                Presence = activityInfo.Presence,
                Grade = activityInfo.Grade
            };

            return View(activityDetails);
        }

        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Students/Edit/5
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
    }
}