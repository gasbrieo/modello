namespace Modello.FunctionalTests.TestHelpers;

public static class ErrorListResponseAssertions
{
    public static void ShouldHaveAnyValidationError(this ErrorListResponse errorListResponse)
    {
        var errors = errorListResponse.Errors.ToArray();
        Assert.NotEmpty(errors);
    }

    public static ErrorListResponseValidator ShouldHaveValidationError(this ErrorListResponse errorListResponse)
    {
        var errors = errorListResponse.Errors.ToArray();
        Assert.NotEmpty(errors);
        return new ErrorListResponseValidator(errors);
    }

    public class ErrorListResponseValidator(ErrorDetail[] errors)
    {
        private readonly ErrorDetail[] _errors = errors;

        public ErrorListResponseValidator WithType(string expectedType)
        {
            Assert.Contains(_errors, e => e.Type == expectedType);
            return this;
        }

        public ErrorListResponseValidator WithError(string expectedError)
        {
            Assert.Contains(_errors, e => e.Error == expectedError);
            return this;
        }

        public ErrorListResponseValidator WithDetail(string expectedDetail)
        {
            Assert.Contains(_errors, e => e.Detail == expectedDetail);
            return this;
        }
    }
}
