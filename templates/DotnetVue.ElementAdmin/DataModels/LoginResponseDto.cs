namespace DotnetVue.ElementAdmin.DataModels
{
    public class LoginResponseDto : ResponseDto
    {
        
        public LoginResponseDto(bool success, string message, string token = null) : base(success, message)
        {
            this.Token = token;
        }
        
        public string Token { get; set; }

    }
}