import { Outlet } from "@tanstack/react-router";

import "./WorkspacesLayout.scss";

const WorkspacesLayout = () => {
  return (
    <main className="workspaces-layout">
      <Outlet />
    </main>
  );
};

export default WorkspacesLayout;
