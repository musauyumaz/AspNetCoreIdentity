using System.Net;

namespace AspNetCoreIdentityApp.Application.Commons.Results
{
    public readonly record struct Result<T>
    {
        public bool IsSucceed { get; init; }
        public T? Data { get; init; }
        public string? ErrorMessage { get; init; }
        public HttpStatusCode StatusCode { get; init; }

        private Result(bool isSucceed, T? data, string? errorMessage, HttpStatusCode statusCode)
        {
            IsSucceed = isSucceed;
            Data = data;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public static Result<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(true, data, null, statusCode);

        public static Result<T> Fail(
            string errorMessage,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(false, default, errorMessage, statusCode);

        public override string ToString()
            => IsSucceed
                ? $"✅ Success ({typeof(T).Name})"
                : $"❌ Fail ({statusCodeText(StatusCode)}): {ErrorMessage}";

        private static string statusCodeText(HttpStatusCode code)
            => $"{(int)code} {code}";
    }

    public readonly record struct Result
    {
        public bool IsSucceed { get; init; }
        public string? ErrorMessage { get; init; }
        public HttpStatusCode StatusCode { get; init; }

        private Result(bool isSucceed, string? errorMessage, HttpStatusCode statusCode)
        {
            IsSucceed = isSucceed;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public static Result Success(HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(true, null, statusCode);

        public static Result Fail(
            string errorMessage,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(false, errorMessage, statusCode);

        public override string ToString()
            => IsSucceed
                ? $"✅ Success ({(int)StatusCode} {StatusCode})"
                : $"❌ Fail ({(int)StatusCode} {StatusCode}): {ErrorMessage}";
    }
}
