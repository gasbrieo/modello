import { BellIcon } from "lucide-react";
import { ButtonHTMLAttributes } from "react";

import IconButton from "@/components/IconButton";

const SidebarItemNotifications = ({ ...rest }: ButtonHTMLAttributes<HTMLButtonElement>) => {
  return (
    <IconButton {...rest}>
      <BellIcon
        width="1rem"
        height="1rem"
        color="rgba(0 0 0 / 40%)"
      />
    </IconButton>
  );
};

export default SidebarItemNotifications;
