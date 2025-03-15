using Modello.Application.Workspaces;
using Modello.FunctionalTests.TestHelpers;
using Modello.Presentation.Requests.V1;

namespace Modello.FunctionalTests.Features.Workspaces;

public class UpdateWorkspaceTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GivenExistingWorkspace_WhenUpdateWorkspaceCalled_ThenReturnsOk()
    {
        // Given
        var createRequest = new CreateWorkspaceRequest() { Name = "Work" };
        var createResponse = await _client.PostAsJsonAsync("/api/v1/workspaces", createRequest);
        var createResult = await createResponse.Content.ReadFromJsonAsync<WorkspaceDto>();
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        Assert.NotNull(createResult);

        var updateRequest = new UpdateWorkspaceRequest() { Name = "Study" };

        // When 
        var updateResponse = await _client.PutAsJsonAsync($"/api/v1/workspaces/{createResult.Id}", updateRequest);
        var updateResult = await updateResponse.Content.ReadFromJsonAsync<WorkspaceDto>();

        // Then
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        Assert.NotNull(updateResult);
        Assert.Equal(createResult.Id, updateResult.Id);
        Assert.Equal(updateRequest.Name, updateResult.Name);

        var getResponse = await _client.GetAsync($"/api/v1/workspaces/{updateResult.Id}");
        var getResult = await getResponse.Content.ReadFromJsonAsync<WorkspaceDto>();
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.NotNull(getResult);
        Assert.Equal(updateResult.Id, getResult.Id);
        Assert.Equal(updateResult.Name, getResult.Name);
    }

    [Fact]
    public async Task GivenEmptyId_WhenUpdateWorkspaceCalled_ThenReturnsBadRequest()
    {
        // Given
        var id = Guid.Empty;
        var updateRequest = new UpdateWorkspaceRequest() { Name = "Work" };

        // When
        var updateResponse = await _client.PutAsJsonAsync($"/api/v1/workspaces/{id}", updateRequest);
        var updateResult = await updateResponse.Content.ReadFromJsonAsync<ErrorListResponse>();

        // Then
        Assert.Equal(HttpStatusCode.BadRequest, updateResponse.StatusCode);
        Assert.NotNull(updateResult);

        updateResult.ShouldHaveValidationError()
            .WithType("ValidationError")
            .WithError("Id must not be empty.")
            .WithDetail("The identifier of the workspace cannot be empty.");
    }

    [Fact]
    public async Task GivenRequestWithEmptyName_WhenUpdateWorkspaceCalled_ThenReturnsBadRequest()
    {
        // Given
        var id = Guid.NewGuid();
        var updateRequest = new UpdateWorkspaceRequest() { Name = string.Empty };

        // When
        var updateResponse = await _client.PutAsJsonAsync($"/api/v1/workspaces/{id}", updateRequest);
        var updateResult = await updateResponse.Content.ReadFromJsonAsync<ErrorListResponse>();

        // Then
        Assert.Equal(HttpStatusCode.BadRequest, updateResponse.StatusCode);
        Assert.NotNull(updateResult);

        updateResult.ShouldHaveValidationError()
            .WithType("ValidationError")
            .WithError("Name must not be empty.")
            .WithDetail("The name of the workspace cannot be empty or contain only white spaces.");
    }

    [Fact]
    public async Task GivenNonExistingWorkspace_WhenUpdateWorkspaceCalled_ThenReturnsNotFound()
    {
        // Given
        var id = Guid.NewGuid();
        var updateRequest = new UpdateWorkspaceRequest() { Name = "Work" };

        // When
        var updateResponse = await _client.PutAsJsonAsync($"/api/v1/workspaces/{id}", updateRequest);
        var updateResult = await updateResponse.Content.ReadFromJsonAsync<ErrorResponse>();

        // Then
        Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
        Assert.NotNull(updateResult);

        Assert.Equal("NotFound", updateResult.Type);
        Assert.Equal("Not Found", updateResult.Error);
        Assert.Equal("The requested resource was not found.", updateResult.Detail);
    }
}
