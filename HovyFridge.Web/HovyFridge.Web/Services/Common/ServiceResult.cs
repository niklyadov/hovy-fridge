namespace HovyFridge.Web.Services.Common
{
    public class ServiceResult<TResult>
    {
        public ServiceResult(ServiceResultStatus status, TResult? result)
        {
            Status = status;
            Result = result;
        }

        public ServiceResultStatus Status { get; }
        public TResult? Result { get; }

        public bool TryGetResult(out TResult? result)
        {
            result = Result;
            return HasValue();
        }

        public bool HasValue()
        {
            return Result != null;
        }
    }

    public class ServiceResultSuccess<TResult> : ServiceResult<TResult>
    {
        public ServiceResultSuccess() : base(ServiceResultStatus.Success, default)
        {
        }
        public ServiceResultSuccess(TResult result) : base(ServiceResultStatus.Success, result)
        {
        }
    }

    public class ServiceResultFail<TResult> : ServiceResult<TResult>
    {
        public ServiceResultFail() : base(ServiceResultStatus.Fail, default)
        {
        }
        public ServiceResultFail(TResult result) : base(ServiceResultStatus.Fail, result)
        {
        }
    }

    public enum ServiceResultStatus
    {
        Success,
        Fail
    }
}