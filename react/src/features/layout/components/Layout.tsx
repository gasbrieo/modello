import { FC } from "react";
import { Outlet } from "@tanstack/react-router";

const Layout: FC = () => {
  return (
    <div>
      <aside>sidemenu</aside>
      <div>
        <Outlet />
      </div>
    </div>
  );
};

export default Layout;
