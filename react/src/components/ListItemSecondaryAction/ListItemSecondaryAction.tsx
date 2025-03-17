import classNames from "classnames";
import type { HTMLAttributes } from "react";

import "./ListItemSecondaryAction.scss";

const ListItemSecondaryAction = ({ className, children, ...rest }: HTMLAttributes<HTMLDivElement>) => {
  return (
    <div
      className={classNames("list-item-secondary-action", className)}
      {...rest}
    >
      {children}
    </div>
  );
};

export default ListItemSecondaryAction;
