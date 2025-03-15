using Modello.Presentation.Configurations.Conventions;

namespace Modello.Presentation.UnitTests.Configurations.Conventions;

public class KebabCaseTransformerTests
{
    private readonly KebabCaseTransformer _transformer = new();

    [Theory]
    [InlineData("TestString", "test-string")]
    [InlineData("AnotherExample", "another-example")]
    [InlineData("SimpleTest", "simple-test")]
    [InlineData("NoChange", "no-change")]
    [InlineData("alllowercase", "alllowercase")]
    [InlineData("UPPERCASE", "uppercase")]
    [InlineData("", null)]
    [InlineData(null, null)]
    public void GivenInput_WhenTransformOutboundCalled_ThensConvertsInputToKebabCase(string? input, string? expected)
    {
        // When
        var result = _transformer.TransformOutbound(input);

        // Then
        Assert.Equal(expected, result);
    }
}
