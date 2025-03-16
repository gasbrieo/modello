import classNames from "classnames";
import type { HTMLAttributes } from "react";

import "./List.scss";

const List = ({ children, className, ...rest }: HTMLAttributes<HTMLUListElement>) => {
  return (
    <ul
      className={classNames("list", className)}
      {...rest}
    >
      {children}
    </ul>
  );
};

export default List;
