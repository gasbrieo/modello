import { EllipsisIcon, ListCollapseIcon } from "lucide-react";

import IconButton from "@/components/IconButton";
import Typography from "@/components/Typography";
import { useSidebarStore } from "@/stores/sidebarStore";

import "./Home.scss";

const Home = () => {
  const toggleSidebar = useSidebarStore((state) => state.toggle);

  return (
    <div className="home">
      <header className="home__header">
        <div className="home__header__left">
          <IconButton onClick={toggleSidebar}>
            <ListCollapseIcon
              height="1rem"
              width="1rem"
            />
          </IconButton>
          <Typography variant="body2">Home</Typography>
        </div>
        <div className="home__header__right">
          <IconButton>
            <EllipsisIcon
              height="1rem"
              width="1rem"
            />
          </IconButton>
        </div>
      </header>
      <div className="home__content">
        <section className="home__content__hero">
          <Typography variant="h4">Home</Typography>
        </section>
      </div>
    </div>
  );
};

export default Home;
