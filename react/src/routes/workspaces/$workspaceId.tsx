import { createFileRoute } from "@tanstack/react-router";

import ViewWorkspace from "@/pages/Secure/Workspaces/ViewWorkspace";

export const Route = createFileRoute("/workspaces/$workspaceId")({
  component: ViewWorkspace,
});
