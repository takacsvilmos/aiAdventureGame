import { defineConfig } from 'vite'
import { webcrypto } from 'crypto';
import react from '@vitejs/plugin-react'

if (!globalThis.crypto) {
  globalThis.crypto = webcrypto;
}
// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
})
