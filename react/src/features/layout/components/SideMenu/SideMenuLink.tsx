import { type FC } from "react";
import { Link, type LinkProps } from "@tanstack/react-router";

import SideMenuItem, { type SideMenuItemProps } from "./SideMenuItem";

export interface SideMenuLinkProps extends SideMenuItemProps {
  to: LinkProps["to"];
}

const SideMenuLink: FC<SideMenuLinkProps> = ({ ...rest }) => {
  return (
    <SideMenuItem
      component={Link}
      {...rest}
    />
  );
};

export default SideMenuLink;
