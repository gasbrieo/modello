using Modello.Application.Common.Messaging;
using Modello.Application.Common.Pagination;

namespace Modello.Application.Workspaces.List;

public record ListWorkspacesQuery(int PageNumber, int PageSize) : IQuery<PagedList<WorkspaceDto>>;
