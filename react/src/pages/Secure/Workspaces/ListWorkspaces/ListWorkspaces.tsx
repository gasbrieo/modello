import { EllipsisIcon, ListCollapseIcon } from "lucide-react";

import IconButton from "@/components/IconButton";
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
              color="rgba(0 0 0 / 40%)"
            />
          </IconButton>
        </div>
        <div className="list-workspaces__header__right">
          <IconButton>
            <EllipsisIcon
              height="1rem"
              width="1rem"
              color="rgba(0 0 0 / 40%)"
            />
          </IconButton>
        </div>
      </header>
      <div className="list-workspaces__content">
        <section className="list-workspaces__content__hero">
          <Typography variant="h4">My workspaces</Typography>
        </section>
      </div>
    </div>
  );
};

export default ListWorkspaces;
