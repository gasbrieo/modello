import { type FC, type ReactNode, useMemo } from "react";

import { ThemeProvider, createTheme } from "@mui/material/styles";

interface ThemeProps {
  children: ReactNode;
}

const Theme: FC<ThemeProps> = ({ children }) => {
  const theme = useMemo(() => createTheme(), []);

  return <ThemeProvider theme={theme}>{children}</ThemeProvider>;
};

export default Theme;
