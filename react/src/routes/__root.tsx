import { createRootRoute } from "@tanstack/react-router";

import AppLayout from "@/pages/Secure/AppLayout";

export const Route = createRootRoute({
  component: AppLayout,
});
