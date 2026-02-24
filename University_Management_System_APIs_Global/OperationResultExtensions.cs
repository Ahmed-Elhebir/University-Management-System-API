using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_System_APIs_Global
{
    public static class OperationResultExtensions
    {
        /// <summary>
        /// Converts OperationResult<T> to non-generic OperationResult
        /// (discards the data, keeps only success/failure and message)
        /// </summary>
        public static OperationResult ToNonGeneric<T>(this OperationResult<T> result)
        {
            if (result.IsSuccess)
                return OperationResult.Success(result.Message);
            else
                return OperationResult.Failure(result.Message);
        }

    }
}
