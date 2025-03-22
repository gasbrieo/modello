import { createFileRoute } from "@tanstack/react-router";

import { ViewWorkspace } from "@/features/workspaces";

export const Route = createFileRoute("/workspaces/$workspaceId")({
  component: ViewWorkspace,
});
