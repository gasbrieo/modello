import { createFileRoute } from "@tanstack/react-router";

import { LayoutWorkspaces } from "@/features/workspaces";

export const Route = createFileRoute("/workspaces")({
  component: LayoutWorkspaces,
});
