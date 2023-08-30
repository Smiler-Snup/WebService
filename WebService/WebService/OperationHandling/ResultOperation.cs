using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.OperationHandling
{
    public class ResultOperation<TSuccess, TError>
    {
        public bool IsSuccess { get; }
        public TSuccess SuccessValue { get; }
        public TError ErrorValue { get; }

        private ResultOperation(bool isSuccess, TSuccess successValue, TError errorValue)
        {
            IsSuccess = isSuccess;
            SuccessValue = successValue;
            ErrorValue = errorValue;
        }

        public static ResultOperation<TSuccess, TError> Success(TSuccess value) =>
            new ResultOperation<TSuccess, TError>(true, value, default);

        public static ResultOperation<TSuccess, TError> Error(TError value) =>
            new ResultOperation<TSuccess, TError>(false, default, value);
    }
}
