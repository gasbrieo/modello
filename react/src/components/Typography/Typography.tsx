import classNames from "classnames";
import type { HTMLAttributes } from "react";

import "./Typography.scss";

type TypographyVariant = "h1" | "h2" | "h3" | "h4" | "h5" | "h6" | "subtitle1" | "subtitle2" | "body1" | "body2" | "caption";

interface TypographyProps extends HTMLAttributes<HTMLParagraphElement> {
  variant?: TypographyVariant;
}

const Typography = ({ children, className, variant = "body2", ...rest }: TypographyProps) => {
  return (
    <p
      className={classNames(
        "typography",
        {
          [`typography--variant-${variant}`]: variant,
        },
        className
      )}
      {...rest}
    >
      {children}
    </p>
  );
};

export default Typography;
