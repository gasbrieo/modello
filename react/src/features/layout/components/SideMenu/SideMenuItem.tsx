import type { FC, ReactNode } from "react";

import ListItem from "@mui/material/ListItem";
import ListItemButton, { type ListItemButtonProps } from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import Tooltip from "@mui/material/Tooltip";

export interface SideMenuItemProps extends ListItemButtonProps {
  icon: ReactNode;
  label: string;
}

const SideMenuItem: FC<SideMenuItemProps> = ({ icon, label, ...rest }) => {
  return (
    <ListItem disablePadding>
      <Tooltip
        title={label}
        placement="right"
      >
        <ListItemButton
          aria-label={label}
          sx={{
            minHeight: 48,
            justifyContent: "center",
          }}
          {...rest}
        >
          <ListItemIcon
            sx={{
              minWidth: 0,
              justifyContent: "center",
            }}
          >
            {icon}
          </ListItemIcon>
        </ListItemButton>
      </Tooltip>
    </ListItem>
  );
};

export default SideMenuItem;
