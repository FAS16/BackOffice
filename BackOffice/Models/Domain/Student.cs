using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models.Domain
{
    public class Student
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(200)]
        [Required]
        public string Email { get; set; }

        public string ApplicationUserId { get; set; }

        public Class Class { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<Attendance> Attendances { get; set; }


    }
}