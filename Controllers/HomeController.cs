using FC_CRUD.DB_Context;
using FC_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace FC_CRUD.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext adb)
        {
            _db = adb;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Developers mdlobj)
        {
            if(mdlobj.ID == 0)
            {
                _db.developer.Add(mdlobj);
                _db.SaveChanges();
            }
            else
            {
                _db.developer.Update(mdlobj);
                _db.SaveChanges();
            }
            return RedirectToAction("Read");
        }

        [HttpGet]
        public IActionResult Read()
        {
            var Data = _db.developer.ToList();
            return View(Data);
        }

        [HttpGet]
        public IActionResult Update(int ID)
        {
            var UpdateData = _db.developer.Where(a => a.ID == ID).First();

            TempData["btn"] = "Button";
            
            return View("Create",UpdateData);
        }

        [HttpGet]
        public IActionResult Delete(int ID)
        {
            var DeleteData = _db.developer.Where(a => a.ID == ID).First();
            
            _db.developer.Remove(DeleteData);
            _db.SaveChanges();

            return RedirectToAction("Read");
        }
       
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }
       
        [HttpPost]
        [AllowAnonymous]
        public IActionResult LogIn(Userinfo mdlobj)
        {
            var Data = _db.Userinfos.Where(a => a.Email == mdlobj.Email).FirstOrDefault();

            if (Data == null)
            {
                TempData["Email"] = "Email Not Found";
                return View();
            }
            else
            {
                if (Data.Email == mdlobj.Email && Data.Password == mdlobj.Password)
                {
                    var Claims = new[] { new Claim(ClaimTypes.Name, Data.Name), 
                        new Claim(ClaimTypes.Email, Data.Email) };

                    var Identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var AuthProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Identity), AuthProperties);

                    HttpContext.Session.SetString("Email", Data.Email);

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Pass"] = "Password Not Match";
                    return View();
                }
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("LogIn");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
