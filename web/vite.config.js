import {defineConfig} from 'vite';
import react from '@vitejs/plugin-react';

// In development, requests to /api go to the ASP.NET Core API (see ../api).
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173,
    proxy: {
      '/api': 'http://localhost:5080',
    },
  },
});
