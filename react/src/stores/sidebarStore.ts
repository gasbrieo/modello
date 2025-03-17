import { create } from "zustand";

type SidebarState = {
  isOpened: boolean;
  toggle: () => void;
};

export const useSidebarStore = create<SidebarState>((set) => ({
  isOpened: true,

  toggle: () => {
    set((state) => ({ isOpened: !state.isOpened }));
  },
}));
