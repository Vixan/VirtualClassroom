using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;
using VirtualClassroom.Models.ProfessorViewModels;

namespace VirtualClassroom.Controllers
{
    public class ProfessorsController : Controller
    {
        IProfessorServices professorServices = null;
        IStudentServices studentServices = null;
        IActivityServices activityServices = null;

        public ProfessorsController(IProfessorServices professorServices, IStudentServices studentServices,
            IActivityServices activityServices)
        {
            this.professorServices = professorServices;
            this.studentServices = studentServices;
            this.activityServices = activityServices;
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

        // GET: Professors/5/Activities/1
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

        // GET: Professors/5/Activities/1/Edit
        [HttpGet]
        [Route("Professors/{professorId}/Activities/{activityId}/Edit")]
        public ActionResult ActivityEdit(int professorId, int activityId)
        {
            Activity activity = professorServices.GetActivity(professorId, activityId);
            List<Activity> allActivities = activityServices.GetAllActivities().ToList();
            List<ActivityType> allActivityTypes = activityServices.GetAllActivityTypes().ToList();

            ActivityEditVM activityEdit = new ActivityEditVM
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                ActivityTypeId = activity.ActivityType.Id,
                OccurenceDates = activity.OccurenceDates.Select(occurenceDate => occurenceDate.OccurenceDate).ToList(),
                ActivityTypes = allActivityTypes
            };

            return View(activityEdit);
        }

        // GET: Professors/5/Activities/1/Edit
        [HttpPost]
        [Route("Professors/{professorId}/Activities/{activityId}/Edit")]
        public ActionResult ActivityEdit(int professorId, ActivityEditVM activity)
        {
            List<ActivityType> allActivityTypes = activityServices.GetAllActivityTypes().ToList();
            List<ActivityOccurence> allActivityOccurences = activityServices.GetActivityOccurences(activity.Id).ToList();

            Activity editedActivity = new Activity
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                ActivityType = allActivityTypes.Find(act => act.Id == activity.ActivityTypeId),
                OccurenceDates = new List<ActivityOccurence>()
            };

            activity.OccurenceDates.ToList().ForEach(date =>
            {
                var activityOccurence = allActivityOccurences.Find(act => act.OccurenceDate.Equals(date));
                var lastId = activityOccurence != null ? activityOccurence.Id : 0;

                editedActivity.OccurenceDates.Add(new ActivityOccurence
                {
                    Id = lastId++,
                    OccurenceDate = date
                });
            });

            activityServices.EditActivity(editedActivity);

            return Redirect($"/Professors/{professorId}/Activities");
        }

        // GET: Students/1/Details
        [Route("Students/{studentId}/Details")]
        public ActionResult StudentDetails(int studentId)
        {
            Student student = studentServices.GetStudent(studentId);

            if (student == null)
            {
                return NotFound();
            }

            StudentDetailsVM studentDetails = new StudentDetailsVM
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email
            };

            return View(studentDetails);
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