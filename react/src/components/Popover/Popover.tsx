import classNames from "classnames";
import { forwardRef, type HTMLAttributes } from "react";

import "./Popover.scss";

interface PopoverProps extends HTMLAttributes<HTMLDivElement> {
  isOpen?: boolean;
}

const Popover = forwardRef<HTMLDivElement, PopoverProps>(({ children, className, isOpen, ...rest }, ref) => {
  if (!isOpen) return null;

  return (
    <div
      className={classNames("popover", className)}
      ref={ref}
      {...rest}
    >
      {children}
    </div>
  );
});

export default Popover;
