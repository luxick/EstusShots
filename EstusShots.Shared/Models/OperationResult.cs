using System;

namespace EstusShots.Shared.Models
{
    public class OperationResult
    {
        public bool Success { get; }
        public string ShortMessage { get; set; }
        public string DetailedMessage { get; set; }
        public string StackTrace { get; set; }

        public OperationResult()
        {
            Success = true;
        }

        public OperationResult(bool success, string shortMessage, string detailedMessage = null)
        {
            Success = success;
            ShortMessage = shortMessage;
            DetailedMessage = detailedMessage;
        }

        public OperationResult(Exception e)
        {
            Success = false;
            ShortMessage = e.Message;
            DetailedMessage = e.InnerException?.Message;
            StackTrace = e.StackTrace;
        }
    }
}