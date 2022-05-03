namespace worksheet2.Model.Response
{
    public class BaseResponse
    {
        public BaseResponse(string message, string messageType)
        {
            Message = message;
            MessageType = messageType;
        }

        public string Message { get; set; }
        public string MessageType { get; set; }
    }
}