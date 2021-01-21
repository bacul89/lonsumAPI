namespace ApiTemplate.Requests
{
    public class LoginAccessRequest
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
    }
}