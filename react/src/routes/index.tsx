import { createFileRoute } from "@tanstack/react-router";

import Home from "@/pages/Secure/Home";

export const Route = createFileRoute("/")({
  component: Home,
});
