using System;
using EstusShots.Shared.Interfaces;

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

    public class ApiResponse<T> where T : class, new()
    {
        public OperationResult OperationResult { get; set; }

        public T Data { get; set; }

        public ApiResponse()
        {
            OperationResult = new OperationResult();
            Data = new T();
        }

        public ApiResponse(OperationResult operationResult)
        {
            OperationResult = operationResult;
            Data = new T();
        }
        
        public ApiResponse(T data)
        {
            OperationResult = new OperationResult();
            Data = data;
        }
        
        public ApiResponse(OperationResult operationResult, T data)
        {
            OperationResult = operationResult;
            Data = data;
        }
    }
    
    public class CriticalErrorResponse :IApiResponse{}
}