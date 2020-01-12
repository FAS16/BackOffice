using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BackOffice.Models.Domain;
using BackOffice.Models.ViewModels;
using BackOffice.Services;

namespace BackOffice.Controllers
{
    public class ClassesController : Controller
    {

        public async Task<ActionResult> Index()
        {

            if (!AuthService.IsAuthenticated())
            {
                RedirectToAction("Login", "Account");
            }

            List<Class> classes = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", AuthService.GetBearerToken());


                client.BaseAddress = new Uri(AuthService.BaseUrl);

                var responseTask = await client.GetAsync("api/classes");

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<List<Class>>();
                    readTask.Wait();

                    classes = readTask.Result;
                }

                //                if (responseTask.StatusCode == HttpStatusCode.Unauthorized)
                //                {
                //                    return RedirectToAction("Login", "Account");
                //                }

                if (!responseTask.IsSuccessStatusCode)
                {

                    return null;
                }

            }

            return View(classes);
        }


        public async Task<ActionResult> GetStudents(int id, string title)
        {
            if (!AuthService.IsAuthenticated())
            {
                RedirectToAction("Login", "Account");
            }

            List<Student> students = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", AuthService.GetBearerToken());


                client.BaseAddress = new Uri(AuthService.BaseUrl);

                var responseTask = await client.GetAsync("api/classes/"+id+"/students");

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<List<Student>>();
                    readTask.Wait();

                    students = readTask.Result;
                }


                if (!responseTask.IsSuccessStatusCode)
                {

                    return null;
                }

            }

            var viewModel = new StudentsViewModel
            {

                Students = students,
                ClassTitle = title

            };
            

            return View(viewModel);

        }

        public async Task<ActionResult> GetCourses(int id, string title)
        {
            if (!AuthService.IsAuthenticated())
            {
                RedirectToAction("Login", "Account");
            }

            if (!AuthService.IsAuthenticated())
            {
                RedirectToAction("Login", "Account");
            }

            List<Course> courses = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", AuthService.GetBearerToken());


                client.BaseAddress = new Uri(AuthService.BaseUrl);

                var responseTask = await client.GetAsync("api/classes/" + id + "/courses");

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<List<Course>>();
                    readTask.Wait();

                    courses = readTask.Result;
                }


                if (!responseTask.IsSuccessStatusCode)
                {

                    return null;
                }

            }

            var viewModel = new CoursesViewModel
            {

                Courses = courses,
                ClassTitle = title,
                ClassId = id

            };


            return View(viewModel);

        }

//        public ActionResult GetRegistrations(object id, object title)
//        {
//             var 
//        }
        public async Task<ActionResult> GetRegistrations(int courseId, int classId, string courseTitle, string classTitle)
        {

            if (!AuthService.IsAuthenticated())
            {
                RedirectToAction("Login", "Account");
            }

            List<Attendance> attendances = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", AuthService.GetBearerToken());

                client.BaseAddress = new Uri(AuthService.BaseUrl);

                var responseTask = await client.GetAsync("api/classes/" + classId + "/courses/"+courseId+"/attendances");

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<List<Attendance>>();
                    readTask.Wait();

                    attendances = readTask.Result;
                }


                if (!responseTask.IsSuccessStatusCode)
                {

                    return null;
                }

            }

            var viewModel = new AttendancesViewModel
            {
                Attendances = attendances,
                ClassTitle = classTitle,
                CourseTitle = courseTitle
            };


            return View(viewModel);
        }
    }
}
