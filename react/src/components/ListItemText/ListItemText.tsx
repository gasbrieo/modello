import classNames from "classnames";
import type { HTMLAttributes } from "react";

import Typography from "@/components/Typography";

import "./ListItemText.scss";

export interface ListItemTextProps extends Omit<HTMLAttributes<HTMLDivElement>, "children"> {
  primary: string;
  secondary?: string;
}

const ListItemText = ({ className, primary, secondary, ...rest }: ListItemTextProps) => {
  return (
    <div
      className={classNames("list-item-text", className)}
      {...rest}
    >
      <Typography
        className="list-item-text__primary"
        variant="body2"
      >
        {primary}
      </Typography>
      {secondary && (
        <Typography
          className="list-item-text__secondary"
          variant="body2"
        >
          {secondary}
        </Typography>
      )}
    </div>
  );
};

export default ListItemText;
