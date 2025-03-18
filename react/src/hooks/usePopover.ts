import { useState } from "react";
import { type Middleware, type Placement, autoUpdate, useClick, useDismiss, useFloating } from "@floating-ui/react";

interface UsePopoverOptions {
  placement?: Placement;
  middleware?: Middleware[];
}

export const usePopover = ({ placement = "bottom", middleware = [] }: UsePopoverOptions = {}) => {
  const [isOpen, setIsOpen] = useState(false);

  const { refs, floatingStyles, context } = useFloating({
    placement,
    middleware: middleware,
    whileElementsMounted: autoUpdate,
    open: isOpen,
    onOpenChange: setIsOpen,
  });

  useClick(context);
  useDismiss(context);

  const toggleIsOpen = () => setIsOpen((prev) => !prev);

  return {
    isOpen,
    refs,
    floatingStyles,
    toggleIsOpen,
  };
};
