using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using System.Web.Hosting;
using Serilog;
using Task = System.Threading.Tasks.Task;
using ApiTemplate.Models;
using System.Configuration;

namespace ApiTemplate.Commons.Helpers
{
    public interface IExchangeManagedService
    {
        Task<bool> SendMail(MailModel mailModel, byte[] bytes, string NamaFile);
    }
    public class ExchangeManagedService : IExchangeManagedService
    {
        private ExchangeService _exchangeService;
        public async Task<bool> SendMail(MailModel mailModel, byte[] bytes, string NamaFile)
        {
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

                var message = new EmailMessage(_exchangeService)
                {
                    Subject = mailModel.Subject,
                    Body = new MessageBody(BodyType.HTML, mailModel.Body),
                };
                //message.Body = new MessageBody(mailModel.Body);
                message.ToRecipients.Add(mailModel.Recipient);
                if (bytes != null)
                {
                    if (bytes.Length > 0)
                        message.Attachments.AddFileAttachment(NamaFile, bytes);
                }
                if (mailModel.RecipientList != null)
                {
                    if (mailModel.RecipientList.Count != 0)
                    {
                        foreach (var recipient in mailModel.RecipientList)
                        {
                            message.ToRecipients.Add(recipient);
                        }
                    }
                }

                await Task.Run(() => {
                    message.SendAndSaveCopy();
                });
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

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