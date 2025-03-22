import { createFileRoute } from "@tanstack/react-router";

import { ListWorkspaces } from "@/features/workspaces";

export const Route = createFileRoute("/workspaces/")({
  component: ListWorkspaces,
});
