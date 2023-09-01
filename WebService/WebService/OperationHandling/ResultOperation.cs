namespace WebService.OperationHandling
{
    /// <summary>
    /// Класс хранит результат выполнения операции
    /// Результат может являться успешным или ошибочным
    /// </summary>
    /// <typeparam name="TSuccess"></typeparam>
    /// <typeparam name="TError"></typeparam>
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
