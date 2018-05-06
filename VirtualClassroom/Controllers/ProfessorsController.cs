using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Professor")]
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
            {
                IEnumerable<ActivityInfo> studentActivityInfos = studentServices.GetActivityInfos(stud.Id, activityId);
                ICollection<ActivityStudentInfoVM> studentInfos = new List<ActivityStudentInfoVM>();

                studentActivityInfos.ToList().ForEach(actInfo =>
                {
                    studentInfos.Add(new ActivityStudentInfoVM
                    {
                        Id = actInfo.Id,
                        OccurenceDate = actInfo.OccurenceDate.OccurenceDate,
                        Grade = actInfo.Grade,
                        Presence = actInfo.Presence
                    });
                });

                activityDetails.Students.Add(new ActivityStudentVM
                {
                    Id = stud.Id,
                    FirstName = stud.FirstName,
                    LastName = stud.LastName,
                    ActivityInfos = studentInfos
                });
            });

            return View(activityDetails);
        }

        // GET: Professors/5/Activities/1/Edit
        [HttpGet]
        [Route("Professors/{professorId}/Activities/{activityId}/Edit")]
        public ActionResult ActivityEdit(int professorId, int activityId)
        {
            Activity activity = professorServices.GetActivity(professorId, activityId);
            List<ActivityType> allActivityTypes = activityServices.GetAllActivityTypes().ToList();

            List<int> selectedStudents = activity.StudentsLink.Select(studentActivity => studentActivity.Student.Id).ToList();
            List<int> otherStudents = studentServices.GetAllStudents().Select(student => student.Id).ToList();
            otherStudents.RemoveAll(student => selectedStudents.Contains(student));

            ActivityEditVM activityEdit = new ActivityEditVM
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                ActivityTypeId = activity.ActivityType.Id,
                OccurenceDates = activity.OccurenceDates.Select(occurenceDate => occurenceDate.OccurenceDate).ToList(),
                ActivityTypes = allActivityTypes,
                SelectedStudentsId = selectedStudents,
                OtherStudentsId = otherStudents
            };

            return View(activityEdit);
        }

        // POST: Professors/5/Activities/1/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Professors/{professorId}/Activities/{activityId}/Edit")]
        public ActionResult ActivityEdit(int professorId, ActivityEditVM activityModel)
        {
            List<ActivityType> allActivityTypes = activityServices.GetAllActivityTypes().ToList();
            Activity activity = activityServices.GetActivity(activityModel.Id); 

            Activity editedActivity = new Activity
            {
                Id = activityModel.Id,
                Name = activityModel.Name,
                Description = activityModel.Description,
                ActivityType = allActivityTypes.Find(actType => actType.Id == activityModel.ActivityTypeId),
                OccurenceDates = activity.OccurenceDates
            };

            // good job
            if(activityModel.OccurenceDates != null)
            {
                List<DateTime> newActivityDates = null;

                newActivityDates = activityModel.OccurenceDates.ToList();
                List<DateTime> oldDates = activity.OccurenceDates.Select(occurence => occurence.OccurenceDate).ToList();
                newActivityDates.RemoveAll(date => oldDates.Contains(date));

                foreach(var date in newActivityDates)
                {
                    ActivityOccurence newActivityOccurence = new ActivityOccurence { OccurenceDate = date, Activity = activity };
                    editedActivity.OccurenceDates.Add(newActivityOccurence);

                    foreach(var link in activity.StudentsLink)
                    {
                        link.Student.ActivityInfos.Add(new ActivityInfo
                        {
                            Student = link.Student.Id,
                            Activity = activity,
                            ActivityId = activity.Id,
                            OccurenceDate = newActivityOccurence
                        });
                    }
                }
            }

            if(activityModel.SelectedStudentsId != null)
            {
                List<int> newStudents = null;
                List<int> removedStudents = null;
                List<int> oldStudents = activity.StudentsLink.Select(link => link.Student.Id).ToList();

                newStudents = activityModel.SelectedStudentsId.ToList();
                newStudents.RemoveAll(student => oldStudents.Contains(student));

                removedStudents = oldStudents.Except(activityModel.SelectedStudentsId).ToList();
                List<StudentActivity> oldStudentActivities = activity.StudentsLink.ToList();

                List<Student> removedStudentsEntities = oldStudentActivities.Where(link => removedStudents.Contains(link.Student.Id))
                                                                            .Select(link => link.Student).ToList();
                foreach(var removedStudent in removedStudentsEntities)
                {
                    List<ActivityInfo> activityInfos = removedStudent.ActivityInfos.ToList();
                    activityInfos.RemoveAll(actInfo => actInfo.ActivityId == activity.Id);

                    removedStudent.ActivityInfos = activityInfos;
                }

                oldStudentActivities.RemoveAll(link => removedStudents.Contains(link.Student.Id));
                editedActivity.StudentsLink = oldStudentActivities;

                foreach(var newStudent in newStudents)
                {
                    Student student = studentServices.GetStudent(newStudent);
                    editedActivity.StudentsLink.Add(new StudentActivity { Student = student, Activity = activity });

                    foreach(var occurence in editedActivity.OccurenceDates)
                    {
                        student.ActivityInfos.Add(new ActivityInfo
                        {
                            Student = student.Id,
                            Activity = activity,
                            ActivityId = activity.Id,
                            OccurenceDate = occurence
                        });
                    }
                }
            }

            professorServices.EditActivity(professorId, editedActivity);

            return Redirect($"/Professors/{professorId}/Activities");
        }

        [HttpGet]
        [Route("Professors/{professorId}/Activities/Add")]
        public ActionResult ActivityAdd(int professorId)
        {
            return View(new ActivityAddVM
            {
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

            if (activityModel.StudentsId != null)
                if (activityModel.StudentsId.Count != 0)
                {
                    List<StudentActivity> students = new List<StudentActivity>();
                    foreach (var studentId in activityModel.StudentsId)
                    {
                        StudentActivity studentActivity = new StudentActivity { Activity = activity, Student = studentServices.GetStudent(studentId) };
                        students.Add(studentActivity);
                    }

                    activity.StudentsLink = students;
                }

            if (activityModel.OccurenceDates != null)
            {
                List<ActivityOccurence> activityOccurence = new List<ActivityOccurence>();
                foreach (var occurenceDate in activityModel.OccurenceDates)
                {
                    ActivityOccurence occurence = new ActivityOccurence { Activity = activity, OccurenceDate = occurenceDate };
                    activityOccurence.Add(occurence);

                    foreach (var studentLink in activity.StudentsLink)
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

        // GET: Professors/5/Activities/1/Students/3/Edit/6
        [HttpGet]
        [Route("Professors/{professorId}/Activities/{activityId}/Students/{studentId}/Edit/{activityInfoId}")]
        public ActionResult ActivityStudentInfoEdit(int professorId, int activityId, int studentId, int activityInfoId)
        {
            ActivityInfo activityInfo = studentServices.GetActivityInfo(studentId, activityInfoId);
            Student student = studentServices.GetStudent(studentId);
            Activity studentActivity = studentServices.GetActivity(studentId, activityId);

            ActivityStudentInfoVM activityDetails = new ActivityStudentInfoVM
            {
                Id = activityInfo.Id,
                Grade = activityInfo.Grade,
                OccurenceDate = activityInfo.OccurenceDate.OccurenceDate,
                Presence = activityInfo.Presence,
                ActivityName = studentActivity.Name,
                StudentName = $"{student.FirstName} {student.LastName}"
            };

            return View(activityDetails);
        }

        // GET: Professors/5/Activities/1/Students/3/Edit/6
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Professors/{professorId}/Activities/{activityId}/Students/{studentId}/Edit/{activityInfoId}")]
        public ActionResult ActivityStudentInfoEdit(int professorId, int activityId, int studentId, int activityInfoId, 
            ActivityStudentInfoVM activityStudentInfo)
        {
            Student student = studentServices.GetStudent(studentId);

            ActivityInfo activityInfo = new ActivityInfo
            {
                Id = activityStudentInfo.Id,
                ActivityId = activityId,
                Grade = activityStudentInfo.Grade,
                Presence = activityStudentInfo.Presence
            };

            studentServices.EditActivityInfo(studentId, activityInfoId, activityInfo);

            return RedirectToAction("ActivityDetails");
        }

        // Post: Professors/5/Activities/1/Delete
        [HttpDelete]
        [Route("Professors/{professorId}/Activities/{activityId}/Delete")]
        public ActionResult ActivityDelete(int professorId, int activityId)
        {
            Activity activityToDelete = activityServices.GetActivity(activityId); 
            activityServices.DeleteActivity(activityToDelete);

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