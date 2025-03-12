﻿using Modello.Application.Common.Results;

namespace Modello.UnitTests.Application.Common.Results;

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
    public void GivenValue_WhenInitializeSuccessUsingFactory_ThenSetsStatusToOk(object value)
    {
        // When
        var result = Result<object>.Success(value);

        // Then
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal(value, result.Value);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Given_WhenInitializeSuccessUsingGenericFactory_ThenSetsStatusToOk()
    {
        // When
        var result = Result.Success();

        // Then
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Null(result.Value);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("test string")]
    [InlineData(123)]
    [InlineData(true)]
    public void GivenValue_WhenInitializeSuccessUsingGenericFactory_ThenSetsStatusToOk(object value)
    {
        // When
        var result = Result.Success(value);

        // Then
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal(value, result.Value);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void GivenError_WhenInitializeErrorUsingFactory_ThenSetsStatusToErrorAndSetsErrors()
    {
        // Given
        var error = Guid.NewGuid().ToString();

        // When
        var result = Result<object>.Error(error);

        // Then
        Assert.Equal(ResultStatus.Error, result.Status);
        Assert.Equal(error, result.Errors.Single());
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void GivenError_WhenInitializeErrorUsingGenericFactory_ThenSetsStatusToErrorAndSetsErrors()
    {
        // Given
        var error = Guid.NewGuid().ToString();

        // When
        var result = Result.Error(error);

        // Then
        Assert.Equal(ResultStatus.Error, result.Status);
        Assert.Equal(error, result.Errors.Single());
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Given_WhenInitializeNotFoundUsingFactory_ThenSetsStatusToErrorAndSetsErrors()
    {
        // When
        var result = Result<object>.NotFound();

        // Then
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Given_WhenInitializeNotFoundUsingGenericFactory_ThenSetsStatusToErrorAndSetsErrors()
    {
        // When
        var result = Result.NotFound();

        // Then
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.False(result.IsSuccess);
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
