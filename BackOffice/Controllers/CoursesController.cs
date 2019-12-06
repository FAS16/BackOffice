using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BackOffice.Services;
using RegistrationEngine.Models.Domain;

namespace BackOffice.Controllers
{
    public class CoursesController : Controller
    {
        private ApiService apiService;
        public CoursesController()
        {

            this.apiService = new ApiService();
            
        }
        // GET: Courses
        public async Task<ActionResult> Index()
        {
            List<Course> courses = await apiService.GetTeacherCourses(Models.Domain.User.Instance.Id);

            return View(courses);
        }
    }
}