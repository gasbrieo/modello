import classNames from "classnames";
import type { HTMLAttributes } from "react";

import "./Avatar.scss";

const Avatar = ({ children, className, ...rest }: HTMLAttributes<HTMLDivElement>) => {
  return (
    <div
      className={classNames("avatar", className)}
      {...rest}
    >
      {children}
    </div>
  );
};

export default Avatar;
