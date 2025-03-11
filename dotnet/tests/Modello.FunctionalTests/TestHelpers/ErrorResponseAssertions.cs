using Modello.Presentation.Responses;

namespace Modello.FunctionalTests.TestHelpers;

public static class ErrorResponseAssertions
{
    public static void ShouldHaveAnyValidationError(this ErrorResponse errorResponse)
    {
        var errors = errorResponse.Errors.ToArray();
        Assert.NotEmpty(errors);
    }

    public static ErrorResponseValidator ShouldHaveValidationError(this ErrorResponse errorResponse)
    {
        var errors = errorResponse.Errors.ToArray();
        Assert.NotEmpty(errors);
        return new ErrorResponseValidator(errors);
    }

    public class ErrorResponseValidator(ErrorItem[] errors)
    {
        private readonly ErrorItem[] _errors = errors;

        public ErrorResponseValidator WithError(string expectedDetail)
        {
            Assert.Contains(_errors, e => e.Error == expectedDetail);
            return this;
        }

        public ErrorResponseValidator WithDetail(string expectedDetail)
        {
            Assert.Contains(_errors, e => e.Detail == expectedDetail);
            return this;
        }
    }
}
