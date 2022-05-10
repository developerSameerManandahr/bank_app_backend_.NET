using System.Collections;

namespace worksheet2.Model.Response
{
    public class BaseResponse
    {

        public string Message { get; set; }
        public string MessageType { get; set; }
        
        public IEnumerable Results { get; set; }

        public BaseResponse(string message, string messageType, IEnumerable results)
        {
            Message = message;
            MessageType = messageType;
            Results = results;
        }

        public BaseResponse(string message, string messageType)
        {
            Message = message;
            MessageType = messageType;
        }
    }
}