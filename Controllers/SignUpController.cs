using M3_V1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace M3_V1.Controllers
{

    //i want to login the specific person and route him to the specific page

    public class SignUpController : Controller
    {

        private readonly M2Context db;
        private readonly IHttpContextAccessor httpContextAccessor;
        SqlConnection conn = new SqlConnection(@"Data Source=MSI;Initial Catalog=M2_;Integrated Security=True;Encrypt=False;");

        public SignUpController(M2Context db,IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor; 
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Pick()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SystemUser obj)
        {
            var username = obj.Username;
            var check = db.SystemUsers.Find(username);

            if(check==null)  //valid not found in database
            {
                if(ModelState.IsValid)
                {
                    httpContextAccessor.HttpContext.Session.SetString("username", obj.Username);
                    db.SystemUsers.Add(obj);
                    db.SaveChanges();
                    return RedirectToAction("Pick");
                }
            }
            //maybe error message here
            return RedirectToAction("Index");




        }


        public IActionResult SystemAdmin()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SystemAdmin(String?username , String?password)
        {
            //var check = db.SystemUsers.Find(username);
            //
            //
            //if(check==null && username!=null)
            //{
            //    httpContextAccessor.HttpContext.Session.SetString("username", username);
            //
            //
            //
            //    SqlCommand addHostRequestProc = new SqlCommand("addHostRequest", conn);
            //
            //    addHostRequestProc.Parameters.AddWithValue("@club_name", (String)obj.Name);
            //    addHostRequestProc.Parameters.AddWithValue("@stadium_name", stadiumNAME);
            //    addHostRequestProc.Parameters.AddWithValue("@starting_time_match", time);
            //
            //    addHostRequestProc.CommandType = CommandType.StoredProcedure;
            //
            //
            //
            //
            //
            //    conn.Open();
            //    addHostRequestProc.ExecuteNonQuery();
            //    conn.Close();
            //
            //
            //
            //    return RedirectToAction("LogIn","SystemAdmin");
            //}
            //


            return View();



        }





        public IActionResult SportsAssociationManager()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SportsAssociationManager(String? username, String? password, String? name)
        {
            var check = db.SystemUsers.Find(username);


            if (check == null && username != null)
            {
                httpContextAccessor.HttpContext.Session.SetString("username", username);


                try
                {
                    SqlCommand addHostRequestProc = new SqlCommand("addAssociationManager", conn);

                    addHostRequestProc.Parameters.AddWithValue("@name", name);
                    addHostRequestProc.Parameters.AddWithValue("@user_name", username);
                    addHostRequestProc.Parameters.AddWithValue("@password", password);

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


                TempData["SignUp"] = "Succesfuly registered";
                return RedirectToAction("LogIn", "SportsAssociationManager");
            }
            //display error message
            TempData["failed"] = "Wrong input try again";
            return View();


        }



        public IActionResult ClubRepresentative()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClubRepresentative(String? username, String? password,String?name,String clubName)
        {
            var check1 = db.SystemUsers.Find(username);
            Club obj = db.Clubs.SingleOrDefault(user => user.Name == clubName);
            ClubRepresentative obj2 = null;

           
            if (obj != null)
            {
                obj2 = db.ClubRepresentatives.SingleOrDefault(user => user.ClubId.Equals(obj.Id));
            }



            if (check1 == null && obj!=null && username != null && obj2==null)
            {

                httpContextAccessor.HttpContext.Session.SetString("username", username);
                httpContextAccessor.HttpContext.Session.SetInt32("clubId", obj.Id);


                try
                {
                    SqlCommand addHostRequestProc = new SqlCommand("addRepresentative", conn);

                    addHostRequestProc.Parameters.AddWithValue("@name", name);
                    addHostRequestProc.Parameters.AddWithValue("@club_name", clubName);
                    addHostRequestProc.Parameters.AddWithValue("@user_name", username);
                    addHostRequestProc.Parameters.AddWithValue("@password", password);

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


                TempData["SignUp"] = "Succesfuly registered";
                return RedirectToAction("LogIn", "ClubRepresentative");
            }
            //diplay error message
            TempData["failed"] = "Wrong input try again";
            return View();
        }







        public IActionResult StadiumManager()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StadiumManager(String? username, String? password , String?name , String?stadiumName)
        {
            var check1 = db.SystemUsers.Find(username);
            Stadium obj = db.Stadiums.SingleOrDefault(user => user.Name == stadiumName);
            StadiumManager obj2 = null;
            if(obj != null)
            {
                 obj2 = db.StadiumManagers.SingleOrDefault(user => user.StadiumId.Equals(obj.Id));
            }



            if (check1 == null && obj != null && username != null && obj2 == null)
            {

                httpContextAccessor.HttpContext.Session.SetString("username", username);
                httpContextAccessor.HttpContext.Session.SetInt32("stadiumId", obj.Id);

                try
                {
                    SqlCommand addHostRequestProc = new SqlCommand("addStadiumManager", conn);

                    addHostRequestProc.Parameters.AddWithValue("@name", name);
                    addHostRequestProc.Parameters.AddWithValue("@stadium_name", stadiumName);
                    addHostRequestProc.Parameters.AddWithValue("@user_name", username);
                    addHostRequestProc.Parameters.AddWithValue("@password", password);

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


                TempData["SignUp"] = "Succesfuly registered";
                return RedirectToAction("LogIn", "StadiumManager");
            }
            TempData["failed"] = "Wrong input try again";
            return View();
        }




        public IActionResult Fan()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Fan(String? username, String? password,String?name,String?nationalId,DateTime?birthdate,String?address,int?phoneNumber)
        {

            var check = db.SystemUsers.Find(username);
            Fan obj = db.Fans.SingleOrDefault(user => user.NationalId == nationalId);

            if (check==null && username !=null && obj==null)
            {

                httpContextAccessor.HttpContext.Session.SetString("username", username);


                try
                {
                    SqlCommand proc = new SqlCommand("addFan", conn);

                    proc.Parameters.AddWithValue("@name", name);
                    proc.Parameters.AddWithValue("@username", username);
                    proc.Parameters.AddWithValue("@password", password);
                    proc.Parameters.AddWithValue("@national_id_number", nationalId);
                    proc.Parameters.AddWithValue("@birth_date", birthdate);
                    proc.Parameters.AddWithValue("@address", address);
                    proc.Parameters.AddWithValue("@phone_number", phoneNumber);


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


                TempData["SignUp"] = "Succesfuly registered";
                return RedirectToAction("LogIn", "Fan");


            }
            //display error message
            TempData["failed"] = "Wrong input try again";
            return View();
        }


    }
}
