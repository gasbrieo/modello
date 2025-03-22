import type { FC } from "react";
import Typography from "@mui/material/Typography";
import Box from "@mui/material/Box";
import { Outlet } from "@tanstack/react-router";

const LayoutWorkspaces: FC = () => {
  return (
    <Box>
      <Typography variant="h6">LayoutWorkspaces</Typography>
      <main>
        <Outlet />
      </main>
    </Box>
  );
};

export default LayoutWorkspaces;
