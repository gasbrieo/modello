using Modello.Application.Workspaces;
using Modello.FunctionalTests.TestHelpers;
using Modello.Presentation.Requests.V1;

namespace Modello.FunctionalTests.Features.Workspaces;

public class ListWorkspaceTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GivenExistingWorkspaces_WhenListWorkspaceCalled_ThenReturnsOk()
    {
        // Given
        await _client.PostAsJsonAsync("/api/v1/workspaces", new CreateWorkspaceRequest() { Name = "Work" });
        await _client.PostAsJsonAsync("/api/v1/workspaces", new CreateWorkspaceRequest() { Name = "Study" });
        await _client.PostAsJsonAsync("/api/v1/workspaces", new CreateWorkspaceRequest() { Name = "Movies" });
        await _client.PostAsJsonAsync("/api/v1/workspaces", new CreateWorkspaceRequest() { Name = "Books" });
        await _client.PostAsJsonAsync("/api/v1/workspaces", new CreateWorkspaceRequest() { Name = "Shopping Items" });

        var listRequest = new ListWorkspacesRequest { PageNumber = 1, PageSize = 5 };

        // When
        var listResponse = await _client.GetAsync($"/api/v1/workspaces?pageNumber={listRequest.PageNumber}&pageSize={listRequest.PageSize}");
        var listResult = await listResponse.Content.ReadFromJsonAsync<StaticPagedList<WorkspaceDto>>();

        // Then
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);
        Assert.NotNull(listResult);
        Assert.Equal(5, listResult.Items.Count());
    }

    [Fact]
    public async Task GivenRequestWithPageNumberBelowMinimum_WhenListWorkspaceCalled_ThenReturnsBadRequest()
    {
        // Given
        var listRequest = new ListWorkspacesRequest { PageNumber = 0, PageSize = 1 };

        // When
        var listResponse = await _client.GetAsync($"/api/v1/workspaces?pageNumber={listRequest.PageNumber}&pageSize={listRequest.PageSize}");
        var listResult = await listResponse.Content.ReadFromJsonAsync<ErrorListResponse>();

        // Then
        Assert.Equal(HttpStatusCode.BadRequest, listResponse.StatusCode);
        Assert.NotNull(listResult);

        listResult.ShouldHaveValidationError()
            .WithType("ValidationError")
            .WithError("PageNumber must be greater than zero.")
            .WithDetail("The page number must be greater than zero.");
    }

    [Fact]
    public async Task GivenRequestWithPageSizeBelowMinimum_WhenListWorkspaceCalled_ThenReturnsBadRequest()
    {
        // Given
        var listRequest = new ListWorkspacesRequest { PageNumber = 1, PageSize = 0 };

        // When
        var listResponse = await _client.GetAsync($"/api/v1/workspaces?pageNumber={listRequest.PageNumber}&pageSize={listRequest.PageSize}");
        var listResult = await listResponse.Content.ReadFromJsonAsync<ErrorListResponse>();

        // Then
        Assert.Equal(HttpStatusCode.BadRequest, listResponse.StatusCode);
        Assert.NotNull(listResult);

        listResult.ShouldHaveValidationError()
            .WithType("ValidationError")
            .WithError("PageSize must be greater than zero.")
            .WithDetail("The page size must be greater than zero.");
    }
}