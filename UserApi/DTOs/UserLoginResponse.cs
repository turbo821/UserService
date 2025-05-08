namespace UserApi.DTOs
{
    public class UserLoginResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? AccessToken { get; set; }
    }
}
