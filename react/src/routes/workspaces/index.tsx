import { createFileRoute } from "@tanstack/react-router";

import WorkspacesIndex from "@/pages/Workspaces/WorkspacesIndex";

export const Route = createFileRoute("/workspaces/")({
  component: WorkspacesIndex,
});
