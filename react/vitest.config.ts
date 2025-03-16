import { resolve } from "path";
import { defineConfig } from "vitest/config";

export default defineConfig({
  resolve: {
    alias: [{ find: "@", replacement: resolve(__dirname, "src") }],
  },
  test: {
    globals: true,
    environment: "jsdom",
    setupFiles: "./src/setupTests.ts",
    coverage: {
      reporter: ["lcov", "html"],
      include: ["src"],
      exclude: ["**/*.test.tsx", "**/index.ts", "**/setupTests.ts", "**/main.tsx", "**/__mocks__/**"],
    },
  },
});
