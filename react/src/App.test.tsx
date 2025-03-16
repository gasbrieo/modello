import { describe, expect, it } from "vitest";
import { render, screen } from "@testing-library/react";

import App from "./App";

describe("App", () => {
  it("should render index route", async () => {
    render(<App />);

    expect(await screen.findByText("Index")).toBeInTheDocument();
  });
});
