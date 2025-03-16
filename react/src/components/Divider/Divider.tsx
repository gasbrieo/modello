import classNames from "classnames";
import type { HTMLAttributes } from "react";

import "./Divider.scss";

const Divider = ({ className, ...rest }: Omit<HTMLAttributes<HTMLHRElement>, "children">) => {
  return (
    <hr
      className={classNames("divider", className)}
      {...rest}
    />
  );
};

export default Divider;
