using M3_V1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace M3_V1.Controllers
{
    public class SportsAssociationManagerController : Controller
    {

        private readonly M2Context db;
        private readonly IHttpContextAccessor httpContextAccessor;
        SqlConnection conn = new SqlConnection(@"Data Source=MSI;Initial Catalog=M2_;Integrated Security=True;Encrypt=False;");


        public SportsAssociationManagerController(M2Context _db, IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(SportsAssociationManager obj)
        {
            if (ModelState.IsValid)   //how to validate ?? it must exist in databse to be logged in
            {
                return RedirectToAction();
            }

            return View(obj);
        }








        public IActionResult addMatch()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addMatch(String?hostName , String?guestName , DateTime? startTime, DateTime? endTime)
        {

            //if(hostName!=null && guestName!=null && startTime != null && endTime!=null)
            //{
            //    var a =db.Clubs.SingleOrDefault(club => club.Name == hostName); 
            //    var b = db.Clubs.SingleOrDefault(club => club.Name == hostName);
            //    Match m =new Match();  //not sure of this constructor
            //    if(a!=null && b!=null && ModelState.IsValid)
            //    {
            //        m.StartTime = startTime;
            //        m.EndTime = endTime;
            //        m.HostId = a.Id;
            //        m.GuestId = b.Id;
            //        //i checked if the clubs are available in databse but didbot check if there is already a match in the same time
            //        db.Matches.Add(m);  //added to dtabase context
            //        db.SaveChanges();
            //    }
            //    else
            //    {
            //
            //    }
            //}

            try
            {
                SqlCommand proc = new SqlCommand("addNewMatch", conn);
                proc.Parameters.AddWithValue("@name_host_club", hostName);
                proc.Parameters.AddWithValue("@name_guest_club", guestName);
                proc.Parameters.AddWithValue("@start_time_match", startTime);
                proc.Parameters.AddWithValue("@end_time_match", endTime);
                proc.CommandType = CommandType.StoredProcedure;


                conn.Open();
                proc.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception e)
            {
                TempData["failed"] = "Wrong input try again";
                return View();
            }

            //no validation happens!!!!  mybe try and catch

            TempData["success"] = "process successfull";
            return RedirectToAction("LogIn", "SportsAssociationManager");


           



        }




        public IActionResult deleteMatch()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult deleteMatch(String? hostName, String? guestName, DateTime? startTime, DateTime? endTime)
        {

            try
            {
                SqlCommand proc = new SqlCommand("deleteMatch1", conn);
                proc.Parameters.AddWithValue("@name_host_club", hostName);
                proc.Parameters.AddWithValue("@name_guest_club", guestName);
                proc.Parameters.AddWithValue("@start_time_match", startTime);
                proc.Parameters.AddWithValue("@end_time_match", endTime);
                proc.CommandType = CommandType.StoredProcedure;


                conn.Open();
                proc.ExecuteNonQuery();
                conn.Close();
            }

            catch(Exception e)
            {
                TempData["failed"] = "Wrong input try again";
                return View();
            }
            //no validation happens!!!!  mybe try and catch


            TempData["success"] = "process successfull";
            return RedirectToAction("LogIn", "SportsAssociationManager");






        }













        //get
        public IActionResult ViewAllUpcomingMatches()

        {
            MyModel model;
            try
            {
                SqlCommand function = new SqlCommand("SELECT * FROM allUpcomingMatchesForSports_Association_Manager()", conn);



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




        //get
        public IActionResult ViewPlayedMatches()
        {
            MyModel model;
            try
            {
                SqlCommand function = new SqlCommand("select * from  PlayedMatchesForSports_Association_Manager()", conn);



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


        //get
        public IActionResult ViewPairsofClubsNeverMatched()
        {
            MyModel model;

            try
            {
                SqlCommand function = new SqlCommand("select * from clubsNeverMatched", conn);



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






    }
}
