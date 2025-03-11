using Modello.Domain.Common.Exceptions;
using Modello.Presentation.Middlewares;
using Modello.Presentation.Responses;

namespace Modello.UnitTests.Presentation.Middlewares;

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
    public async Task TryHandleAsync_WhenHandlingValidationException_ShouldWriteBadRequestResponse()
    {
        // Arrange
        var exception = new FluentValidation.ValidationException(
        [
            new() { ErrorCode = "Id must not be empty.", ErrorMessage = "The identifier of the workspace cannot be empty." },
            new() { ErrorCode = "Name must not be empty.", ErrorMessage = "The name of the workspace cannot be empty or contain only white spaces." }
        ]);

        // Act
        var result = await _handler.TryHandleAsync(_context, exception, CancellationToken.None);
        var errorResponse = await GetErrorResponse();

        // Assert
        Assert.True(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, _context.Response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", _context.Response.ContentType);

        Assert.NotNull(errorResponse);
        Assert.Equal(_context.Request.Path, errorResponse.Instance);
        Assert.Equal(_context.TraceIdentifier, errorResponse.TraceId);
        Assert.Equal(2, errorResponse.Errors.Count);

        Assert.Equal(exception.Errors.ElementAt(0).ErrorCode, errorResponse.Errors.ElementAt(0).Error);
        Assert.Equal(exception.Errors.ElementAt(0).ErrorMessage, errorResponse.Errors.ElementAt(0).Detail);

        Assert.Equal(exception.Errors.ElementAt(1).ErrorCode, errorResponse.Errors.ElementAt(1).Error);
        Assert.Equal(exception.Errors.ElementAt(1).ErrorMessage, errorResponse.Errors.ElementAt(1).Detail);
    }

    [Fact]
    public async Task TryHandleAsync_WhenHandlingBadRequestException_ShouldWriteBadRequestResponse()
    {
        // Arrange
        var exception = new ConcreteBadRequestException();

        // Act
        var result = await _handler.TryHandleAsync(_context, exception, CancellationToken.None);
        var errorResponse = await GetErrorResponse();

        // Assert
        Assert.True(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, _context.Response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", _context.Response.ContentType);

        Assert.NotNull(errorResponse);
        Assert.Equal(_context.Request.Path, errorResponse.Instance);
        Assert.Equal(_context.TraceIdentifier, errorResponse.TraceId);
        Assert.Single(errorResponse.Errors);

        Assert.Equal(exception.Error, errorResponse.Errors.ElementAt(0).Error);
        Assert.Equal(exception.Detail, errorResponse.Errors.ElementAt(0).Detail);
    }

    [Fact]
    public async Task TryHandleAsync_WhenHandlingNotFoundException_ShouldWriteNotFoundResponse()
    {
        // Arrange
        var exception = new ConcreteNotFoundException();

        // Act
        var result = await _handler.TryHandleAsync(_context, exception, CancellationToken.None);
        var errorResponse = await GetErrorResponse();

        // Assert
        Assert.True(result);
        Assert.Equal((int)HttpStatusCode.NotFound, _context.Response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", _context.Response.ContentType);
        
        Assert.NotNull(errorResponse);
        Assert.Equal(_context.Request.Path, errorResponse.Instance);
        Assert.Equal(_context.TraceIdentifier, errorResponse.TraceId);
        Assert.Single(errorResponse.Errors);

        Assert.Equal(exception.Error, errorResponse.Errors.ElementAt(0).Error);
        Assert.Equal(exception.Detail, errorResponse.Errors.ElementAt(0).Detail);
    }

    [Fact]
    public async Task TryHandleAsync_WhenHandlingException_ShouldWriteInternalServerErrorResponse()
    {
        // Arrange
        var exception = new Exception("The identifier of the workspace cannot be empty.");

        // Act
        var result = await _handler.TryHandleAsync(_context, exception, CancellationToken.None);
        var errorResponse = await GetErrorResponse();

        // Assert
        Assert.True(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, _context.Response.StatusCode);
        Assert.Equal("application/json; charset=utf-8", _context.Response.ContentType);

        Assert.NotNull(errorResponse);
        Assert.Equal(_context.Request.Path, errorResponse.Instance);
        Assert.Equal(_context.TraceIdentifier, errorResponse.TraceId);
        Assert.Single(errorResponse.Errors);

        Assert.Equal("Internal Server Error", errorResponse.Errors.ElementAt(0).Error);
        Assert.Equal("An unexpected error occurred. Please, try again later.", errorResponse.Errors.ElementAt(0).Detail);
    }

    private async Task<ErrorResponse?> GetErrorResponse()
    {
        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        var jsonResponse = await new StreamReader(_context.Response.Body).ReadToEndAsync();
        return JsonSerializer.Deserialize<ErrorResponse>(jsonResponse, _serializerOptions);
    }

    public class ConcreteBadRequestException() : BadRequestException("Name must not be empty.", "The name of the workspace cannot be empty or contain only white spaces.");

    public class ConcreteNotFoundException() : NotFoundException("Workspace not found.", "The workspace with the provided identifier was not found.");
}
