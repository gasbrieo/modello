using Modello.Application.Workspaces;
using Modello.FunctionalTests.TestHelpers;
using Modello.Presentation.Requests.V1;

namespace Modello.FunctionalTests.Features.Workspaces;

public class GetWorkspaceTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GivenExistingWorkspace_WhenGetWorkspaceCalled_ThenReturnsOk()
    {
        // Given
        var createRequest = new CreateWorkspaceRequest() { Name = "Work" };
        var createResponse = await _client.PostAsJsonAsync("/api/v1/workspaces", createRequest);
        var createResult = await createResponse.Content.ReadFromJsonAsync<WorkspaceDto>();
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        Assert.NotNull(createResult);

        // When
        var getResponse = await _client.GetAsync($"/api/v1/workspaces/{createResult.Id}");
        var getResult = await getResponse.Content.ReadFromJsonAsync<WorkspaceDto>();

        // Then
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.NotNull(getResult);
        Assert.Equal(createResult.Id, getResult.Id);
        Assert.Equal(createResult.Name, getResult.Name);
    }

    [Fact]
    public async Task GivenEmptyId_WhenGetWorkspaceCalled_ThenReturnsBadRequest()
    {
        // Given
        var id = Guid.Empty;

        // When
        var getResponse = await _client.GetAsync($"/api/v1/workspaces/{id}");
        var getResult = await getResponse.Content.ReadFromJsonAsync<ErrorListResponse>();

        // Then
        Assert.Equal(HttpStatusCode.BadRequest, getResponse.StatusCode);
        Assert.NotNull(getResult);

        getResult.ShouldHaveValidationError()
            .WithType("ValidationError")
            .WithError("Id must not be empty.")
            .WithDetail("The identifier of the workspace cannot be empty.");
    }

    [Fact]
    public async Task GivenNonExistingWorkspace_WhenGetWorkspaceCalled_ThenReturnsNotFound()
    {
        // Given
        var id = Guid.NewGuid();

        // When
        var getResponse = await _client.GetAsync($"/api/v1/workspaces/{id}");
        var getResult = await getResponse.Content.ReadFromJsonAsync<ErrorResponse>();

        // Then
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        Assert.NotNull(getResult);

        Assert.Equal("NotFound", getResult.Type);
        Assert.Equal("Not Found", getResult.Error);
        Assert.Equal("The requested resource was not found.", getResult.Detail);
    }
}
