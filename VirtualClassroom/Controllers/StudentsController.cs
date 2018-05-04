using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;
using VirtualClassroom.Models.StudentViewModels;

namespace VirtualClassroom.Controllers
{
    [Authorize(Roles = "Student")]
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
            return NotFound();
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

            StudentActivitiesVM model = new StudentActivitiesVM()
            {
                Activities = studentServices.GetActivities(student.Id),
                Professors = new List<Professor>()
            };

            List<Professor> allProfessors = professorServices.GetAllProfessors().ToList();
            foreach(var activity in model.Activities)
            {
                Professor activityProfessor = allProfessors.Find(prof => prof.Activities.ToList().Exists(act => act.Id == activity.Id));
                model.Professors.Add(activityProfessor);
            }

            return View(model);
        }

        // GET: Students/5/Activities/1
        [Route("Students/{studentId}/Activities/{activityId}")]
        public ActionResult ActivityDetails(int studentId, int activityId)
        {
            Activity activity = studentServices.GetActivity(studentId, activityId);
            var activityInfos = studentServices.GetActivityInfos(studentId, activityId);

            ActivityDetailsVm activityDetailsVm = new ActivityDetailsVm()
            {
                ActivityName = activity.Name,
                ActivityInfos = activityInfos
            };

            return View(activityDetailsVm);
        }
    }
}