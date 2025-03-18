import { EllipsisIcon, HashIcon, ListCollapseIcon } from "lucide-react";
import { Link } from "@tanstack/react-router";

import Divider from "@/components/Divider";
import IconButton from "@/components/IconButton";
import List from "@/components/List";
import ListItem from "@/components/ListItem";
import ListItemButton from "@/components/ListItemButton";
import ListItemIcon from "@/components/ListItemIcon";
import ListItemText from "@/components/ListItemText";
import Typography from "@/components/Typography";

import { useSidebarStore } from "@/stores/sidebarStore";

import "./ListWorkspaces.scss";

const ListWorkspaces = () => {
  const toggleSidebar = useSidebarStore((state) => state.toggle);

  return (
    <div className="list-workspaces">
      <header className="list-workspaces__header">
        <div className="list-workspaces__header__left">
          <IconButton onClick={toggleSidebar}>
            <ListCollapseIcon
              height="1rem"
              width="1rem"
            />
          </IconButton>
        </div>
        <div className="list-workspaces__header__right">
          <IconButton>
            <EllipsisIcon
              height="1rem"
              width="1rem"
            />
          </IconButton>
        </div>
      </header>
      <div className="list-workspaces__content">
        <section className="list-workspaces__content__hero">
          <Typography variant="h4">My workspaces</Typography>
          <Typography variant="subtitle1">My workspaces description</Typography>
        </section>
        <section className="list-workspaces__content__list">
          <List>
            <Divider />
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
        </section>
      </div>
    </div>
  );
};

export default ListWorkspaces;
