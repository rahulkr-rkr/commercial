using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommercialApp.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace CommercialApp.Controllers
{
    public class ApplicantController : Controller
    {
        // GET: Applicant
        public ActionResult ApplicantView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ApplicantView(Applicant applicant)
        {
            using (SqlConnection con = new SqlConnection("Data Source=RAHUL\\SQLEXPRESS01;Initial Catalog=Commercial;Integrated Security=True"))
            {
                // Insert query  
                //string query = "INSERT INTO APPLICANT(first_name,last_name,email,phone_number,role,ssn) VALUES(@fname, @lname, @email, @phone_number, @role, @ssn)";
                //using stored procedure
                using (SqlCommand cmd = new SqlCommand("Hrj_InsertUpdateDelete_Applicant", con))
                {
                    cmd.Connection = con;
                    // opening connection  
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Passing parameter values  
                    cmd.Parameters.AddWithValue("@fname", applicant.first_name);
                    cmd.Parameters.AddWithValue("@lname", applicant.last_name);
                    cmd.Parameters.AddWithValue("@email", applicant.email);
                    cmd.Parameters.AddWithValue("@phone", applicant.phone);
                    cmd.Parameters.AddWithValue("@role", applicant.role);
                    cmd.Parameters.AddWithValue("@ssn", applicant.ssn);
                    cmd.Parameters.AddWithValue("@Query", 1);
                    // Executing stored procedure 
                    cmd.ExecuteNonQuery();
                    //status = (cmd.ExecuteNonQuery() >= 1) ? "Record is saved Successfully!" : "Record is not saved";

                    //status += "<br/>";
                    Session["first_name"] = applicant.first_name;
                    return RedirectToAction("BusinessView", "Business", new { name = @Session["first_name"] });

                }
                /*
                using (SqlCommand cmdd = new SqlCommand("select * from APPLICANT where first_name="+ applicant.first_name,con))
                {
                    cmdd.Connection = con;
                    //con.Open();
                    SqlDataReader sdr = cmdd.ExecuteReader();
                    if (sdr.Read())
                    {
                        Session["applicant_id"] = sdr["applicant_id"].ToString();
                        
                    }
                   
                }
                
              */
                con.Close();
            }
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }
    }
}