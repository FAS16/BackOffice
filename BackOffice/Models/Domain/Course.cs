using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices;

namespace BackOffice.Models.Domain
{
    public class Course
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public List<Lesson> Lessons { get; set; }

        public List<Class> Classes { get; set; }

        [Required]
        public Teacher Teacher { get; set; }
    }
}