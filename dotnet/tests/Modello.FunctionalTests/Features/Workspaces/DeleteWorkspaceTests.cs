using System.Net.Http.Json;
using Modello.Application.Workspaces;
using Modello.FunctionalTests.TestHelpers;
using Modello.Presentation.Requests.V1;
using Modello.Presentation.Responses;

namespace Modello.FunctionalTests.Features.Workspaces;

public class DeleteWorkspaceTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task DeleteWorkspace_ShouldReturnNoContent()
    {
        // Arrange
        var createRequest = new CreateWorkspaceRequest() { Name = "Work" };
        var createResponse = await _client.PostAsJsonAsync("/api/v1/workspaces", createRequest);
        var createResult = await createResponse.Content.ReadFromJsonAsync<WorkspaceDto>();
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        Assert.NotNull(createResult);

        // Act 
        var deleteResponse = await _client.DeleteAsync($"/api/v1/workspaces/{createResult.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getResponse = await _client.GetAsync($"/api/v1/workspaces/{createResult.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteWorkspace_WhenIdIsEmpty_ShouldReturnBadRequest()
    {
        // Arrange
        var id = Guid.Empty;

        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/v1/workspaces/{id}");
        var deleteResult = await deleteResponse.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, deleteResponse.StatusCode);
        Assert.NotNull(deleteResult);

        deleteResult.ShouldHaveValidationError()
            .WithError("Id must not be empty.");
    }

    [Fact]
    public async Task DeleteWorkspace_WhenWorkspaceDoesNotExist_ShouldReturnNoContent()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/v1/workspaces/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }
}
