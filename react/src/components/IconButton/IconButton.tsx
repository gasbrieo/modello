import classNames from "classnames";
import { type ButtonHTMLAttributes, forwardRef } from "react";

import "./IconButton.scss";

type IconButtonSize = "small" | "medium" | "large";
type IconButtonShape = "square" | "rounded";

interface IconButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  size?: IconButtonSize;
  shape?: IconButtonShape;
}

const IconButton = forwardRef<HTMLButtonElement, IconButtonProps>(({ children, className, size = "medium", shape = "square", ...rest }, ref) => {
  return (
    <button
      className={classNames(
        "icon-button",
        {
          [`icon-button--size-${size}`]: size,
          [`icon-button--shape-${shape}`]: shape,
        },
        className
      )}
      ref={ref}
      {...rest}
    >
      {children}
    </button>
  );
});

IconButton.displayName = "IconButton";

export default IconButton;
