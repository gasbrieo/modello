import classNames from "classnames";
import { HTMLAttributes } from "react";

import "./Sidebar.scss";

interface SidebarProps extends HTMLAttributes<HTMLElement> {
  isCollapsed?: boolean;
}

const Sidebar = ({ children, className, isCollapsed }: SidebarProps) => {
  return (
    <aside
      className={classNames(
        "sidebar",
        {
          "sidebar--collapsed": !isCollapsed,
        },
        className
      )}
    >
      {children}
    </aside>
  );
};

export default Sidebar;
