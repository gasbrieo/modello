namespace Modello.Presentation.Requests.V1;

public class ListWorkspacesRequest
{
    [FromQuery]
    public int PageNumber { get; set; } = 1;

    [FromQuery]
    public int PageSize { get; set; } = 10;
}
