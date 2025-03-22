import { FC } from "react";

import CssBaseline from "@mui/material/CssBaseline";

import { Router } from "./features/router";
import { Theme } from "./features/theme";

import "@fontsource/roboto/300.css";
import "@fontsource/roboto/400.css";
import "@fontsource/roboto/500.css";
import "@fontsource/roboto/700.css";

const App: FC = () => {
  return (
    <Theme>
      <CssBaseline enableColorScheme />
      <Router />
    </Theme>
  );
};

export default App;
