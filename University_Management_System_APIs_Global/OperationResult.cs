using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_System_APIs_Global
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();


        public static OperationResult<T> Success(T data, string message = "Operation completed successfully")
        {
            return new OperationResult<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                Errors = new List<string>()
            };
        }

        public static OperationResult<T> Failure(string message)
        {
            return new OperationResult<T>
            {
                IsSuccess = false,
                Message = message,
                Data = default,
                Errors = new List<string> { message }
            };
        }
    }

    public class OperationResult : OperationResult<object>
    {
        public static OperationResult Success(string message = "Operation completed successfully")
        {
            return new OperationResult()
            {
                IsSuccess = true,
                Message = message,
                Data = null,
                Errors = new List<string>()
            };
        }

        public new static OperationResult Failure(string message)
        {
            return new OperationResult()
            {
                IsSuccess = false,
                Message = message,
                Data = null,
                Errors = new List<string>() { message }
            };
        }

        public static OperationResult Failure(List<string> errors)
        {
            return new OperationResult()
            {
                IsSuccess = false,
                Message = string.Join("; ", errors),
                Data = null,
                Errors = errors
            };
        }
    }
}
