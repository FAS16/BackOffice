using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BackOffice.Models.Domain;
using BackOffice.Models.ViewModels;
using BackOffice.Services;

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

            if (!AuthService.IsAuthenticated())
            {
                RedirectToAction("Login", "Account");
            }

            List<Course> courses = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", AuthService.GetBearerToken());


                client.BaseAddress = new Uri(AuthService.BaseUrl);

                var responseTask = await client.GetAsync("api/courses");

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<List<Course>>();
                    readTask.Wait();

                    courses = readTask.Result;
                }

//                if (responseTask.StatusCode == HttpStatusCode.Unauthorized)
//                {
//                    return RedirectToAction("Login", "Account");
//                }

                if (!responseTask.IsSuccessStatusCode)
                {
                    return View("ErrorPage", responseTask.StatusCode.ToString());
                }

            }

            return View(courses);
        }

        public async Task<ActionResult> DeleteCourse(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", AuthService.GetBearerToken());

                client.BaseAddress = new Uri(AuthService.BaseUrl);

                var responseTask = await client.DeleteAsync("api/courses/" + id);


                if (responseTask.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (!responseTask.IsSuccessStatusCode)
                {

                    return View("ErrorPage", responseTask.StatusCode.ToString());
                }

            }

            return RedirectToAction("Index", "Courses");

        }

        public async Task<ActionResult> EditCourse(int id)
        {
            Course course = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", AuthService.GetBearerToken());


                client.BaseAddress = new Uri(AuthService.BaseUrl);

                var responseTask = await client.GetAsync("api/courses/" + id);

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<Course>();
                    readTask.Wait();

                    course = readTask.Result;
                }

                if (responseTask.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (!responseTask.IsSuccessStatusCode)
                {
                    return View("ErrorPage", responseTask.StatusCode.ToString());
                }

            }

            var viewModel = new CourseFormViewModel(course);
            var teachers = this.RetrieveTeachers();
            viewModel.Teachers = teachers;

            return View("CourseForm", viewModel);

        }

        public List<Teacher> RetrieveTeachers()
        {
            List<Teacher> teachers = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", AuthService.GetBearerToken());


                client.BaseAddress = new Uri(AuthService.BaseUrl);

                var responseTask = client.GetAsync("api/teachers");

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Teacher>>();
                    readTask.Wait();

                    teachers = readTask.Result;

                    return teachers;
                }

            }

            return null;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(Course course)
        {

            if (!ModelState.IsValid)
            { 
                var viewModel = new CourseFormViewModel(course);
                var teachers = this.RetrieveTeachers();
                viewModel.Teachers = teachers;

                return View("CourseForm", viewModel);

            }


            if (course.Id == 0)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", AuthService.GetBearerToken());

                    client.BaseAddress = new Uri(AuthService.BaseUrl);


                    var responseTask = await client.PostAsJsonAsync("api/courses/", course);


                    if (responseTask.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }

                    if (!responseTask.IsSuccessStatusCode)
                    {

                        return View("ErrorPage", responseTask.StatusCode.ToString());
                    }

                }
            }
            else
            {

                // Put
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", AuthService.GetBearerToken());

                    client.BaseAddress = new Uri(AuthService.BaseUrl);


                    var responseTask = await client.PutAsJsonAsync("api/courses/" + course.Id, course);


                    if (responseTask.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }

                    if (!responseTask.IsSuccessStatusCode)
                    {

                        return View("ErrorPage", responseTask.StatusCode.ToString());
                    }

                }

            }

            return RedirectToAction("Index", "Courses");

        }

    }
}