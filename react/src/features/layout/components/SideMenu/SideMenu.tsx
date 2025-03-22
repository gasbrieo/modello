import type { FC } from "react";
import HomeIcon from "@mui/icons-material/Home";
import NotificationsIcon from "@mui/icons-material/Notifications";
import SearchIcon from "@mui/icons-material/Search";
import WorkspacesIcon from "@mui/icons-material/Workspaces";
import Drawer from "@mui/material/Drawer";
import List from "@mui/material/List";
import type { CSSObject, Theme } from "@mui/material/styles";

import SideMenuAccount from "./SideMenuAccount";
import SideMenuItem from "./SideMenuItem";
import SideMenuLink from "./SideMenuLink";

const closedMixin = (theme: Theme): CSSObject => ({
  transition: theme.transitions.create("width", {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  overflowX: "hidden",
  width: `calc(${theme.spacing(6)} + 2px)`,
  backgroundColor: theme.palette.grey[50],
  borderColor: theme.palette.grey[200],
});

const SideMenu: FC = () => {
  return (
    <Drawer
      open
      anchor="left"
      variant="permanent"
      sx={(theme) => ({
        flexShrink: 0,
        whiteSpace: "nowrap",
        boxSizing: "border-box",
        display: "flex",
        flexDirection: "column",
        height: "100vh",
        ...closedMixin(theme),
        "& .MuiDrawer-paper": {
          ...closedMixin(theme),
        },
      })}
    >
      <List sx={{ flexGrow: 1 }}>
        <SideMenuAccount />

        <SideMenuLink
          icon={<HomeIcon />}
          label="Início"
          to="/"
        />

        <SideMenuLink
          icon={<WorkspacesIcon />}
          label="Projetos"
          to="/workspaces"
        />

        <SideMenuItem
          icon={<SearchIcon />}
          label="Buscar"
          onClick={() => console.log("do search")}
        />
      </List>
      <List>
        <SideMenuItem
          icon={<NotificationsIcon />}
          label="Notificações"
          onClick={() => console.log("do notifications")}
        />
      </List>
    </Drawer>
  );
};

export default SideMenu;
