using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Text;

using Agazfoundation.Models;
public class SendMail
{
    string admintomail = "aliaadil107@gmail.com";
    string noreplyemail = "aliaadil107@gmail.com";
    string host = "aliaadil107@gmail.com";

    string logo = "https://alhawatourstravel.com/assets/images/common/logo.png";
    string uploadfile = "https://alhawatourstravel.com/Content/Uploadimg/";

  //  string siteurl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"].ToString();
    string smtpemail = "aliaadil107@gmail.com";
    string smtpemailpassword = "aadilali786aa";

    DataTable dtemail = new DataTable();
    private string _emailid;
    private string _BookingID;
    public string emailid
    {
        get { return _emailid; }
        set { _emailid = value; }
    }
    public string BookingID
    {
        get { return _BookingID; }
        set { _BookingID = value; }
    }

    private string _agencyId;
    public string agencyId
    {
        get { return _agencyId; }
        set { _agencyId = value; }
    }

    public SendMail()
    {
        //
        // TODO: Add constructor logic here
        //
        //fillemail();
    }

    public void fillemail()
    {


        //string query = "select * from tbl_superadmin";
        //dtemail = SqlHelper.ExecuteDataset(CommandType.Text, query).Tables[0];
        dtemail.ReadXml(HttpContext.Current.Server.MapPath("TestXML/adminemail.xml"));
        if (dtemail.Rows.Count > 0)
        {
            admintomail = dtemail.Rows[0]["Email"].ToString(); //"vijay.teamindiawebdesign@gmail.com";//dtemail.Rows[0]["adminemail"].ToString();
        }
        else
        {
            admintomail = "bookings@mtt4travel.com";
        }
    }

    //Send email to admin and user after submitting visa


    public void sendpaymentagencyemail(Agazfoundation.Models.add_donation add_donation)
    {
        //string bookingPaid = "";
        //string Body = "";
        //string sql = "select *,(select  ISNULL(((SUM(ISNULL(creditamount,0)+ISNULL(cancelamount,0)+ISNULL(payment,0))  -SUM(ISNULL(debitamount,0)))),0) as balance from tbl_agencybalance where AgencyID=tbl_agency.agencyid) as balance from tbl_agency where agencyid='" + getaddtopup.agencyid + "'";
        //string email = "";
        //string companyname = "";
        //string balance = "";
        //DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        //if (dt.Rows.Count > 0)
        //{
        //    email = dt.Rows[0]["email"].ToString();
        //    companyname = dt.Rows[0]["companyname"].ToString();
        //    balance = dt.Rows[0]["balance"].ToString();
        //}
        //string createddate = DateTime.UtcNow.ToString("dd/MM/yyyy");

        //string status = "confirm";
        //string sub_status = "accepted and approved";
        //if (getaddtopup.checkstatus.ToLower() == "decline")
        //{
        //    sub_status = "Declined";
        //    status = "Decline";
        //}

        StringBuilder str = new StringBuilder();
        str.AppendLine("<!DOCTYPE html><html><head><meta charset='utf-8'><meta http-equiv='x-ua-compatible' content='ie=edge'><title>Deposit Request Email</title><meta name='viewport' content='width=device-width,initial-scale=1'><style type='text/css'>@media screen{@font-face{font-family:'Source Sans Pro';font-style:normal;font-weight:400;src:local('Source Sans Pro Regular'),local('SourceSansPro-Regular'),url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff')}@font-face{font-family:'Source Sans Pro';font-style:normal;font-weight:700;src:local('Source Sans Pro Bold'),local('SourceSansPro-Bold'),url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff')}}a,body,table,td{-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%}table,td{mso-table-rspace:0;mso-table-lspace:0}img{-ms-interpolation-mode:bicubic}a[x-apple-data-detectors]{font-family:inherit!important;font-size:inherit!important;font-weight:inherit!important;line-height:inherit!important;color:inherit!important;text-decoration:none!important}div[style*='margin: 16px 0;']{margin:0!important}body{width:100%!important;height:100%!important;padding:0!important;margin:0!important}table{border-collapse:collapse!important}a{color:#1a82e2}img{height:auto;line-height:100%;text-decoration:none;border:0;outline:0}</style></head><body style='background-color:#e9ecef'><table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td align='center' bgcolor='#e9ecef'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width:600px'><tr><td align='left' bgcolor='#ffffff' style='padding:36px 24px 0;font-family:\"Source Sans Pro\",Helvetica,Arial,sans-serif;border-top:3px solid #d4dadf'><h1 style='margin:0;font-size:32px;font-weight:700;letter-spacing:-1px;line-height:48px'>Thank you " + add_donation.fname  + " " + add_donation.lname + "</h1></td></tr></table></td></tr><tr><td align='center' bgcolor='#e9ecef'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width:600px'><tr><td align='left' bgcolor='#ffffff' style='padding:24px;font-family:\"Source Sans Pro\",Helvetica,Arial,sans-serif;font-size:16px;line-height:24px'><p style='margin:0'>We successfully recieved your payment for an amount of  " + add_donation.amount + " made on " + add_donation.amount_type + " </p></td></tr><tr><td align='left' bgcolor='#ffffff' style='padding:24px;font-family:\"Source Sans Pro\",Helvetica,Arial,sans-serif;font-size:16px;line-height:24px;border-bottom:3px solid #d4dadf'><p style='margin:0'>Regards,<br>Agaz Foundation</p></td></tr></table></td></tr><tr><td align='center' bgcolor='#e9ecef' style='padding:24px'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width:600px'></table></td></tr></table></body></html>");

        System.Net.Mail.MailMessage objeto_mail_user = new System.Net.Mail.MailMessage();
        SmtpClient client_user = new SmtpClient("smtp.gmail.com");
        client_user.Port = 25;
        client_user.Host = "localhost";
   
        client_user.EnableSsl = true;
        client_user.DeliveryMethod = SmtpDeliveryMethod.Network;
        client_user.UseDefaultCredentials = false;
        client_user.Credentials = new System.Net.NetworkCredential(smtpemail, smtpemailpassword);
        objeto_mail_user.From = new MailAddress(noreplyemail);
        objeto_mail_user.To.Add(new MailAddress(add_donation.email));
        // objeto_mail_user.To.Add(new MailAddress("sunnytiwd@gmail.com"));
        objeto_mail_user.Bcc.Add(new MailAddress( admintomail));
        objeto_mail_user.Subject = "Thank you";
        objeto_mail_user.Body = str.ToString();
        objeto_mail_user.IsBodyHtml = true;
        client_user.Send(objeto_mail_user);
    }
}