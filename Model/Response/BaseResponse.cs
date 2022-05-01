namespace worksheet2.Model.Response
{
    public class BaseResponse
    {
        public string Message { get; set; }
        public string MessageType { get; set; }


        public BaseResponse(string message, string messageType)
        {
            this.Message = message;
            this.MessageType = messageType;
        }
    }
}