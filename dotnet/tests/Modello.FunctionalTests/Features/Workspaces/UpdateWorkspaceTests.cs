using System.Net.Http.Json;
using Modello.Application.Workspaces;
using Modello.FunctionalTests.TestHelpers;
using Modello.Presentation.Requests.V1;
using Modello.Presentation.Responses;

namespace Modello.FunctionalTests.Features.Workspaces;

public class UpdateWorkspaceTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task UpdateWorkspace_ShouldReturnOk()
    {
        // Arrange
        var createRequest = new CreateWorkspaceRequest() { Name = "Work" };
        var createResponse = await _client.PostAsJsonAsync("/api/v1/workspaces", createRequest);
        var createResult = await createResponse.Content.ReadFromJsonAsync<WorkspaceDto>();
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        Assert.NotNull(createResult);

        var updateRequest = new UpdateWorkspaceRequest() { Name = "Study" };

        // Act 
        var updateResponse = await _client.PutAsJsonAsync($"/api/v1/workspaces/{createResult.Id}", updateRequest);
        var updateResult = await updateResponse.Content.ReadFromJsonAsync<WorkspaceDto>();

        // Assert
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
    public async Task UpdateWorkspace_WhenIdIsEmpty_ShouldReturnBadRequest()
    {
        // Arrange
        var id = Guid.Empty;
        var updateRequest = new UpdateWorkspaceRequest() { Name = "Work" };

        // Act
        var updateResponse = await _client.PutAsJsonAsync($"/api/v1/workspaces/{id}", updateRequest);
        var result = await updateResponse.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, updateResponse.StatusCode);
        Assert.NotNull(result);

        result.ShouldHaveValidationError()
            .WithError("Id must not be empty.");
    }

    [Fact]
    public async Task UpdateWorkspace_WhenNameIsEmpty_ShouldReturnBadRequest()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updateRequest = new UpdateWorkspaceRequest() { Name = string.Empty };

        // Act
        var updateResponse = await _client.PutAsJsonAsync($"/api/v1/workspaces/{id}", updateRequest);
        var updateResult = await updateResponse.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, updateResponse.StatusCode);
        Assert.NotNull(updateResult);

        updateResult.ShouldHaveValidationError()
            .WithError("Name must not be empty.");
    }

    [Fact]
    public async Task UpdateWorkspace_WhenWorkspaceDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updateRequest = new UpdateWorkspaceRequest() { Name = "Work" };

        // Act
        var updateResponse = await _client.PutAsJsonAsync($"/api/v1/workspaces/{id}", updateRequest);
        var updateResult = await updateResponse.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
        Assert.NotNull(updateResult);
    }
}
