namespace DotnetVue.ElementAdmin.DataModels
{
    public class ResponseDto
    {
        public ResponseDto(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        public bool Success { get; set; }

        public string Message { get; set; }

    }
}
