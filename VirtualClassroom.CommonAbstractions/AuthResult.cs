using System.Collections.Generic;

namespace VirtualClassroom.CommonAbstractions
{
    public class AuthResult
    {
        public bool Succeded { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
