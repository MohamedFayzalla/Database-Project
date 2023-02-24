using M3_V1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.CodeAnalysis;
using System.Data;
using System.Data.SqlClient;

namespace M3_V1.Controllers
{
    public class SystemAdminController : Controller
    {
        private readonly M2Context db;
        private readonly IHttpContextAccessor httpContextAccessor;
        SqlConnection conn = new SqlConnection(@"Data Source=MSI;Initial Catalog=M2_;Integrated Security=True;Encrypt=False;");


        public SystemAdminController(M2Context db,IHttpContextAccessor httpContextAccessor)
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
        public IActionResult SignUp(SystemAdmin model)
        {
            String username = (String)httpContextAccessor.HttpContext.Session.GetString("username");
            if (model!=null && model.SuperId==username)
            {
                db.SystemAdmins.Add(model);      //no validation still
                db.SaveChanges();
                return RedirectToAction("LogIn");
            }
            return RedirectToAction("Index");

        }





       


        public IActionResult LogIn()
        {
            return View();
        }


        public IActionResult addClub()
        {
            return View();
        }


        [HttpPost]
        public IActionResult addClub(String?name , String?location)
       {
            try
            {
                SqlCommand proc = new SqlCommand("addClub", conn);
                proc.Parameters.AddWithValue("@name_club", name);
                proc.Parameters.AddWithValue("@location_club", location);
                proc.CommandType = CommandType.StoredProcedure;



                conn.Open();
                proc.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception e)
            {
                TempData["failed"] = "Wrong input try again!";
                return View();
            }

            //no validation happens!!!!  mybe try and catch

            TempData["success"] = "process successfull";
            return RedirectToAction("LogIn", "SystemAdmin");
            
       }



        public IActionResult deleteClub()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult deleteClub(String? clubName)
        {
            try
            {
                SqlCommand proc = new SqlCommand("deleteClub", conn);
                proc.Parameters.AddWithValue("@name_club", clubName);

                proc.CommandType = CommandType.StoredProcedure;



                conn.Open();
                proc.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception e)
            {
                TempData["failed"] = "Wrong input try again!";
                return View();
            }

            //no validation happens!!!!  mybe try and catch

            TempData["success"] = "process successfull";
            return RedirectToAction("LogIn", "SystemAdmin");

        }







        public IActionResult addStadium()
        {
            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addStadium(Stadium obj)
       {

            if (ModelState.IsValid && obj != null)
            {

                try
                {
                    SqlCommand proc = new SqlCommand("addStadium", conn);
                    proc.Parameters.AddWithValue("@name_stadium", obj.Name);
                    proc.Parameters.AddWithValue("@location_stadium", obj.Location);
                    proc.Parameters.AddWithValue("@capacity_stadium", obj.Capacity);
                    proc.CommandType = CommandType.StoredProcedure;

                    //no validationsss!!!

                    conn.Open();
                    proc.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception e)
                {
                    TempData["failed"] = "Wrong input try again!";
                    return View();
                }

                TempData["success"] = "process successfull";
                return RedirectToAction("LogIn", "SystemAdmin");
            }

            else
            {
                TempData["failed"] = "Wrong input try again!";
                return View();
            }
           
        }
       
       //public IActionResult deleteStadium(String?name)
       //{
       //
       //}

        public IActionResult deleteStadium()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult deleteStadium(String?stadiumName)
        {
            try
            {
                SqlCommand proc = new SqlCommand("deleteStadium", conn);
                proc.Parameters.AddWithValue("@name_stadium", stadiumName);

                proc.CommandType = CommandType.StoredProcedure;

                //no validationsss!!!

                conn.Open();
                proc.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception e)
            {
                TempData["failed"] = "Wrong input try again!";
                return View();
            }

            TempData["success"] = "process successfull";
            return RedirectToAction("LogIn", "SystemAdmin");
        }


        public IActionResult blockFan()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult blockFan(String?nationalId)
        {

            try
            {
                SqlCommand proc = new SqlCommand("blockFan", conn);
                proc.Parameters.AddWithValue("@national_id_number_fan", nationalId);

                proc.CommandType = CommandType.StoredProcedure;

                //no validationsss!!!

                conn.Open();
                proc.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception e)
            {
                TempData["failed"] = "Wrong input try again!";
                return View();
            }

            TempData["success"] = "process successfull";
            return RedirectToAction("LogIn", "SystemAdmin");

        }





    }
}
