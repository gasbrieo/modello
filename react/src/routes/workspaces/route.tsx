import { createFileRoute } from "@tanstack/react-router";

import WorkspacesLayout from "@/pages/Workspaces/WorkspacesLayout";

export const Route = createFileRoute("/workspaces")({
  component: WorkspacesLayout,
});
