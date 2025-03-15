using Modello.FunctionalTests.TestHelpers;

namespace Modello.FunctionalTests.Features.Swagger;

public class SwaggerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetSwaggerUI_ShouldReturnOk()
    {
        // Act
        var response = await _client.GetAsync("swagger");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("v1")]
    public async Task GetSwaggerJson_ShouldReturnOk(string groupName)
    {
        // Act
        var response = await _client.GetAsync($"swagger/{groupName}/swagger.json");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
