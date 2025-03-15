using Modello.Application.Workspaces;
using Modello.FunctionalTests.TestHelpers;
using Modello.Presentation.Requests.V1;

namespace Modello.FunctionalTests.Features.Workspaces;

public class DeleteWorkspaceTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GivenExistingWorkspace_WhenDeleteWorkspaceCalled_ThenReturnsNoContent()
    {
        // Given
        var createRequest = new CreateWorkspaceRequest() { Name = "Work" };
        var createResponse = await _client.PostAsJsonAsync("/api/v1/workspaces", createRequest);
        var createResult = await createResponse.Content.ReadFromJsonAsync<WorkspaceDto>();
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        Assert.NotNull(createResult);

        // When
        var deleteResponse = await _client.DeleteAsync($"/api/v1/workspaces/{createResult.Id}");

        // Then
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getResponse = await _client.GetAsync($"/api/v1/workspaces/{createResult.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task GivenNonExistingWorkspace_WhenDeleteWorkspaceCalled_ThenReturnsNoContent()
    {
        // Given
        var id = Guid.NewGuid();

        // When
        var deleteResponse = await _client.DeleteAsync($"/api/v1/workspaces/{id}");

        // Then
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task GivenEmptyId_WhenDeleteWorkspaceCalled_ThenReturnsBadRequest()
    {
        // Given
        var id = Guid.Empty;

        // When
        var deleteResponse = await _client.DeleteAsync($"/api/v1/workspaces/{id}");
        var deleteResult = await deleteResponse.Content.ReadFromJsonAsync<ErrorListResponse>();

        // Then
        Assert.Equal(HttpStatusCode.BadRequest, deleteResponse.StatusCode);
        Assert.NotNull(deleteResult);

        deleteResult.ShouldHaveValidationError()
            .WithType("ValidationError")
            .WithError("Id must not be empty.")
            .WithDetail("The identifier of the workspace cannot be empty.");
    }
}
