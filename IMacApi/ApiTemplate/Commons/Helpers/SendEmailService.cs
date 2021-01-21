using ApiTemplate.Requests;
using ApiTemplate.Responses;
using ApiTemplate.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Serilog;

namespace ApiTemplate.Commons.Helpers
{
    public interface ISendEmailService
    {
        void SendMail(SendEmailRequest model, byte[] bytesFile);
    }
    public class SendEmailService : ISendEmailService
    {
        private readonly ILogger _logger;
        public SendEmailService(ILogger logger)
        {
            _logger = logger;
        }
        public async void SendMail(SendEmailRequest model, byte[] bytesFile)
        {
            HttpClient _clientSendEmail = new HttpClient();
            var clientid = ConfigurationManager.AppSettings["clientid"];
            var urloath = ConfigurationManager.AppSettings["urloath"];
            var appid = ConfigurationManager.AppSettings["appid"];
            var urlsendemail = ConfigurationManager.AppSettings["urlsendemail"];
            string logerror = string.Format(@"client id={0}, urloauth= {1}, urlsendemail= {2}, appid = {3}", clientid, urloath, urlsendemail, appid);

            var content = new FormUrlEncodedContent(new[]
                        {
                                //uncoment saat publish
                                new KeyValuePair<string, string>("username", model.userlogin),
                                //new KeyValuePair<string, string>("username", "guest.mii@londonsumatra.com"), 
                                new KeyValuePair<string, string>("grant_type", "password"),
                                new KeyValuePair<string, string>("client_id", clientid)
                            });
            try
            {
               
                var result = await _clientSendEmail.PostAsync(urloath, content);
                if (result.IsSuccessStatusCode)
                {
                    var resultapi = await result.Content.ReadAsStringAsync();
                    var appoints = JsonConvert.DeserializeObject<ResponseTokenVM>(resultapi);
                    using (var multipartFormDataContent = new MultipartFormDataContent())
                    {
                        var values = new[]
                        {
                                new KeyValuePair<string, string>("appid", appid),
                                //uncoment saat publish
                                new KeyValuePair<string, string>("recipient", model.recipient),
                                //new KeyValuePair<string, string>("recipient", "guest.mii@londonsumatra.com"), 
                                new KeyValuePair<string, string>("subject", model.subject),
                                new KeyValuePair<string, string>("content", model.content)
                             };

                        foreach (var keyValuePair in values)
                        {
                            multipartFormDataContent.Add(new StringContent(keyValuePair.Value, Encoding.UTF8, "application/json"),
                                String.Format("{0}", keyValuePair.Key));
                        }
                        if (bytesFile != null)
                        {
                            var byteArrayContent = new ByteArrayContent(bytesFile);
                            byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/pdf");
                            multipartFormDataContent.Add(byteArrayContent, "attachments", "SuratPernyataan.pdf");
                        }

                        _clientSendEmail.DefaultRequestHeaders.Clear();
                        _clientSendEmail.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", appoints.AccessToken);
                        var responseSendemail = await _clientSendEmail.PostAsync(urlsendemail, multipartFormDataContent);//, new MultipartFormDataContent
                        if (responseSendemail.IsSuccessStatusCode)
                        {
                            var responseapi = await responseSendemail.Content.ReadAsStringAsync();
                            var responseapisendemail = JsonConvert.DeserializeObject<Response>(responseapi);
                            _logger?.Information($"There was an information on send email invocation: {responseapisendemail.Message + " - "+ logerror}".ToUpper());

                        }
                        else
                        {
                            var responseapi = await responseSendemail.Content.ReadAsStringAsync();
                            var responseapisendemail = JsonConvert.DeserializeObject<Response>(responseapi);
                            _logger?.Information($"There was an error on send email post invocation: {responseapisendemail.Message + " - "+ logerror}".ToUpper());
                        }
                    }
                }
                else
                {
                    var responseapi = await result.Content.ReadAsStringAsync();
                    var responseapisendemail = JsonConvert.DeserializeObject<Response>(responseapi);                    
                    _logger?.Information($"There was an error on send email get barrier invocation: {responseapisendemail.Message + " - " + logerror}".ToUpper());
                }

            }
            catch (Exception ex)
            {
                _logger?.Information($"There was an error on send email invocation: {ex.Message + " - " + logerror}".ToUpper());
            }
        }
    }
}