using System;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security;
using System.Threading.Tasks;
using System.Web.Http;
using ApiTemplate.Repositories;
using ApiTemplate.Services;
using ApiTemplate.ViewModels;
using Microsoft.Exchange.WebServices.Data;
using Quartz;
using Serilog;
using Task = System.Threading.Tasks.Task;

namespace ApiTemplate.Commons.Helpers
{

    public class EmailJobDueDate : IJob
    {
        //private readonly IAllDeviceService _allDeviceService;
        //public EmailJobDueDate(IAllDeviceService allDeviceService)
        //{
        //    _allDeviceService = allDeviceService;
        //}
        private ExchangeService _exchangeService;
        public Task Execute(Quartz.IJobExecutionContext context)
        {

            //MailModel email = new MailModel();
            //email.Body = "tess";
            //// TODO ambil email to
            ////email.Recipient = emailto;
            //email.Recipient = "ridho.kurniawan@mii.co.id";
            //email.RecipientList = null;
            //email.Subject = "Notifikasi Checklist User";
            //await _exchangeManagedService.SendMail(email, null, "");
            //using (var baseContext = new BaseContext())
            //{
            //    ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 360;
            //    string commandText = "[dbo].[GetALLCategory] ";
            //    var listModel = baseContext.Database.SqlQuery<CategoryVM>(commandText).ToListAsync();

            //}

            //_logger.Information(@"Oh Hai \o/");
            var koneksi = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(koneksi);
            SqlCommand command = new SqlCommand("GetEngineOverdue", con);
            command.CommandType = CommandType.StoredProcedure;
            con.Open();
            command.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(command);
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            adp.Fill(ds);
            dt = ds.Tables[0];
            string a = string.Empty;
            string b = string.Empty;

            //using (MailMessage mail = new MailMessage())
            //{
            //    mail.From = new MailAddress("sendercvonline@gmail.com");
            //    mail.To.Add("baculsanjose@gmail.com");
            //    mail.Subject = a;
            //    mail.Body = b;//"<a href='http://lonsumimac'>tesssss</a>";//emailto.Body;
            //    mail.IsBodyHtml = true;

            //    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            //    {
            //        smtp.Credentials = new NetworkCredential("sendercvonline@gmail.com", "dian081193");
            //        smtp.EnableSsl = true;
            //        smtp.Send(mail);
            //    }
            //}
            var username = ConfigurationManager.AppSettings["Username"];
            var password = ConfigurationManager.AppSettings["Password"];
            var domain = ConfigurationManager.AppSettings["Domain"];
            var email = ConfigurationManager.AppSettings["Email"];
            this._exchangeService = new ExchangeService(ExchangeVersion.Exchange2013)
            {

                Credentials = new WebCredentials(username, password, domain)
            };

            _exchangeService.AutodiscoverUrl(email, RedirectionCallback);


            try
            {
                //var msgBody = new MessageBody
                //{
                //    BodyType = BodyType.HTML,
                //    Text = mailModel.Body
                //};
                foreach (DataRow rw in dt.Rows)
                {

                    var message = new EmailMessage(_exchangeService)
                    {
                        Subject = rw["Subject"].ToString(),
                        Body = new MessageBody(BodyType.HTML, rw["Body"].ToString()),
                    };
                    message.ToRecipients.Add(rw["Email"].ToString());

                    Task.Run(() => {
                        message.SendAndSaveCopy();
                    });
                }

                //return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return Task.CompletedTask;

        }

        private static bool RedirectionCallback(string url)
        {
            // Return true if the URL is an HTTPS URL.
            return url.ToLower().StartsWith("https://");
        }

        private SecureString ConvertStringToSecure(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return null;
            var result = new SecureString();
            foreach (char c in password) result.AppendChar(c);
            return result;
        }


    }
}