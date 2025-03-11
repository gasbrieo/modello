using System.Net.Http.Json;
using Modello.Application.Workspaces;
using Modello.FunctionalTests.TestHelpers;
using Modello.Presentation.Requests.V1;
using Modello.Presentation.Responses;

namespace Modello.FunctionalTests.Features.Workspaces;

public class GetWorkspaceTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetWorkspace_ShouldReturnOk()
    {
        // Arrange
        var createRequest = new CreateWorkspaceRequest() { Name = "Work" };
        var createResponse = await _client.PostAsJsonAsync("/api/v1/workspaces", createRequest);
        var createResult = await createResponse.Content.ReadFromJsonAsync<WorkspaceDto>();
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        Assert.NotNull(createResult);

        // Act 
        var getResponse = await _client.GetAsync($"/api/v1/workspaces/{createResult.Id}");
        var getResult = await getResponse.Content.ReadFromJsonAsync<WorkspaceDto>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.NotNull(getResult);
        Assert.Equal(createResult.Id, getResult.Id);
        Assert.Equal(createResult.Name, getResult.Name);
    }

    [Fact]
    public async Task GetWorkspace_WhenIdIsEmpty_ShouldReturnBadRequest()
    {
        // Arrange
        var id = Guid.Empty;

        // Act
        var getResponse = await _client.GetAsync($"/api/v1/workspaces/{id}");
        var getResult = await getResponse.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, getResponse.StatusCode);
        Assert.NotNull(getResult);

        getResult.ShouldHaveValidationError()
            .WithError("Id must not be empty.")
            .WithDetail("The identifier of the workspace cannot be empty.");
    }

    [Fact]
    public async Task GetWorkspace_WhenWorkspaceDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var getResponse = await _client.GetAsync($"/api/v1/workspaces/{id}");
        var getResult = await getResponse.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        Assert.NotNull(getResult);

        getResult.ShouldHaveValidationError()
            .WithError("Workspace not found.")
            .WithDetail("The workspace with the provided identifier was not found.");
    }
}
