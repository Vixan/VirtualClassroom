using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Models.StudentViewModels
{
    public class ActivityDetailsVm
    {
        public string ActivityName { get; set; }

        public IEnumerable<ActivityInfo> ActivityInfos { get; set; }
    }
}
