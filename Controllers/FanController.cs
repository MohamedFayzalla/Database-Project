using M3_V1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace M3_V1.Controllers
{
    public class FanController : Controller
    {
        private readonly M2Context db;
        private readonly IHttpContextAccessor httpContextAccessor;
        SqlConnection conn = new SqlConnection(@"Data Source=MSI;Initial Catalog=M2_;Integrated Security=True;Encrypt=False;");
        
        
        
        
        

        public FanController(M2Context db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }





        
        public IActionResult Index()
        {
            return View();
           // return View();
        }



        public IActionResult LogIn()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(Fan obj)
        {
            if (ModelState.IsValid)   //how to validate ?? it must exist in databse to be logged in
            {
                return View();
            }

          
           return View(obj);
        }


        public IActionResult ViewMatchesAvailable()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewMatchesAvailable(DateTime?time)
        {
            MyModel model;
            try
            {
                SqlCommand function = new SqlCommand("SELECT * FROM dbo.availableMatchesToAttend1(@date)", conn);
                function.Parameters.AddWithValue("@date", time);


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
            return View("DisplayMatchesAvailable", model);
        }



        public IActionResult DisplayMatchesAvailable(MyModel model)
        {
            return View(model);
        }


        //get
        public IActionResult PurchaseTicket(String?nationalId,String?hostClubName,String?guestClubName , DateTime?startTimeMatch)
        {

            String NationalId = httpContextAccessor.HttpContext.Session.GetString("NationalId");
            Fan obj  = db.Fans.SingleOrDefault(user => user.NationalId == NationalId);


            if(obj.Status==true)  //he is not bloked
            {
                try
                {
                    SqlCommand proc = new SqlCommand("purchaseTicket", conn);
                    proc.Parameters.AddWithValue("@national_id_number_fan", NationalId);
                    proc.Parameters.AddWithValue("@hosting_club_name", hostClubName);
                    proc.Parameters.AddWithValue("@guest_club_name", guestClubName);
                    proc.Parameters.AddWithValue("@start_time_match", startTimeMatch);
                    proc.CommandType = CommandType.StoredProcedure;


                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(proc);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    MyModel model = new MyModel();
                    model.Data = dt;



                    conn.Close();
                }
                catch(Exception e)
                {
                    TempData["failed"] = "Wrong input try again";
                    return View();
                }
                TempData["success"] = "process successfull";
            }

           
            return RedirectToAction("Login");



        }




    }


   



}
