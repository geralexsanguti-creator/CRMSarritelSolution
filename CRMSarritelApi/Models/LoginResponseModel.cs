namespace CRMSarritelApi.Models
{
    public class LoginResponseModel
    {
        public required string Token { get; set; }
        public long TokenExpired { get; set; }
    }
}
