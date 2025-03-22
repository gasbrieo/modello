import { useState, type FC } from "react";
import Avatar from "@mui/material/Avatar";
import Tooltip from "@mui/material/Tooltip";
import IconButton from "@mui/material/IconButton";
import ListItem from "@mui/material/ListItem";
import MenuItem from "@mui/material/MenuItem";
import ListItemIcon from "@mui/material/ListItemIcon";
import Menu from "@mui/material/Menu";
import ListItemText from "@mui/material/ListItemText";
import LogoutIcon from "@mui/icons-material/Logout";

const SideMenuAccount: FC = () => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <>
      <ListItem
        disablePadding
        sx={{
          justifyContent: "center",
        }}
      >
        <Tooltip
          title="Conta"
          placement="right"
        >
          <IconButton
            size="small"
            aria-controls={open ? "account-menu" : undefined}
            aria-haspopup="true"
            aria-expanded={open ? "true" : undefined}
            onClick={handleClick}
          >
            <Avatar
              src="https://api.dicebear.com/7.x/initials/svg?seed=G"
              sx={{ width: 32, height: 32 }}
            />
          </IconButton>
        </Tooltip>
      </ListItem>
      <Menu
        anchorEl={anchorEl}
        id="account-menu"
        open={open}
        onClose={handleClose}
        onClick={handleClose}
        anchorOrigin={{ vertical: "top", horizontal: "right" }}
        slotProps={{
          paper: {
            elevation: 0,
            sx: {
              overflow: "visible",
              filter: "drop-shadow(0px 2px 8px rgba(0,0,0,0.32))",
              mt: -0.5,
              ml: 1,
              minWidth: 154,
              maxWidth: 244,
            },
          },
        }}
      >
        <MenuItem onClick={handleClose}>
          <ListItemIcon>
            <LogoutIcon fontSize="small" />
          </ListItemIcon>
          <ListItemText primary="Sair" />
        </MenuItem>
      </Menu>
    </>
  );
};

export default SideMenuAccount;
