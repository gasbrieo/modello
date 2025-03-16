import { HashIcon } from "lucide-react";

import List from "@/components/List";
import ListItem from "@/components/ListItem";
import ListItemButton from "@/components/ListItemButton";
import ListItemIcon from "@/components/ListItemIcon";
import ListItemText from "@/components/ListItemText";
import Typography from "@/components/Typography";
import ListSubheader from "@/components/ListSubheader";

import "./WorkspacesIndex.scss";
import Divider from "@/components/Divider";
import { Link } from "@tanstack/react-router";

const WorkspacesIndex = () => {
  return (
    <section className="workspaces-index">
      <Typography variant="h6">Meus workspaces</Typography>
      <List>
        <ListSubheader>4 projetos</ListSubheader>
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
                className="workspaces-index__list-item-button"
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
  );
};

export default WorkspacesIndex;
