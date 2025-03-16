import classNames from "classnames";
import type { LiHTMLAttributes } from "react";

import "./ListItem.scss";

const ListItem = ({ children, className, ...rest }: LiHTMLAttributes<HTMLLIElement>) => {
  return (
    <li
      className={classNames("list-item", className)}
      {...rest}
    >
      {children}
    </li>
  );
};

export default ListItem;
