import { EllipsisIcon, ListCollapseIcon } from "lucide-react";
import { useParams } from "@tanstack/react-router";

import IconButton from "@/components/IconButton";
import Typography from "@/components/Typography";

import { useSidebarStore } from "@/stores/sidebarStore";

import "./ViewWorkspace.scss";

const ViewWorkspace = () => {
  const { workspaceId } = useParams({ strict: false });
  const toggleSidebar = useSidebarStore((state) => state.toggle);

  return (
    <div className="view-workspaces">
      <header className="view-workspaces__header">
        <div className="view-workspaces__header__left">
          <IconButton onClick={toggleSidebar}>
            <ListCollapseIcon
              height="1rem"
              width="1rem"
            />
          </IconButton>
        </div>
        <div className="view-workspaces__header__right">
          <IconButton>
            <EllipsisIcon
              height="1rem"
              width="1rem"
            />
          </IconButton>
        </div>
      </header>
      <div className="view-workspaces__content">
        <section className="view-workspaces__content__hero">
          <Typography variant="h4">Workspace {workspaceId}</Typography>
        </section>
      </div>
    </div>
  );
};

export default ViewWorkspace;
