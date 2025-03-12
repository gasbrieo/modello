namespace Modello.Result;

public class Result : Result<Result>
{
    public Result() : base()
    {
    }

    protected internal Result(ResultStatus status) : base(status)
    {
    }

    public static Result Success()
    {
        return new();
    }

    public static Result<T> Success<T>(T value)
    {
        return new(value);
    }

    public new static Result Error(string error)
    {
        return new(ResultStatus.Error)
        {
            Errors = [error]
        };
    }
}
