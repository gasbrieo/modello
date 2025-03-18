import { AnimatePresence, motion } from "framer-motion";
import { HTMLAttributes } from "react";

interface CollapseProps extends HTMLAttributes<HTMLDivElement> {
  isOpened: boolean;
}

const Collapse = ({ isOpened, children }: CollapseProps) => {
  return (
    <AnimatePresence>
      {isOpened && (
        <motion.div
          initial={{ height: 0, opacity: 0 }}
          animate={{ height: "auto", opacity: 1 }}
          exit={{ height: 0, opacity: 0 }}
          transition={{ duration: 0.1 }}
          style={{ overflow: "hidden" }}
        >
          {children}
        </motion.div>
      )}
    </AnimatePresence>
  );
};

export default Collapse;
