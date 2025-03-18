import { createFileRoute } from "@tanstack/react-router";

import ListWorkspaces from "@/pages/Secure/Workspaces/ListWorkspaces";

export const Route = createFileRoute("/workspaces/")({
  component: ListWorkspaces,
});
