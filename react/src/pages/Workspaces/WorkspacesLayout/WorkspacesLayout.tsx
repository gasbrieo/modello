import { Outlet } from "@tanstack/react-router";

import "./WorkspacesLayout.scss";

const WorkspacesLayout = () => {
  return (
    <div className="workspaces-layout">
      <main className="workspaces-layout__content">
        <Outlet />
      </main>
    </div>
  );
};

export default WorkspacesLayout;
