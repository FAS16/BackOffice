using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationEngine.Models.Domain
{
    public class Lesson
    {

        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateTime CheckedIn { get; set; }
        public DateTime CheckedOut { get; set; }

    }
}