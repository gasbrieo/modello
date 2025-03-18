import { ChevronDownIcon, HashIcon, HouseIcon, PlusIcon, SearchIcon } from "lucide-react";
import { useState } from "react";
import { Link, Outlet } from "@tanstack/react-router";

import Collapse from "@/components/Collapse";
import List from "@/components/List";
import ListItemButton from "@/components/ListItemButton";
import ListItem from "@/components/ListItem";
import ListItemIcon from "@/components/ListItemIcon";
import ListItemSecondaryAction from "@/components/ListItemSecondaryAction";
import ListItemText from "@/components/ListItemText";
import Sidebar from "@/components/Sidebar/Sidebar";
import SidebarItemAccount from "@/components/SidebarItemAccount";
import SidebarItemNotifications from "@/components/SidebarItemNotifications";

import { useSidebarStore } from "@/stores/sidebarStore";

import "./AppLayout.scss";
import classNames from "classnames";

const AccountSection = () => {
  return (
    <div className="app-sidebar__account-section">
      <SidebarItemAccount
        name="Gabriel"
        avatar="https://api.dicebear.com/7.x/initials/svg?seed=G"
      />
      <SidebarItemNotifications />
    </div>
  );
};

const NavSection = () => {
  return (
    <List className="app-sidebar__nav-section">
      <ListItem>
        <ListItemButton
          as={Link}
          to="/"
          activeProps={{ className: "app-sidebar__item--active" }}
        >
          <ListItemIcon>
            <HouseIcon
              width="1em"
              height="1em"
            />
          </ListItemIcon>
          <ListItemText primary="Home" />
        </ListItemButton>
      </ListItem>
      <ListItem>
        <ListItemButton>
          <ListItemIcon>
            <SearchIcon
              width="1em"
              height="1em"
            />
          </ListItemIcon>
          <ListItemText primary="Buscar" />
        </ListItemButton>
      </ListItem>
    </List>
  );
};

const WorkspacesSection = () => {
  const [isOpened, setIsOpened] = useState(true);

  return (
    <List className="app-sidebar__workspaces-section">
      <ListItem>
        <ListItemButton
          as={Link}
          to="/workspaces"
          activeOptions={{ exact: true }}
          activeProps={{ className: "app-sidebar__item--active" }}
        >
          <ListItemText primary="My Workspaces" />
        </ListItemButton>
        <ListItemSecondaryAction>
          <ListItemButton>
            <ListItemIcon>
              <PlusIcon
                width="1em"
                height="1em"
              />
            </ListItemIcon>
          </ListItemButton>
          <ListItemButton onClick={() => setIsOpened((prev) => !prev)}>
            <ListItemIcon>
              <ChevronDownIcon
                width="1em"
                height="1em"
                className={classNames("app-sidebar__workspaces-section__toggle-icon", {
                  "app-sidebar__workspaces-section__toggle-icon--collapsed": !isOpened,
                })}
              />
            </ListItemIcon>
          </ListItemButton>
        </ListItemSecondaryAction>
      </ListItem>
      <Collapse isOpened={isOpened}>
        <List>
          {[1, 2, 3, 4].map((id) => {
            return (
              <ListItem key={id}>
                <ListItemButton
                  as={Link}
                  to="/workspaces/$workspaceId"
                  params={{
                    workspaceId: id,
                  }}
                  activeProps={{ className: "app-sidebar__item--active" }}
                >
                  <ListItemIcon>
                    <HashIcon
                      width="1em"
                      height="1em"
                    />
                  </ListItemIcon>
                  <ListItemText primary={`Workspace ${id}`} />
                </ListItemButton>
              </ListItem>
            );
          })}
        </List>
      </Collapse>
    </List>
  );
};

const AppSidebar = () => {
  const isOpened = useSidebarStore((state) => state.isOpened);

  return (
    <Sidebar
      className="app-sidebar"
      isCollapsed={!isOpened}
    >
      <AccountSection />
      <NavSection />
      <WorkspacesSection />
    </Sidebar>
  );
};

const AppLayout = () => {
  return (
    <div className="app-layout">
      <AppSidebar />
      <main className="app-layout__content">
        <Outlet />
      </main>
    </div>
  );
};

export default AppLayout;
