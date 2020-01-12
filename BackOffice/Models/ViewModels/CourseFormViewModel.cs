using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using BackOffice.Models.Domain;

namespace BackOffice.Models.ViewModels
{
    public class CourseFormViewModel
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public List<Lesson> Lessons { get; set; }

        public List<Class> Classes { get; set; }

        [Required]
        public Teacher Teacher { get; set; }

        public string PageTitle => Id != 0 ? "Edit table" : "Add table";
        public List<Teacher> Teachers { get; set; }


        public CourseFormViewModel()
        {
            Id = 0;
        }



        public CourseFormViewModel(Course course)
        {
            Id = course.Id;
            Title = course.Title;
            Lessons = course.Lessons;
            Classes = course.Classes;
            Teacher = course.Teacher;
        }
    }
}