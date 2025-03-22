import type { FC } from "react";

import Box from "@mui/material/Box";
import { Outlet } from "@tanstack/react-router";

import { SideMenu } from "./SideMenu";

const Layout: FC = () => {
  return (
    <Box sx={{ display: "flex" }}>
      <SideMenu />
      <Box
        component="main"
        sx={{ flexGrow: 1, p: 1.5 }}
      >
        <Outlet />
      </Box>
    </Box>
  );
};

export default Layout;
