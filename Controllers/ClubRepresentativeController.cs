using M3_V1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NuGet.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace M3_V1.Controllers
{
    public class ClubRepresentativeController : Controller
    {
        private readonly M2Context db;
        private readonly IHttpContextAccessor httpContextAccessor;
        SqlConnection conn = new SqlConnection(@"Data Source=MSI;Initial Catalog=M2_;Integrated Security=True;Encrypt=False;");


        public ClubRepresentativeController(M2Context db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(String?username, String name , String clubName)
        {


            Club obj = db.Clubs.SingleOrDefault(user => user.Name == clubName);

            if (ModelState.IsValid && obj!=null)
            {
                String usernameReal = (String)httpContextAccessor.HttpContext.Session.GetString("username");
                if (username!=null && username == usernameReal)
                {
                    
                }
            }
            return RedirectToAction("Index");

        }







        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult LogIn(ClubRepresentative obj)
        //{
        //    if (ModelState.IsValid)   //how to validate ?? it must exist in databse to be logged in
        //    {
        //        return View();
        //    }
        //
        //    return View(obj);
        //}


        public IActionResult LogIn()
        {

            return View();
        }






        public IActionResult ViewMyClubInfo()
        {
            String username = (String)httpContextAccessor.HttpContext.Session.GetString("username"); //get the username from the session dictionary
            int clubId =(int)httpContextAccessor.HttpContext.Session.GetInt32("clubId"); //get the id from the session dictionary

            Club obj = db.Clubs.SingleOrDefault(user => user.Id == clubId); //got the object from the databse

            // IEnumerable < Club > z= new IEnumerable;
            //ViewBag.clear();
            ViewBag.ClubName = obj.Name;
            ViewBag.ClubLocation = obj.Location;
            return View();


        }





        public IActionResult ViewMyClubUpcomingMatches()
        {
            String username = (String) httpContextAccessor.HttpContext.Session.GetString("username"); //get the username from the session dictionary
            int clubId = (int)httpContextAccessor.HttpContext.Session.GetInt32("clubId"); //get the id from the session dictionary

            Club obj = db.Clubs.SingleOrDefault(user => user.Id == clubId); //got the object from the databse

            MyModel model;
            try
            {
                SqlCommand function = new SqlCommand("select * from upcomingMatchesOfClub(@club_name)", conn);
                function.Parameters.AddWithValue("@club_name", (String)obj.Name);
                //function.CommandType=CommandType.StoredProcedure;


                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(function);
                DataTable dt = new DataTable();
                da.Fill(dt);


                model = new MyModel();
                model.Data = dt;



                conn.Close();
            }
            catch(Exception e)
            {
                return RedirectToAction("Login");
            }

            TempData["success"] = "process successfull";
            return View(model);


        }

        public IActionResult ViewAllAvailableStadiums()
        {
            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewAllAvailableStadiums(DateTime? time)
        {
            MyModel model;
            try
            {

                SqlCommand function = new SqlCommand("select * from dbo.viewAvailableStadiumsOn(@datetime)", conn);
                function.Parameters.AddWithValue("@datetime", time);
                //function.CommandType=CommandType.StoredProcedure;




                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(function);
                DataTable dt = new DataTable();
                da.Fill(dt);


                model = new MyModel();
                model.Data = dt;



                conn.Close();
            }
            catch(Exception e)
            {
                TempData["failed"] = "Wrong input try again";
                return View();
            }

            TempData["success"] = "process successfull";
            return View("displayStadiums", model);





        }

       

        public IActionResult displayStadiums(MyModel model)
        {
            return View(model);


        }






        public IActionResult sendRequest()
        {
           


            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult sendRequest(String?stadiumNAME,DateTime?time)
        {




            String username = httpContextAccessor.HttpContext.Session.GetString("username"); //get the username from the session dictionary
            int clubId = (int)httpContextAccessor.HttpContext.Session.GetInt32("clubId"); //get the id from the session dictionary

            Club obj = db.Clubs.SingleOrDefault(user => user.Id == clubId); //got the object from the databse


            try
            {
                SqlCommand addHostRequestProc = new SqlCommand("addHostRequest", conn);

                addHostRequestProc.Parameters.AddWithValue("@club_name", (String)obj.Name);
                addHostRequestProc.Parameters.AddWithValue("@stadium_name", stadiumNAME);
                addHostRequestProc.Parameters.AddWithValue("@starting_time_match", time);

                addHostRequestProc.CommandType = CommandType.StoredProcedure;





                conn.Open();
                addHostRequestProc.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception e)
            {
                TempData["failed"] = "Wrong input try again";
                return View();
            }


            TempData["success"] = "process successfull";
            return RedirectToAction("LogIn");

           




        }

        //get
       //public IActionResult ViewAllPlayedMatches()
       //{
       //    String username = httpContextAccessor.HttpContext.Session.GetString("username"); //get the username from the session dictionary
       //    int clubId = (int)httpContextAccessor.HttpContext.Session.GetInt32("clubId"); //get the id from the session dictionary
       //
       //    Club obj = db.Clubs.SingleOrDefault(user => user.Id == clubId); //got the object from the databse
       //
       //
       //
       //}














    }
}


