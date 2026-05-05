import { createRoot } from "react-dom/client";
import { initializeFaro, getWebInstrumentations } from "@grafana/faro-web-sdk";
import App from "./App.tsx";
import "./index.css";

initializeFaro({
  url: "https://tu-grafana.com/collect",
  app: {
    name: "CRMSarritel",
    version: "1.0.0",
  },
  instrumentations: getWebInstrumentations({
    captureConsole: true,
    captureErrors: true,
  }),
});

createRoot(document.getElementById("root")!).render(<App />);