using M3_V1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace M3_V1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly M2Context db; //this is the database context which i think includes all tables
        private readonly IHttpContextAccessor contextAccessor;



        public HomeController(M2Context _db, IHttpContextAccessor contextAccessor)
        {
            db = _db;
            this.contextAccessor = contextAccessor;
        }

        //   public HomeController(ILogger<HomeController> logger)
        //   {
        //       _logger = logger;
        //       
        //   }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult create()
        {
            return View();
        }


      // post
       [HttpPost]
       [ValidateAntiForgeryToken]
       public IActionResult LogIn(SystemUser obj)
       {

           

            var username = obj.Username;
            var check=db.SystemUsers.Find(username);


            if (check!=null && check.Password==obj.Password)
            {
                if (username != null)
                {
                    var admin = db.SystemAdmins.SingleOrDefault(user => user.SuperId == username);            //we must check oj\n the passsword also!!!!!
                    var sportsManager = db.SportsAssociationManagers.SingleOrDefault(user => user.SuperId == username);
                    var clubRepresentative = db.ClubRepresentatives.SingleOrDefault(user => user.SuperId == username);
                    var stadiumManager = db.StadiumManagers.SingleOrDefault(user => user.SuperId == username);
                    var fan = db.Fans.SingleOrDefault(user => user.SuperId == obj.Username);



                    if (admin != null)
                    {

                        if (ModelState.IsValid)
                        {
                            contextAccessor.HttpContext.Session.SetString("username", obj.Username);
                            return RedirectToAction("LogIn", "SystemAdmin");
                        }
                    }
                    else if (sportsManager != null)
                    {
                        if (ModelState.IsValid)
                        {
                            contextAccessor.HttpContext.Session.SetString("username", obj.Username);
                            return RedirectToAction("LogIn", "SportsAssociationManager");
                        }
                    }
                    else if (clubRepresentative != null)
                    {
                        if (ModelState.IsValid)
                        {
                            int clubId = (int)clubRepresentative.ClubId;
                            contextAccessor.HttpContext.Session.SetString("username", obj.Username);
                            contextAccessor.HttpContext.Session.SetInt32("clubId", clubId);
                            return RedirectToAction("LogIn", "ClubRepresentative");
                        }
                    }
                    else if (stadiumManager != null)
                    {
                        if (ModelState.IsValid)
                        {
                            int stadiumId = (int)stadiumManager.StadiumId;
                            contextAccessor.HttpContext.Session.SetString("username", obj.Username);
                            contextAccessor.HttpContext.Session.SetInt32("stadiumId", stadiumId);
                            return RedirectToAction("LogIn", "StadiumManager");
                        }
                    }

                    else if (fan != null)
                    {
                        if (ModelState.IsValid)
                        {
                            String NationalId = fan.NationalId;
                            contextAccessor.HttpContext.Session.SetString("username", obj.Username);
                            contextAccessor.HttpContext.Session.SetString("NationalId", NationalId);
                            return RedirectToAction("LogIn", "Fan");
                        }
                    }
                    else
                    {
                        //maybe display error message
                        return RedirectToAction("Index");  //means the input is not in the database
                    }



                }
            }

                //maybe display error message
                return RedirectToAction("Index");  //means the input is not in the database
            


           

        }








        public IActionResult SignIn()
        {
            return View();
        }









    }
}