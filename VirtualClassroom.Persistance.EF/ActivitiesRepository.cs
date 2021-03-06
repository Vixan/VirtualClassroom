﻿using System.Linq;
using System.Collections.Generic;
using VirtualClassroom.Domain;
using Microsoft.EntityFrameworkCore;

namespace VirtualClassroom.Persistence.EF
{
    class ActivitiesRepository : Repository<Activity>, IActivitiesRepository
    {
        public ActivitiesRepository(DummyDbContext context)
        {
            dataContext = context;
        }

        public override Activity GetById(int identifier)
        {
            return dataContext.Activities
                .Where(activity => activity.Id == identifier)
                    .Include(activity => activity.ActivityType)
                    .Include(activity => activity.OccurenceDates)

                    .Include(activity => activity.StudentsLink)
                        .ThenInclude(studentsLink => studentsLink.Student)
                            .ThenInclude(student => student.ActivityInfos)
                                .ThenInclude(activityInfo => activityInfo.OccurenceDate)

                     .Include(activity => activity.StudentsLink)
                        .ThenInclude(studentsLink => studentsLink.Student)
                            .ThenInclude(student => student.ActivityInfos)
                                .ThenInclude(activityInfo => activityInfo.Activity)

                    .Include(activity => activity.StudentsLink)
                        .ThenInclude(studentsLink => studentsLink.Activity)
                .FirstOrDefault();
        }

        public Activity GetByName(string name)
        {
            return dataContext.Activities
                .Where(activity => activity.Name == name)
                    .Include(activity => activity.OccurenceDates)
                .FirstOrDefault();
        }

        public override IEnumerable<Activity> GetAll()
        {
            return dataContext.Activities
                .Include(activity => activity.OccurenceDates)
                .Include(activity => activity.ActivityType)
                .Include(activity => activity.StudentsLink)
                    .ThenInclude(studentsLink => studentsLink.Student)
                        .ThenInclude(student => student.ActivityInfos);
        }

        public List<Activity> GetByType(ActivityType type)
        {
            List<Activity> activities = (List<Activity>)dataContext.Activities
                .Where(activity => activity.ActivityType.Id == type.Id)
                    .Include(activity => activity.OccurenceDates);

            return activities;
        }

        public ActivityType GetActivityType(string activityTypeName)
        {
            return dataContext.ActitivityTypes
                .Where(activityType => activityType.Name == activityTypeName)
                .FirstOrDefault();
        }

        public List<ActivityType> GetActivityTypes()
        {
            List<ActivityType> activityTypes = dataContext.ActitivityTypes.ToList();

            return activityTypes;
        }

        public void RemoveActivityInfo(ActivityInfo activityInfo)
        {
            dataContext.ActivityInfos.Remove(dataContext.ActivityInfos.Find(activityInfo.Id));
        }

        public void RemoveStudentActivity(StudentActivity studentActivity)
        {
            dataContext.StudentActivities.Remove(dataContext.StudentActivities.Find(studentActivity.Id));
        }
    }
}
