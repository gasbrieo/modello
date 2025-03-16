import { createFileRoute } from "@tanstack/react-router";

import Workspace from "@/pages/Workspaces/Workspace";

export const Route = createFileRoute("/workspaces/$workspaceId")({
  component: Workspace,
});
