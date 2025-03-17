import classNames from "classnames";
import { BellIcon, ChevronDownIcon, ChevronRightIcon, HashIcon, HouseIcon, PlusIcon, SearchIcon } from "lucide-react";
import { Link, Outlet } from "@tanstack/react-router";

import Avatar from "@/components/Avatar";
import Typography from "@/components/Typography";

import { useSidebarStore } from "@/stores/sidebarStore";

import "./Layout.scss";
import IconButton from "@/components/IconButton";
import List from "@/components/List";
import ListItemButton from "@/components/ListItemButton";
import ListItem from "@/components/ListItem";
import ListItemIcon from "@/components/ListItemIcon";
import ListItemText from "@/components/ListItemText";
import { useState } from "react";
import ListItemSecondaryAction from "@/components/ListItemSecondaryAction";

const Account = () => {
  return (
    <div className="layout__sidebar__account">
      <button className="layout__sidebar__account__info">
        <Avatar>
          <img
            alt="Gabriel"
            src="https://api.dicebear.com/7.x/initials/svg?seed=G"
          />
        </Avatar>
        <Typography variant="body2">Gabriel</Typography>
        <ChevronDownIcon
          width="16px"
          height="16px"
        />
      </button>
      <div>
        <IconButton>
          <BellIcon
            width="16px"
            height="16px"
          />
        </IconButton>
      </div>
    </div>
  );
};

const FixedMenu = () => {
  return (
    <List className="layout__sidebar__fixed-menu">
      <ListItem title="Home">
        <ListItemButton
          as={Link}
          to="/"
        >
          <ListItemIcon>
            <HouseIcon
              width="16px"
              height="16px"
            />
          </ListItemIcon>
          <ListItemText primary="Home" />
        </ListItemButton>
      </ListItem>
      <ListItem title="Buscar">
        <ListItemButton
          as={Link}
          to="/"
        >
          <ListItemIcon>
            <SearchIcon
              width="16px"
              height="16px"
            />
          </ListItemIcon>
          <ListItemText primary="Buscar" />
        </ListItemButton>
      </ListItem>
    </List>
  );
};

const MyWorkspaces = () => {
  const [isOpened, setIsOpened] = useState(true);

  return (
    <List className="layout__sidebar__my-workspaces">
      <ListItem>
        <ListItemButton
          as={Link}
          to="/workspaces"
        >
          <ListItemText primary="My Workspaces" />
        </ListItemButton>
        <ListItemSecondaryAction>
          <IconButton
            onClick={() => {}}
            className="layout__sidebar__my-workspaces__toggle-button"
          >
            <PlusIcon
              width="16px"
              height="16px"
            />
          </IconButton>
          <IconButton
            onClick={() => setIsOpened((prev) => !prev)}
            className="layout__sidebar__my-workspaces__toggle-button"
          >
            <ChevronDownIcon
              width="16px"
              height="16px"
            />
          </IconButton>
        </ListItemSecondaryAction>
      </ListItem>
      {isOpened && (
        <div>
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
        </div>
      )}
    </List>
  );
};

const Layout = () => {
  const isSidebarOpened = useSidebarStore((state) => state.isOpened);

  return (
    <div className="layout">
      <aside
        className={classNames("layout__sidebar", {
          "layout__sidebar--opened": isSidebarOpened,
          "layout__sidebar--closed": !isSidebarOpened,
        })}
      >
        <Account />
        <FixedMenu />
        <MyWorkspaces />
      </aside>
      <main className="layout__content">
        <Outlet />
      </main>
    </div>
  );
};

export default Layout;
