using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ProfessorsController(IProfessorServices professorServices, IStudentServices studentServices, IActivityServices activityServices)
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

        // GET: Professors/5/Activities
        [Route("Professors/{professorId}/Activities")]
        public ActionResult Activities(int professorId)
        {
            Professor professor = professorServices.GetProfessor(professorId);

            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professors/5/Activities/1
        [Route("Professors/{professorId}/Activities/{activityId}")]
        public ActionResult ActivityDetails(int professorId, int activityId)
        {
            Professor professor = professorServices.GetProfessor(professorId);
            Activity activity = professorServices.GetActivity(professorId, activityId);
            IEnumerable<Student> students = studentServices.GetAllStudents().ToList();
            IEnumerable<Student> activityStudents = activityServices.GetStudents(activity);
            ICollection<ActivityOccurence> activityOccurences = activity.OccurenceDates.ToList();

            if (!activityStudents.Any())
            {
                return View(null);
            }

            ActivityDetailsVM activityDetails = new ActivityDetailsVM
            {
                Id = activity.Id,
                Name = activity.Name,
                Students = new List<ActivityStudentVM>(),
                OccurenceDates = activityOccurences
            };

            foreach (var stud in activityStudents)
            {
                var activityInfo = studentServices.GetActivityInfo(stud.Id, activityId);

                activityDetails.Students.Add(new ActivityStudentVM
                {
                    Id = stud.Id,
                    FirstName = stud.FirstName,
                    LastName = stud.LastName,
                    ActivityInfo = new ActivityStudentInfoVM
                    {
                        Id = activityInfo.Id,
                        OccurenceDate = activityInfo.OccurenceDate.OccurenceDate,
                        Grade = activityInfo.Grade,
                        Presence = activityInfo.Presence
                    }
                });
            }


            return View(activityDetails);
        }

        // GET: Professors/5/Activities/1/Edit
        [HttpGet]
        [Route("Professors/{professorId}/Activities/{activityId}/Edit")]
        public ActionResult ActivityEdit(int professorId, int activityId)
        {
            Activity activity = professorServices.GetActivity(professorId, activityId);
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

        // POST: Professors/5/Activities/1/Edit
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

            professorServices.EditActivity(professorId, editedActivity);

            return Redirect($"/Professors/{professorId}/Activities");
        }

        // GET: Professors/5/Activities/1/Students/3/Edit
        [HttpGet]
        [Route("Professors/{professorId}/Activities/{activityId}/Students/{studentId}/Edit")]
        public ActionResult ActivityStudentInfoEdit(int professorId, int activityId, int studentId)
        {
            Activity activity = activityServices.GetActivity(activityId);
            Student student = studentServices.GetStudent(studentId);
            Activity studentActivity = studentServices.GetActivity(studentId, activityId);

            var activityInfo = studentServices.GetActivityInfo(studentId, activityId);

            ActivityStudentInfoVM activityDetails = new ActivityStudentInfoVM
            {
                Id = studentActivity.Id,
                Grade = activityInfo.Grade,
                OccurenceDate = activityInfo.OccurenceDate.OccurenceDate,
                Presence = activityInfo.Presence,
                ActivityName = studentActivity.Name,
                StudentName = $"{student.FirstName} {student.LastName}"
            };

            return View(activityDetails);
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
    }
}