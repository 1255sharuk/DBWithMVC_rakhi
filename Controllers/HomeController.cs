using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace DBWithMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
            List<SelectListItem> country = new List<SelectListItem>();
            SqlCommand cmd = new SqlCommand("pro_country", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                country.Add(new SelectListItem
                {
                    Value = reader["cid"].ToString(),
                    Text = reader["cname"].ToString()
                }) ;
            }
            conn.Close();
            ViewBag.cname = country;
             return View();
        }
        public JsonResult GetState(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
            List<SelectListItem> state = new List<SelectListItem>();
            SqlCommand cmd = new SqlCommand("pro_state", conn);
           cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cid", id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                state.Add(new SelectListItem
                {
                    Value = reader["sid"].ToString(),
                    Text = reader["sname"].ToString()
                });
            }
            conn.Close();
            return Json(state, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetCity(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
            List<SelectListItem> City = new List<SelectListItem>();
            SqlCommand cmd = new SqlCommand("pro_city", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sid", id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                City.Add(new SelectListItem
                {
                    Value = reader["cityid"].ToString(),
                    Text = reader["cityname"].ToString()
                });
            }
            conn.Close();
            return Json(City, JsonRequestBehavior.AllowGet);
        }
    }
}