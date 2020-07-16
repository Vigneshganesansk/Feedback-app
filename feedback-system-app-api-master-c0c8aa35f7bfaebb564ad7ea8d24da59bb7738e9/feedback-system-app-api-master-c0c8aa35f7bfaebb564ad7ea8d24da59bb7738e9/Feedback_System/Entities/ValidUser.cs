using System;
using System.Collections.Generic;

namespace Feedback_System.Entities
{
    public partial class ValidUser
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsValid { get; set; }
    }
}
