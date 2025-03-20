import { FC } from "react";

import { Outlet } from "@tanstack/react-router";

const Layout: FC = () => {
  return <Outlet />;
};

export default Layout;
