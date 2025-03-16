import { Link, Outlet, createRootRoute } from "@tanstack/react-router";

export const Route = createRootRoute({
  component: () => (
    <>
      <div>
        <Link to="/">Home</Link>
        <Link to="/workspaces">Workspaces</Link>
      </div>
      <hr />
      <Outlet />
    </>
  ),
});
