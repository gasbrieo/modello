using Modello.Application.Workspaces;
using Modello.FunctionalTests.TestHelpers;
using Modello.Presentation.Requests.V1;

namespace Modello.FunctionalTests.Features.Workspaces;

public class CreateWorkspaceTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GivenRequest_WhenCreateWorkspaceCalled_ThenReturnsCreated()
    {
        // Given
        var createRequest = new CreateWorkspaceRequest() { Name = "Work" };

        // When
        var createResponse = await _client.PostAsJsonAsync("/api/v1/workspaces", createRequest);
        var createResult = await createResponse.Content.ReadFromJsonAsync<WorkspaceDto>();

        // Then
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
    public async Task GivenRequestWithEmptyId_WhenCreateWorkspaceCalled_ThenReturnsBadRequest()
    {
        // Given
        var createRequest = new CreateWorkspaceRequest();

        // When
        var createResponse = await _client.PostAsJsonAsync("/api/v1/workspaces", createRequest);
        var createResult = await createResponse.Content.ReadFromJsonAsync<ErrorListResponse>();

        // Then
        Assert.Equal(HttpStatusCode.BadRequest, createResponse.StatusCode);
        Assert.NotNull(createResult);

        createResult.ShouldHaveValidationError()
            .WithType("ValidationError")
            .WithError("Name must not be empty.")
            .WithDetail("The name of the workspace cannot be empty or contain only white spaces.");
    }
}
