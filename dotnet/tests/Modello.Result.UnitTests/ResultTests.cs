namespace Modello.Result.UnitTests;

public class ResultTests
{
    [Fact]
    public void GivenStringValue_WhenInitializeUsingConstructor_ThenSetsValue()
    {
        // Given
        var expectedString = "test string";

        // When
        var result = new Result<string>(expectedString);

        // Then
        Assert.Equal(expectedString, result.Value);
    }

    [Fact]
    public void GivenIntValue_WhenInitializeUsingConstructor_ThenSetsValue()
    {
        // Given
        var expectedInt = 123;

        // When
        var result = new Result<int>(expectedInt);

        // Then
        Assert.Equal(expectedInt, result.Value);
    }

    [Fact]
    public void GivenBoolValue_WhenInitializeUsingConstructor_ThenSetsValue()
    {
        // Given
        var expectedBool = true;

        // When
        var result = new Result<bool>(expectedBool);

        // Then
        Assert.Equal(expectedBool, result.Value);
    }

    [Fact]
    public void GivenObjectValue_WhenInitializeUsingConstructor_ThenSetsValue()
    {
        // Given
        var expectedObject = new TestObject();

        // When
        var result = new Result<TestObject>(expectedObject);

        // Then
        Assert.Equal(expectedObject, result.Value);
    }

    [Theory]
    [InlineData("test string")]
    [InlineData(123)]
    [InlineData(true)]
    public void GivenValue_WhenInitializeUsingConstructor_ThenSetsStatusToOk(object value)
    {
        // When
        var result = new Result<object>(value);

        // Then
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    [Theory]
    [InlineData("test string")]
    [InlineData(123)]
    [InlineData(true)]
    public void GivenValue_WhenInitializeUsingFactory_ThenSetsStatusToOk(object value)
    {
        // When
        var result = Result<object>.Success(value);

        // Then
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void Given_WhenInitializeUsingGenericFactory_ThenSetsStatusToOk()
    {
        // When
        var result = Result.Success();

        // Then
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Null(result.Value);
    }

    [Theory]
    [InlineData("test string")]
    [InlineData(123)]
    [InlineData(true)]
    public void GivenValue_WhenInitializeUsingGenericFactory_ThenSetsStatusToOk(object value)
    {
        // When
        var result = Result.Success(value);

        // Then
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void GivenError_WhenInitializeUsingFactory_ThenSetsStatusToErrorAndSetsErrors()
    {
        // Given
        var error = Guid.NewGuid().ToString();

        // When
        var result = Result<object>.Error(error);

        // Then
        Assert.Equal(ResultStatus.Error, result.Status);
        Assert.Equal(error, result.Errors.Single());
    }

    [Fact]
    public void GivenError_WhenInitializeUsingGenericFactory_ThenSetsStatusToErrorAndSetsErrors()
    {
        // Given
        var error = Guid.NewGuid().ToString();

        // When
        var result = Result.Error(error);

        // Then
        Assert.Equal(ResultStatus.Error, result.Status);
        Assert.Equal(error, result.Errors.Single());
    }

    [Fact]
    public void GivenStringValue_WhenConvertToResult_ThenSetsStatusToOkAndSetsValue()
    {
        // Given
        var expectedString = "test string";

        // When
        Result<string> result = expectedString;

        // Then
        Assert.Equal(expectedString, result.Value);
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    [Fact]
    public void GivenIntValue_WhenConvertToResult_ThenSetsStatusToOkAndSetsValue()
    {
        // Given
        var expectedInt = 123;

        // When
        Result<int> result = expectedInt;

        // Then
        Assert.Equal(expectedInt, result.Value);
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    [Fact]
    public void GivenBoolValue_WhenConvertToResult_ThenSetsStatusToOkAndSetsValue()
    {
        // Given
        var expectedBool = true;

        // When
        Result<bool> result = expectedBool;

        // Then
        Assert.Equal(expectedBool, result.Value);
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    [Fact]
    public void GivenObjectValue_WhenConvertToResult_ThenSetsStatusToOkAndSetsValue()
    {
        // Given
        var expectedObject = new TestObject();

        // When
        Result<TestObject> result = expectedObject;

        // Then
        Assert.Equal(expectedObject, result.Value);
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    [Fact]
    public void GivenResultValue_WhenConvertToResult_ThenKeepsStatusAndError()
    {
        // Given
        var result = Result.Error(Guid.NewGuid().ToString());

        // When
        Result<string> convertedResult = result;

        // Then
        Assert.NotNull(convertedResult);
        Assert.Null(convertedResult.Value);
        Assert.Equal(result.Status, convertedResult.Status);
        Assert.Equal(result.Errors, convertedResult.Errors);
    }

    private record TestObject;
}
