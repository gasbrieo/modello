import classNames from "classnames";
import type { HTMLAttributes } from "react";

import "./ListItemIcon.scss";

const ListItemIcon = ({ children, className, ...rest }: HTMLAttributes<HTMLDivElement>) => {
  return (
    <div
      className={classNames("list-item-icon", className)}
      {...rest}
    >
      {children}
    </div>
  );
};

export default ListItemIcon;
