using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationEngine.Models.Domain
{
    public class Class
    {

        public int Id { get; set; }
        public int Name { get; set; }
        public DateTime Created { get; set; }
        public List<Student> Students { get; set; }
    }
}