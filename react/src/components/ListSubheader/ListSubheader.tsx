import classNames from "classnames";
import type { LiHTMLAttributes } from "react";

import "./ListSubheader.scss";

const ListSubheader = ({ children, className, ...rest }: LiHTMLAttributes<HTMLLIElement>) => {
  return (
    <li
      className={classNames("list-subheader", className)}
      {...rest}
    >
      {children}
    </li>
  );
};

export default ListSubheader;
