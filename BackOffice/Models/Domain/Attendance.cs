using System;
using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models.Domain
{
    public class Attendance
    {
        public int Id { get; set; }
        public Lesson Lesson { get; set; }
        public int LessonId { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public DateTime Timestamp { get; set; }

        public CheckTypes CheckType { get; set; }

        [Required]
        public string TagId { get; set; }

        [Required]
        public string DeviceId { get; set; }

        public bool PossibleFraud { get; set; }
    }
}