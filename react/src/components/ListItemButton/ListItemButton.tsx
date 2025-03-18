import classNames from "classnames";
import { type ElementType, type ComponentPropsWithRef, type Ref, forwardRef } from "react";

import "./ListItemButton.scss";

export type ListItemButtonProps<T extends ElementType = "button"> = {
  as?: T;
} & ComponentPropsWithRef<T>;

const ListItemButton = <T extends ElementType = "button">({ as, className, children, ...rest }: ListItemButtonProps<T>, ref: Ref<HTMLElement>) => {
  const Component = as || "button";

  return (
    <Component
      className={classNames("list-item-button", className)}
      ref={ref}
      {...rest}
    >
      {children}
    </Component>
  );
};

export default forwardRef(ListItemButton);
