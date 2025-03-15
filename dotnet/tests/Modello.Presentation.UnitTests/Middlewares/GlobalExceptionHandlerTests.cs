using Modello.Foundation.AspNetCore;
using Modello.Presentation.Middlewares;

namespace Modello.Presentation.UnitTests.Middlewares;

public class GlobalExceptionHandlerTests
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    private readonly Mock<ILogger<GlobalExceptionHandler>> _loggerMock = new();
    private readonly DefaultHttpContext _context = new();
    private readonly GlobalExceptionHandler _handler;

    public GlobalExceptionHandlerTests()
    {
        _context.TraceIdentifier = Guid.NewGuid().ToString();
        _context.Request.Path = "/api/v1/error";
        _context.Response.Body = new MemoryStream();
        _handler = new(_loggerMock.Object);
    }

    [Fact]
    public async Task GivenValidationException_WhenTryHandleCalled_ThenWritesBadRequestResponse()
    {
        // Given
        var exception = new FluentValidation.ValidationException(
        [
            new() { ErrorCode = "Id must not be empty.", ErrorMessage = "The identifier of the workspace cannot be empty." },
            new() { ErrorCode = "Name must not be empty.", ErrorMessage = "The name of the workspace cannot be empty or contain only white spaces." }
        ]);

        // When
        var result = await _handler.TryHandleAsync(_context, exception, CancellationToken.None);
        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        var jsonResponse = await new StreamReader(_context.Response.Body).ReadToEndAsync();
        var errorResponse = JsonSerializer.Deserialize<ErrorListResponse>(jsonResponse, _serializerOptions);

        // Then
        Assert.True(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, _context.Response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", _context.Response.ContentType);

        Assert.NotNull(errorResponse);
        Assert.Equal(_context.Request.Path, errorResponse.Instance);
        Assert.Equal(_context.TraceIdentifier, errorResponse.TraceId);
        Assert.Equal(2, errorResponse.Errors.Count());

        var validationError1 = exception.Errors.ElementAt(0);
        var error1 = errorResponse.Errors.ElementAt(0);
        Assert.Equal("ValidationError", error1.Type);
        Assert.Equal(validationError1.ErrorCode, error1.Error);
        Assert.Equal(validationError1.ErrorMessage, error1.Detail);

        var validationError2 = exception.Errors.ElementAt(0);
        var error2 = errorResponse.Errors.ElementAt(0);
        Assert.Equal("ValidationError", error2.Type);
        Assert.Equal(validationError2.ErrorCode, error2.Error);
        Assert.Equal(validationError2.ErrorMessage, error2.Detail);
    }

    [Fact]
    public async Task GivenException_WhenTryHandleCalled_ThenWritesInternalServerErrorResponse()
    {
        // Given
        var exception = new Exception("The identifier of the workspace cannot be empty.");

        // When
        var result = await _handler.TryHandleAsync(_context, exception, CancellationToken.None);

        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        var jsonResponse = await new StreamReader(_context.Response.Body).ReadToEndAsync();
        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(jsonResponse, _serializerOptions);

        // Then
        Assert.True(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, _context.Response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", _context.Response.ContentType);

        Assert.NotNull(errorResponse);
        Assert.Equal(_context.Request.Path, errorResponse.Instance);
        Assert.Equal(_context.TraceIdentifier, errorResponse.TraceId);
        Assert.Equal("InternalServerError", errorResponse.Type);
        Assert.Equal("Internal Server Error", errorResponse.Error);
        Assert.Equal("An unexpected error occurred. Please, try again later.", errorResponse.Detail);
    }
}
