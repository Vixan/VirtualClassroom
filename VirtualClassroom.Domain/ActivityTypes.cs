using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VirtualClassroom.Domain
{
    class ActivityTypes
    {
        public int Identifier { get; set; }

        [Required(ErrorMessage = "Activity Type is required")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be 2 to 50 characters long")]
        public int Name { get; set; }
    }
}
