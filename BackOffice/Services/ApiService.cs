using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using BackOffice.Models.Domain;
using RegistrationEngine.Models.Domain;

namespace BackOffice.Services
{
    public class ApiService
    {

        public async Task<List<Course>> GetTeacherCourses(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AuthService.BaseUrl);

                var response = await client.GetAsync("/api/teachers/"+id+"/courses");



                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<List<Course>>();
                    readTask.Wait();
                    var courses = readTask.Result;
                    return courses;
                }
               
            }

            return null;

        }

    }
}