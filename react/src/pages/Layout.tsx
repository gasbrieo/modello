import { FolderIcon, LayoutDashboardIcon, SearchIcon } from "lucide-react";
import { Link, Outlet } from "@tanstack/react-router";

import Avatar from "@/components/Avatar";
import List from "@/components/List";
import ListItem from "@/components/ListItem";
import ListItemAvatar from "@/components/ListItemAvatar";
import ListItemButton from "@/components/ListItemButton";
import ListItemIcon from "@/components/ListItemIcon";

import "./Layout.scss";

const Layout = () => {
  return (
    <div className="layout">
      <aside className="layout__sidebar">
        <List className="layout__sidebar-account">
          <ListItem className="layout__sidebar-item">
            <ListItemButton>
              <ListItemAvatar>
                <Avatar>
                  <img
                    alt="Gabriel"
                    src="https://api.dicebear.com/7.x/initials/svg?seed=G"
                  />
                </Avatar>
              </ListItemAvatar>
            </ListItemButton>
          </ListItem>
        </List>
        <List>
          <ListItem title="Dashboard">
            <ListItemButton
              as={Link}
              to="/"
            >
              <ListItemIcon>
                <LayoutDashboardIcon
                  width="1.5em"
                  height="1.5em"
                />
              </ListItemIcon>
            </ListItemButton>
          </ListItem>
          <ListItem title="Workspaces">
            <ListItemButton
              as={Link}
              to="/workspaces"
            >
              <ListItemIcon>
                <FolderIcon
                  width="1.5em"
                  height="1.5em"
                />
              </ListItemIcon>
            </ListItemButton>
          </ListItem>
          <ListItem title="Buscar">
            <ListItemButton>
              <ListItemIcon>
                <SearchIcon
                  width="1.5em"
                  height="1.5em"
                />
              </ListItemIcon>
            </ListItemButton>
          </ListItem>
        </List>
      </aside>
      <main className="layout__content">
        <Outlet />
      </main>
    </div>
  );
};

export default Layout;
