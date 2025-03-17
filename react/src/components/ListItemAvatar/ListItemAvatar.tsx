import classNames from "classnames";
import type { HTMLAttributes } from "react";

import "./ListItemAvatar.scss";

const ListItemAvatar = ({ className, children, ...rest }: HTMLAttributes<HTMLDivElement>) => {
  return (
    <div
      className={classNames("list-item-avatar", className)}
      {...rest}
    >
      {children}
    </div>
  );
};

export default ListItemAvatar;
