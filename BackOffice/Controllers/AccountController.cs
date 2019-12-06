using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BackOffice.Models;
using BackOffice.Models.Auth;
using BackOffice.Models.Domain;
using BackOffice.Services;

namespace BackOffice.Controllers
{
    public class AccountController : Controller
    {
        private string TokenString;
        private string BaseUrl;
        public AccountController()
        {
            TokenString = AuthService.TokenString;
            BaseUrl = AuthService.BaseUrl;

        }
        // GET: Account
        
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AccountViewModels.LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            BearerToken token;

            using (var client = new HttpClient())
            {
                var tokenRequest =
                    new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", model.Email),
                        new KeyValuePair<string, string>("password", model.Password)
                    };

                HttpContent encodedRequest = new FormUrlEncodedContent(tokenRequest);

                var response = await client.PostAsync(BaseUrl + "/Token", encodedRequest);
                

                if (response.IsSuccessStatusCode)
                {

                    token = response.Content.ReadAsAsync<BearerToken>().Result;

                    // Store token in ASP.NET Session State
                    Session[TokenString] = token.AccessToken;

                    //Initialize user (singleton)
                    
                    Models.Domain.User.Instance.Token = token;
                    Models.Domain.User.Instance.Email = token.UserName;

                    User user = await GetTeacherInfo(token.UserName);
                    Models.Domain.User.Instance.Id = user.Id;

                    return RedirectToLocal(returnUrl);


                }

                ModelState.AddModelError("", "Error while logging user in = " + response.StatusCode);
                return View(model);

            }
        }


        public async Task<User> GetTeacherInfo(string username)
        {
            User user;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var response = await client.GetAsync("/api/teachers/1");
                

               
                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<User>();
                    readTask.Wait();
                    user = readTask.Result;
                    Models.Domain.User.Instance.Id = user.Id;
                }
                else
                {
                    user = null;
                    ModelState.AddModelError("", "Error while logging user in = " + response.StatusCode);

                }
            }

            return user;

        }



        public ActionResult RegisterTeacher()
        {

            return View();
        }

        public ActionResult RegisterStudent()
        {

            return View();
        }

        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<ActionResult> Register(AccountViewModels.RegisterViewModel model)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                using (var client = new HttpClient())
        //                {
        //
        //
        //                    var response = await client.PostAsJsonAsync(BaseUrl + "/Api/Account/Register/Teacher", model);
        //
        //
        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        return RedirectToAction("Login");
        //                    }
        //
        //                    ModelState.AddModelError("", "Error while registering user = " + response.StatusCode);
        //                    model.Password = null;
        //                    model.ConfirmPassword = null;
        //                    return View(model);
        //
        //                }
        //            }
        //
        //            // If we got this far, something failed, redisplay form
        //            return View(model);
        //        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }



        public ActionResult LogOff()
        {

            AuthService.SignOut();

            return RedirectToAction("Login");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterTeacher(AccountViewModels.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {


                    var response = await client.PostAsJsonAsync(BaseUrl + "/Api/Account/Register/Teacher", model);


                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }

                    ModelState.AddModelError("", "Error while registering user = " + response.StatusCode);
                    model.Password = null;
                    model.ConfirmPassword = null;
                    return RedirectToAction("RegisterTeacher",model);

                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("RegisterTeacher", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterStudent(AccountViewModels.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {


                    var response = await client.PostAsJsonAsync(BaseUrl + "/Api/Account/Register/Student", model);


                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }

                    ModelState.AddModelError("", "Error while registering user = " + response.StatusCode);
                    model.Password = null;
                    model.ConfirmPassword = null;
                    return RedirectToAction("RegisterStudent", model);

                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("RegisterStudent", model);
        }
    }
}