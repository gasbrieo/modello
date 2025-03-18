import classNames from "classnames";
import { ChevronDownIcon } from "lucide-react";
import { ButtonHTMLAttributes } from "react";

import Avatar from "@/components/Avatar";
import Typography from "@/components/Typography";

import "./SidebarItemAccount.scss";

interface SidebarItemAccountProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  name: string;
  avatar: string;
}

const SidebarItemAccount = ({ className, name, avatar, ...rest }: SidebarItemAccountProps) => {
  return (
    <button
      className={classNames("sidebar-item-account", className)}
      {...rest}
    >
      <Avatar>
        <img
          alt="Avatar"
          src={avatar}
        />
      </Avatar>
      <Typography variant="body2">{name}</Typography>
      <ChevronDownIcon
        width="1rem"
        height="1rem"
        color="rgba(0 0 0 / 40%)"
      />
    </button>
  );
};

export default SidebarItemAccount;
