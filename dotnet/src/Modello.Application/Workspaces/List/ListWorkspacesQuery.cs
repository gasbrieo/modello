using Modello.Application.Common.Messaging;
using Modello.Application.Common.Pagination;
using Modello.Application.Common.Results;

namespace Modello.Application.Workspaces.List;

public sealed record ListWorkspacesQuery(int PageNumber, int PageSize) : IQuery<Result<PagedList<WorkspaceDto>>>;
