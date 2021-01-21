using System.Configuration;
using System.Text;

namespace Lonsum.MyLonsum.Helper
{
    public class ConfigurationHelper
    {
        public static string CredentialAudience
        {
            get { return ConfigurationManager.AppSettings["Audience"]; }
        }

        public static string CredentialIssuer
        {
            get { return ConfigurationManager.AppSettings["Issuer"]; }
        }

        public static string CredentialSecurityKey
        {
            get { return ConfigurationManager.AppSettings["SecurityKey"]; }
        }
        
        public static byte[] GetSymmetricSecurityKeyAsBytes()
        {
            var securityKey = CredentialSecurityKey;
            byte[] data = Encoding.UTF8.GetBytes(securityKey);
            return data;
        }
    }
}
