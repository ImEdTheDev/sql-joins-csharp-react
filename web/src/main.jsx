import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import PurchasesDashboard from "./PurchasesDashboard";
import "./index.css";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <PurchasesDashboard />
  </StrictMode>
);
