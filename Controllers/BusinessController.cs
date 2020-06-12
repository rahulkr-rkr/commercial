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
    public class BusinessController : Controller
    {
        // GET: Business
       /* public ActionResult BusinessView()
        {
            return View();
        }*/
        [HttpGet]
        public ActionResult BusinessView(string name)
        {
            Business businessObj = new Business();
            using (SqlConnection con = new SqlConnection("Data Source=RAHUL\\SQLEXPRESS01;Initial Catalog=Commercial;Integrated Security=True"))
            {
                string sqlQuery = "select * from APPLICANT where first_name='" + name + "'";
                con.Open();
                SqlCommand sqlCmd = new SqlCommand(sqlQuery, con);
                SqlDataReader sdr = sqlCmd.ExecuteReader();
                if (sdr.Read())
                {        
                    businessObj.applicant_id = Convert.ToInt32(sdr["applicant_id"]);
                }
                con.Close();
            }

                return View(businessObj);
        }

        [HttpPost]
        public ActionResult BusinessView(Business business)
        {
            using (SqlConnection con = new SqlConnection("Data Source=RAHUL\\SQLEXPRESS01;Initial Catalog=Commercial;Integrated Security=True"))
            {
                // Insert query  
                string query = "INSERT INTO BUSINESS(business_name,industry_type,TIN,number_of_employees,business_entity_type,date_of_establishment,turnover_amount,applicant_id,annual_revenue) VALUES(@bname,@itype,@tin,@nofemployees,@bentity,@date,@tamount,@applicant_id,@annual_revenue)";
                //using stored procedure
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Connection = con;
                    // opening connection  
                    con.Open();
                    //cmd.CommandType = CommandType.StoredProcedure;
                    // Passing parameter values  
                    cmd.Parameters.AddWithValue("@bname", business.business_name);
                    cmd.Parameters.AddWithValue("@itype", business.indsustry_type);
                    cmd.Parameters.AddWithValue("@tin", business.tin);
                    cmd.Parameters.AddWithValue("@nofemployees", business.number_of_employees);
                    cmd.Parameters.AddWithValue("@bentity", business.business_entity_type);
                    cmd.Parameters.AddWithValue("@date", business.date_of_establishment);
                    cmd.Parameters.AddWithValue("@tamount", business.turnover_amount);
                    cmd.Parameters.AddWithValue("@annual_revenue", business.annual_revenue);
                    cmd.Parameters.AddWithValue("@applicant_id", business.applicant_id);
                    //cmd.Parameters.AddWithValue("@Query", 1);
                    // Executing stored procedure 
                    cmd.ExecuteNonQuery();
                    //status = (cmd.ExecuteNonQuery() >= 1) ? "Record is saved Successfully!" : "Record is not saved";

                    //status += "<br/>";
                    Session["business_name"] = business.business_name;
                    return RedirectToAction("BAddressView", "Business_Address", new { bname = @Session["business_name"] });

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

        public ActionResult BWelcome()
        {
            return View();
        }
    }
}