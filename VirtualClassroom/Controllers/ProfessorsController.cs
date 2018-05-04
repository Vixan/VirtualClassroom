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
            Activity currentActivity = activityServices.GetActivity(activityId);
            List<Student> activityStudents = activityServices.GetStudents(currentActivity).ToList();

            ActivityDetailsVM activityDetails = new ActivityDetailsVM
            {
                Id = currentActivity.Id,
                Name = currentActivity.Name,
                OccurenceDates = currentActivity.OccurenceDates,
                Students = new List<ActivityStudentVM>()
            };

            activityStudents.ToList().ForEach(stud =>
                activityDetails.Students.Add(new ActivityStudentVM
                {
                    Id = stud.Id,
                    FirstName = stud.FirstName,
                    LastName = stud.LastName,
                    ActivityInfos = new List<ActivityStudentInfoVM>()
                })
            );

            activityStudents.ToList().ForEach(stud =>
                stud.ActivityInfos.ToList().ForEach(act =>
                    activityDetails.Students.ToList()
                    .Find(s => s.Id == stud.Id)
                    .ActivityInfos.Add(new ActivityStudentInfoVM
                    {
                        Id = stud.Id,
                        Grade = act.Grade,
                        OccurenceDate = act.OccurenceDate.OccurenceDate,
                        Presence = act.Presence
                    })
                )
            );

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
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        [Route("Professors/{professorId}/Activities/Add")]
        public ActionResult ActivityAdd(int professorId)
        {
            return View(new ActivityAddVM {
                OccurenceDates = new List<DateTime>(),
                StudentsId = studentServices.GetAllStudents()
                                            .Select(student => student.Id)
                                            .ToList()
            });
        }

        [HttpPost]
        [Route("Professors/{professorId}/Activities/Add")]
        public ActionResult ActivityAdd(int professorId, ActivityAddVM activityModel)
        {
            Activity activity = new Activity
            {
                Name = activityModel.Name,
                Description = activityModel.Description,
                ActivityType = activityServices.GetAllActivityTypes().ToList()
                                               .Find(type => type.Name == activityModel.ActivityTypeName)
            };

            if(activityModel.StudentsId.Count != 0)
            {
                List<StudentActivity> students = new List<StudentActivity>();
                foreach(var studentId in activityModel.StudentsId)
                {
                    StudentActivity studentActivity = new StudentActivity { Activity = activity, Student = studentServices.GetStudent(studentId) };
                    students.Add(studentActivity);
                }

                activity.StudentsLink = students;
            }

            if(activityModel.OccurenceDates != null)
            {
                List<ActivityOccurence> activityOccurence = new List<ActivityOccurence>();
                foreach(var occurenceDate in activityModel.OccurenceDates)
                {
                    ActivityOccurence occurence = new ActivityOccurence { Activity = activity, OccurenceDate = occurenceDate };
                    activityOccurence.Add(occurence);

                    foreach(var studentLink in activity.StudentsLink)
                    {
                        Student student = studentLink.Student;

                        student.ActivityInfos.Add(new ActivityInfo { Activity = activity, ActivityId = activity.Id, Student = student.Id, OccurenceDate = occurence });
                    }
                }

                activity.OccurenceDates = activityOccurence;
            }

            if (!professorServices.CreateActivity(professorId, activity))
                return View(activityModel);

            return Redirect($"/Professors/{professorId}/Activities");
        }

        // GET: Professors/5/Activities/1/Students/3/Edit
        [HttpGet]
        [Route("Professors/{professorId}/Activities/{activityId}/Students/{studentId}/Edit/{activityInfoId}")]
        public ActionResult ActivityStudentInfoEdit(int professorId, int activityId, int studentId, int activityInfoId)
        {
            Activity activity = activityServices.GetActivity(activityId);
            ActivityInfo activityInfo = studentServices.GetActivityInfo(studentId, activityId);
            Student student = studentServices.GetStudent(studentId);
            Activity studentActivity = studentServices.GetActivity(studentId, activityId);

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

        // GET: Professors/5/Activities/1/Students/3/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Professors/{professorId}/Activities/{activityId}/Students/{studentId}/Edit")]
        public ActionResult ActivityStudentInfoEdit(int professorId, int activityId, int studentId, ActivityStudentInfoVM activityStudentInfo)
        {
            Student student = studentServices.GetStudent(studentId);
            ActivityInfo studentActivity = studentServices.GetActivityInfo(studentId, activityId);

            ActivityInfo activityInfo = new ActivityInfo
            {
                Id = activityStudentInfo.Id,
                ActivityId = activityId,
                Grade = activityStudentInfo.Grade,
                Presence = activityStudentInfo.Presence
            };

            studentServices.EditActivity(studentId, activityInfo);

            return RedirectToAction("ActivityDetails");
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