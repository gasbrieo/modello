using System.Net.Http.Json;
using Modello.Application.Workspaces;
using Modello.FunctionalTests.TestHelpers;
using Modello.Presentation.Requests.V1;
using Modello.Presentation.Responses;

namespace Modello.FunctionalTests.Features.Workspaces;

public class CreateWorkspaceTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task CreateWorkspace_ShouldReturnCreated()
    {
        // Arrange
        var createRequest = new CreateWorkspaceRequest() { Name = "Work" };

        // Act 
        var createResponse = await _client.PostAsJsonAsync("/api/v1/workspaces", createRequest);
        var createResult = await createResponse.Content.ReadFromJsonAsync<WorkspaceDto>();

        // Assert
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        Assert.NotNull(createResult);
        Assert.NotEqual(Guid.Empty, createResult.Id);
        Assert.Equal(createRequest.Name, createResult.Name);

        var getResponse = await _client.GetAsync($"/api/v1/workspaces/{createResult.Id}");
        var getResult = await getResponse.Content.ReadFromJsonAsync<WorkspaceDto>();
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.NotNull(getResult);
        Assert.Equal(createResult.Id, getResult.Id);
        Assert.Equal(createResult.Name, getResult.Name);
    }

    [Fact]
    public async Task CreateWorkspace_WhenNameIsEmpty_ShouldReturnBadRequest()
    {
        // Arrange
        var createRequest = new CreateWorkspaceRequest();

        // Act
        var createResponse = await _client.PostAsJsonAsync("/api/v1/workspaces", createRequest);
        var createResult = await createResponse.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, createResponse.StatusCode);
        Assert.NotNull(createResult);

        createResult.ShouldHaveValidationError()
            .WithError("Name must not be empty.")
            .WithDetail("The name of the workspace cannot be empty or contain only white spaces.");
    }
}
