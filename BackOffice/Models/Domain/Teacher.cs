using System;
using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models.Domain
{
    public class Teacher
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ApplicationUserId { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}