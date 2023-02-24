using M3_V1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace M3_V1.Controllers
{
    public class StadiumManagerController : Controller
    {
        private readonly M2Context db;
        private readonly IHttpContextAccessor httpContextAccessor;
        SqlConnection conn = new SqlConnection(@"Data Source=MSI;Initial Catalog=M2_;Integrated Security=True;Encrypt=False;");


        public StadiumManagerController(M2Context db,IHttpContextAccessor httpContextAccessor)
        {
            this.db= db;
            this.httpContextAccessor= httpContextAccessor;
        }





        public IActionResult LogIn()
        {

            return View();
        }





        public IActionResult Index()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(StadiumManager obj)
        {
            if (ModelState.IsValid)   //how to validate ?? it must exist in databse to be logged in
            {
                return View();
            }

            return View(obj);
        }


        //GET
        public IActionResult ViewMyStadiumInfo()
        {
            String username = (String)httpContextAccessor.HttpContext.Session.GetString("username"); //get the username from the session dictionary
            int stadiumId = (int)httpContextAccessor.HttpContext.Session.GetInt32("stadiumId"); //get the id from the session dictionary
            
            Stadium obj = db.Stadiums.SingleOrDefault(user => user.Id == stadiumId); //got the object from the databse
            
            

            ViewBag.StadiumName = obj.Name;
            ViewBag.StadiumLocation = obj.Location;
            ViewBag.stadiumCapcity = obj.Capacity;
            return View();
           
        }




        public IActionResult ViewRequestsReceived()
        {

            MyModel model;
            String username = (String)httpContextAccessor.HttpContext.Session.GetString("username"); //get the username from the session dictionary
            int stadiumId = (int)httpContextAccessor.HttpContext.Session.GetInt32("stadiumId"); //get the id from the session dictionary

            StadiumManager obj = db.StadiumManagers.SingleOrDefault(manager => manager.StadiumId == stadiumId);

            try
            {
                SqlCommand function = new SqlCommand("Select * from dbo.allRequests1(@stadium_manager_user_name)", conn);
                function.Parameters.AddWithValue("@stadium_manager_user_name", (String)obj.SuperId);
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











        public IActionResult ViewPendingRequest()
        {
            String username = (String)httpContextAccessor.HttpContext.Session.GetString("username"); //get the username from the session dictionary
            int stadiumId = (int)httpContextAccessor.HttpContext.Session.GetInt32("stadiumId"); //get the id from the session dictionary

            StadiumManager obj = db.StadiumManagers.SingleOrDefault(manager => manager.StadiumId == stadiumId);
            MyModel model;
            try
            {
                SqlCommand function = new SqlCommand("Select * from dbo.allPendingRequests1(@stadium_manager_user_name)", conn);
                function.Parameters.AddWithValue("@stadium_manager_user_name", (String)obj.SuperId);
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

        
        public IActionResult acceptRequest(string?stadiumManagerUserName , string? hostClubName , string? guestClubName , DateTime? time)
        {
            String username = (String)httpContextAccessor.HttpContext.Session.GetString("username"); //get the username from the session dictionary
            int stadiumId = (int)httpContextAccessor.HttpContext.Session.GetInt32("stadiumId"); //get the id from the session dictionary

            StadiumManager obj = db.StadiumManagers.SingleOrDefault(manager => manager.StadiumId == stadiumId);
            stadiumManagerUserName = obj.SuperId;

            try
            {
                SqlCommand function = new SqlCommand("acceptRequestKareem", conn);
                function.Parameters.AddWithValue("@username", stadiumManagerUserName);
                function.Parameters.AddWithValue("@hostname", hostClubName);
                function.Parameters.AddWithValue("@guestname", guestClubName);
                function.Parameters.AddWithValue("@start_time", time);
                function.CommandType = CommandType.StoredProcedure;


                conn.Open();
                function.ExecuteNonQuery();
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




        public IActionResult rejectRequest(string? stadiumManagerUserName, string? hostClubName, string? guestClubName, DateTime? time)
        {
            String username = (String)httpContextAccessor.HttpContext.Session.GetString("username"); //get the username from the session dictionary
            int stadiumId = (int)httpContextAccessor.HttpContext.Session.GetInt32("stadiumId"); //get the id from the session dictionary

            StadiumManager obj = db.StadiumManagers.SingleOrDefault(manager => manager.StadiumId == stadiumId);
            stadiumManagerUserName = obj.SuperId;


            try
            {
                SqlCommand function = new SqlCommand("rejectRequestKareem", conn);
                function.Parameters.AddWithValue("@username", stadiumManagerUserName);
                function.Parameters.AddWithValue("@hostname", hostClubName);
                function.Parameters.AddWithValue("@guestname", guestClubName);
                function.Parameters.AddWithValue("@start_time", time);
                function.CommandType = CommandType.StoredProcedure;


                conn.Open();
                function.ExecuteNonQuery();
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







    }
}
