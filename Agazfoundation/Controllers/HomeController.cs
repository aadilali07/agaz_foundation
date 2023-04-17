using Agazfoundation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Agazfoundation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Add_donation(string id)
        {

            Agazfoundation.Models.add_donation add_donation = new Agazfoundation.Models.add_donation();

            if (id != null)
            {

                string str = "select * from  tbl_donation where id='" + id + "'";
                DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, str).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    add_donation.fname = dt.Rows[0]["fname"].ToString();
                    add_donation.lname = dt.Rows[0]["lname"].ToString();

                

                    if (dt.Rows[0]["imageurl"] != null && dt.Rows[0]["imageurl"].ToString() != "")
                    {

                        add_donation.imageurl = dt.Rows[0]["imageurl"].ToString();

                    }
                   
                    add_donation.id = dt.Rows[0]["id"].ToString();
                    add_donation.fathername = dt.Rows[0]["fathername"].ToString();
                    add_donation.mothername = dt.Rows[0]["mothername"].ToString();
                    add_donation.amount = dt.Rows[0]["amount"].ToString();
                    add_donation.city = dt.Rows[0]["city"].ToString();
                    add_donation.country = dt.Rows[0]["country"].ToString();
                    add_donation.phone = dt.Rows[0]["phone"].ToString();
                    add_donation.email = dt.Rows[0]["email"].ToString();
                    add_donation.amount_type = dt.Rows[0]["amount_type"].ToString();
                    add_donation.message = dt.Rows[0]["message"].ToString();

                }
            }
            return View(add_donation);
        }

        [HttpPost]
        public ActionResult insert_donation(Agazfoundation.Models.add_donation add_donation)
        {
           

            HttpPostedFileBase file = Request.Files["fileupload"];
            string imageurl = "";
            if (add_donation.imageurl != null && add_donation.imageurl != "")
            {
                imageurl = add_donation.imageurl;
            }

            string uploadpayslipss;

            if (file != null && file.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file.FileName.Split('.')[1];
                file.SaveAs(path + uploadpayslipss);
                imageurl = uploadpayslipss;

            }

            if (add_donation.id != null)
            {
                string sql = "update  tbl_donation set fname='" + add_donation.fname + "',lname='" + add_donation.lname + "',fathername='" + add_donation.fathername + "',imageurl='" + imageurl + "',mothername='" + add_donation.mothername + "',phone='" + add_donation.phone + "',email='" + add_donation.email + "',city='" + add_donation.city + "',country='" + add_donation.country + "',amount='" + add_donation.amount + "',amount_type='" + add_donation.amount_type + "',message='" + add_donation.message + "'    where id='" + add_donation.id + "' ";
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql);

            }

            else
            {
                string sql = "insert into tbl_donation(fname,lname,imageurl,fathername,mothername,phone,email,city,country,amount,amount_type,message) values('" + add_donation.fname + "','" + add_donation.lname + "','" + imageurl + "','" + add_donation.fathername + "','" + add_donation.mothername + "','" + add_donation.phone + "','" + add_donation.email + "','" + add_donation.city + "','" + add_donation.country + "','" + add_donation.amount + "','" + add_donation.amount_type + "','" + add_donation.message + "')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, sql);

                 
            }

                return RedirectToAction("view_donation");
        }


        public ActionResult view_donation(Agazfoundation.Models.agencyfillter agencyfillter)
         {

            List<Agazfoundation.Models.add_donation> add_donation = new List<Agazfoundation.Models.add_donation>();


            TempData["name"] = agencyfillter.name;
            TempData["countryname"] = agencyfillter.countryname;
            TempData["status"] = agencyfillter.status;
            TempData["defrom"] = agencyfillter.departurefrom;


            string conditions = "";
            if (agencyfillter != null)
            {


                if (agencyfillter.status != null && agencyfillter.status != null)
                {
                    if (conditions != "")
                    {
                        conditions = conditions + " and amount_type='" + agencyfillter.status.ToString().Replace("'", "").ToLower() + "'";
                    }
                    else
                    {
                        conditions = conditions + " amount_type='" + agencyfillter.status.ToString().Replace("'", "").ToLower() + "'";
                    }
                }



                if (agencyfillter.name != null && agencyfillter.name != "")
                {
                    if (conditions != "")
                    {
                        conditions = conditions + " and fname ='" + agencyfillter.name.ToString().Replace("'", "") + "'";
                    }
                    else
                    {
                        conditions = conditions + " fname ='" + agencyfillter.name.ToString().Replace("'", "") + "'";
                    }
                }

                if (agencyfillter.countryname != null && agencyfillter.countryname != "")
                {
                    if (conditions != "")
                    {
                        conditions = conditions + " and country ='" + agencyfillter.countryname.ToString().Replace("'", "") + "'";
                    }
                    else
                    {
                        conditions = conditions + " country ='" + agencyfillter.countryname.ToString().Replace("'", "") + "'";
                    }
                }




                if (conditions != "")
                {
                    conditions = " where " + conditions + "";
                }
            }
           
            string sql = "select * from tbl_donation " + conditions + " order by id desc ";

            DataTable dtr = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            if (dtr.Rows.Count > 0)
            {
                foreach (DataRow dr in dtr.Rows)
                {

                    add_donation.Add(new Agazfoundation.Models.add_donation()
                    {
                        fname = dr["fname"].ToString(),
                        lname = dr["lname"].ToString(),
                        fathername = dr["fathername"].ToString(),
                        mothername = dr["mothername"].ToString(),
                        city = dr["city"].ToString(),
                        country = dr["country"].ToString(),
                        phone = dr["phone"].ToString(),
                        email = dr["email"].ToString(),
                        amount = dr["amount"].ToString(),
                        amount_type = dr["amount_type"].ToString(),
                        message = dr["message"].ToString(),
                        imageurl = dr["imageurl"].ToString(),
                       
                        id = dr["id"].ToString(),
                      

                    });
                }
            }
          //string str = "SELECT SUM(amount) FROM tbl_donation";

          //  DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, str).Tables[0];

          //  string total = dt.Rows[0]["id"].ToString();
            return View(add_donation);
        }

        public ActionResult delete_donation(string id)
        {
            if (id != null && id.ToString() != "")
            {
                string sql = "delete from tbl_donation where id='" + id.ToString() + "'";
                SqlHelper.ExecuteDataset(CommandType.Text, sql);
                return RedirectToAction("view_donation");
            }
            else
            {
                return RedirectToAction("view_donation");
            }
        }


        public ActionResult donation_reciept(string id)
        {
            Agazfoundation.Models.add_donation  add_donation = new Agazfoundation.Models.add_donation();

            string sql = "select * from tbl_donation where id= '" +id +"' ";

            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];

            add_donation.fname = dt.Rows[0]["fname"].ToString();
            add_donation.lname = dt.Rows[0]["lname"].ToString();



            if (dt.Rows[0]["imageurl"] != null && dt.Rows[0]["imageurl"].ToString() != "")
            {

                add_donation.imageurl = dt.Rows[0]["imageurl"].ToString();

            }

            add_donation.id = dt.Rows[0]["id"].ToString();
            add_donation.fathername = dt.Rows[0]["fathername"].ToString();
            add_donation.mothername = dt.Rows[0]["mothername"].ToString();
            add_donation.amount = dt.Rows[0]["amount"].ToString();
            add_donation.city = dt.Rows[0]["city"].ToString();
            add_donation.country = dt.Rows[0]["country"].ToString();
            add_donation.phone = dt.Rows[0]["phone"].ToString();
            add_donation.email = dt.Rows[0]["email"].ToString();
            add_donation.amount_type = dt.Rows[0]["amount_type"].ToString();
            add_donation.message = dt.Rows[0]["message"].ToString();

            return View(add_donation);
        }

        public ActionResult email_donation(string id)
        {
            Agazfoundation.Models.add_donation add_donation = new Agazfoundation.Models.add_donation();

            string sql = "select * from tbl_donation where id= '" + id + "' ";

            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];

            add_donation.fname = dt.Rows[0]["fname"].ToString();
            add_donation.lname = dt.Rows[0]["lname"].ToString();



            if (dt.Rows[0]["imageurl"] != null && dt.Rows[0]["imageurl"].ToString() != "")
            {

                add_donation.imageurl = dt.Rows[0]["imageurl"].ToString();

            }

            add_donation.id = dt.Rows[0]["id"].ToString();
            add_donation.fathername = dt.Rows[0]["fathername"].ToString();
            add_donation.mothername = dt.Rows[0]["mothername"].ToString();
            add_donation.amount = dt.Rows[0]["amount"].ToString();
            add_donation.city = dt.Rows[0]["city"].ToString();
            add_donation.country = dt.Rows[0]["country"].ToString();
            add_donation.phone = dt.Rows[0]["phone"].ToString();
            add_donation.email = dt.Rows[0]["email"].ToString();
            add_donation.amount_type = dt.Rows[0]["amount_type"].ToString();
            add_donation.message = dt.Rows[0]["message"].ToString();


            SendMail SendMail = new SendMail();

               SendMail.sendpaymentagencyemail(add_donation);


            return RedirectToAction("view_donation");
        }
    }
}