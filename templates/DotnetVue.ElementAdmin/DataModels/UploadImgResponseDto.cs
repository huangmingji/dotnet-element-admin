using System.Collections.Generic;

namespace DotnetVue.ElementAdmin.DataModels
{
    public class UploadImgResponseDto : ResponseDto
    {
        public UploadImgResponseDto(string message) : this(false, message)
        {
        }
        
        public UploadImgResponseDto(List<string> data ) : this(true, "", data)
        {
        } 

        public UploadImgResponseDto(bool success, string message, List<string> data = null) : base(success, message)
        {
            this.Data = data;
        }
        
        
        public List<string> Data { get; set; }
    }
}