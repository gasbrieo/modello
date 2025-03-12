namespace Modello.Result;

public class Result<TValue>
{
    public TValue? Value { get; init; }

    public ResultStatus Status { get; protected set; } = ResultStatus.Ok;

    public IEnumerable<string> Errors { get; protected set; } = [];

    protected Result()
    {
    }

    public Result(TValue? value)
    {
        Value = value;
    }

    protected Result(ResultStatus status)
    {
        Status = status;
    }

    public static Result<TValue> Success(TValue value)
    {
        return new(value);
    }

    public static Result<TValue> Error(string error)
    {
        return new(ResultStatus.Error) 
        { 
            Errors = [error] 
        };
    }

    public static implicit operator Result<TValue>(TValue value)
    {
        return new(value);
    }

    public static implicit operator Result<TValue>(Result result) => new(default(TValue))
    {
        Status = result.Status,
        Errors = result.Errors,
    };
}

