using System;

namespace BackOffice.Models.Domain
{
    public class Lesson
    {
        public int Id { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public Weekday Weekday { get; set; }

        public Room ClassRoom { get; set; }
    }
}