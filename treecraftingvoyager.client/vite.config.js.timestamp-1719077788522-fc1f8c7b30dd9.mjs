// vite.config.js
import { fileURLToPath, URL } from "node:url";
import { defineConfig } from "file:///H:/Czuczen%20projects/ASP.NET/TREECRAFTINGVOYAGER/TreeCraftingVoyager/treecraftingvoyager.client/node_modules/vite/dist/node/index.js";
import plugin from "file:///H:/Czuczen%20projects/ASP.NET/TREECRAFTINGVOYAGER/TreeCraftingVoyager/treecraftingvoyager.client/node_modules/@vitejs/plugin-vue/dist/index.mjs";
import fs from "fs";
import path from "path";
import child_process from "child_process";
import { env } from "process";
var __vite_injected_original_import_meta_url = "file:///H:/Czuczen%20projects/ASP.NET/TREECRAFTINGVOYAGER/TreeCraftingVoyager/treecraftingvoyager.client/vite.config.js";
var baseFolder = env.APPDATA !== void 0 && env.APPDATA !== "" ? `${env.APPDATA}/ASP.NET/https` : `${env.HOME}/.aspnet/https`;
var certificateName = "treecraftingvoyager.client";
var certFilePath = path.join(baseFolder, `${certificateName}.pem`);
var keyFilePath = path.join(baseFolder, `${certificateName}.key`);
if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
  if (0 !== child_process.spawnSync("dotnet", [
    "dev-certs",
    "https",
    "--export-path",
    certFilePath,
    "--format",
    "Pem",
    "--no-password"
  ], { stdio: "inherit" }).status) {
    throw new Error("Could not create certificate.");
  }
}
var target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` : env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(";")[0] : "https://localhost:7265";
console.log("Target: " + target);
var vite_config_default = defineConfig({
  plugins: [plugin()],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", __vite_injected_original_import_meta_url))
    }
  },
  server: {
    proxy: {
      "^/api/": {
        target,
        changeOrigin: true,
        secure: false,
        configure: (proxy, options) => {
          proxy.on("proxyReq", (proxyReq, req, res) => {
            console.log("Proxying request to:", target);
            console.log("Request Headers:", proxyReq.getHeaders());
          });
          proxy.on("proxyRes", (proxyRes, req, res) => {
            console.log("Response Headers:", proxyRes.headers);
          });
        }
      }
    },
    port: 5173,
    https: {
      key: fs.readFileSync(keyFilePath),
      cert: fs.readFileSync(certFilePath)
    },
    open: true
    // This will open the browser automatically when Vite server starts
  }
});
export {
  vite_config_default as default
};
//# sourceMappingURL=data:application/json;base64,ewogICJ2ZXJzaW9uIjogMywKICAic291cmNlcyI6IFsidml0ZS5jb25maWcuanMiXSwKICAic291cmNlc0NvbnRlbnQiOiBbImNvbnN0IF9fdml0ZV9pbmplY3RlZF9vcmlnaW5hbF9kaXJuYW1lID0gXCJIOlxcXFxDenVjemVuIHByb2plY3RzXFxcXEFTUC5ORVRcXFxcVFJFRUNSQUZUSU5HVk9ZQUdFUlxcXFxUcmVlQ3JhZnRpbmdWb3lhZ2VyXFxcXHRyZWVjcmFmdGluZ3ZveWFnZXIuY2xpZW50XCI7Y29uc3QgX192aXRlX2luamVjdGVkX29yaWdpbmFsX2ZpbGVuYW1lID0gXCJIOlxcXFxDenVjemVuIHByb2plY3RzXFxcXEFTUC5ORVRcXFxcVFJFRUNSQUZUSU5HVk9ZQUdFUlxcXFxUcmVlQ3JhZnRpbmdWb3lhZ2VyXFxcXHRyZWVjcmFmdGluZ3ZveWFnZXIuY2xpZW50XFxcXHZpdGUuY29uZmlnLmpzXCI7Y29uc3QgX192aXRlX2luamVjdGVkX29yaWdpbmFsX2ltcG9ydF9tZXRhX3VybCA9IFwiZmlsZTovLy9IOi9DenVjemVuJTIwcHJvamVjdHMvQVNQLk5FVC9UUkVFQ1JBRlRJTkdWT1lBR0VSL1RyZWVDcmFmdGluZ1ZveWFnZXIvdHJlZWNyYWZ0aW5ndm95YWdlci5jbGllbnQvdml0ZS5jb25maWcuanNcIjtpbXBvcnQgeyBmaWxlVVJMVG9QYXRoLCBVUkwgfSBmcm9tICdub2RlOnVybCc7XHJcblxyXG5pbXBvcnQgeyBkZWZpbmVDb25maWcgfSBmcm9tICd2aXRlJztcclxuaW1wb3J0IHBsdWdpbiBmcm9tICdAdml0ZWpzL3BsdWdpbi12dWUnO1xyXG5pbXBvcnQgZnMgZnJvbSAnZnMnO1xyXG5pbXBvcnQgcGF0aCBmcm9tICdwYXRoJztcclxuaW1wb3J0IGNoaWxkX3Byb2Nlc3MgZnJvbSAnY2hpbGRfcHJvY2Vzcyc7XHJcbmltcG9ydCB7IGVudiB9IGZyb20gJ3Byb2Nlc3MnO1xyXG5cclxuY29uc3QgYmFzZUZvbGRlciA9XHJcbiAgICBlbnYuQVBQREFUQSAhPT0gdW5kZWZpbmVkICYmIGVudi5BUFBEQVRBICE9PSAnJ1xyXG4gICAgICAgID8gYCR7ZW52LkFQUERBVEF9L0FTUC5ORVQvaHR0cHNgXHJcbiAgICAgICAgOiBgJHtlbnYuSE9NRX0vLmFzcG5ldC9odHRwc2A7XHJcblxyXG5jb25zdCBjZXJ0aWZpY2F0ZU5hbWUgPSBcInRyZWVjcmFmdGluZ3ZveWFnZXIuY2xpZW50XCI7XHJcbmNvbnN0IGNlcnRGaWxlUGF0aCA9IHBhdGguam9pbihiYXNlRm9sZGVyLCBgJHtjZXJ0aWZpY2F0ZU5hbWV9LnBlbWApO1xyXG5jb25zdCBrZXlGaWxlUGF0aCA9IHBhdGguam9pbihiYXNlRm9sZGVyLCBgJHtjZXJ0aWZpY2F0ZU5hbWV9LmtleWApO1xyXG5cclxuaWYgKCFmcy5leGlzdHNTeW5jKGNlcnRGaWxlUGF0aCkgfHwgIWZzLmV4aXN0c1N5bmMoa2V5RmlsZVBhdGgpKSB7XHJcbiAgICBpZiAoMCAhPT0gY2hpbGRfcHJvY2Vzcy5zcGF3blN5bmMoJ2RvdG5ldCcsIFtcclxuICAgICAgICAnZGV2LWNlcnRzJyxcclxuICAgICAgICAnaHR0cHMnLFxyXG4gICAgICAgICctLWV4cG9ydC1wYXRoJyxcclxuICAgICAgICBjZXJ0RmlsZVBhdGgsXHJcbiAgICAgICAgJy0tZm9ybWF0JyxcclxuICAgICAgICAnUGVtJyxcclxuICAgICAgICAnLS1uby1wYXNzd29yZCcsXHJcbiAgICBdLCB7IHN0ZGlvOiAnaW5oZXJpdCcsIH0pLnN0YXR1cykge1xyXG4gICAgICAgIHRocm93IG5ldyBFcnJvcihcIkNvdWxkIG5vdCBjcmVhdGUgY2VydGlmaWNhdGUuXCIpO1xyXG4gICAgfVxyXG59XHJcblxyXG5jb25zdCB0YXJnZXQgPSBlbnYuQVNQTkVUQ09SRV9IVFRQU19QT1JUID8gYGh0dHBzOi8vbG9jYWxob3N0OiR7ZW52LkFTUE5FVENPUkVfSFRUUFNfUE9SVH1gIDpcclxuICAgIGVudi5BU1BORVRDT1JFX1VSTFMgPyBlbnYuQVNQTkVUQ09SRV9VUkxTLnNwbGl0KCc7JylbMF0gOiAnaHR0cHM6Ly9sb2NhbGhvc3Q6NzI2NSc7XHJcblxyXG5jb25zb2xlLmxvZyhcIlRhcmdldDogXCIgKyB0YXJnZXQpO1xyXG5cclxuLy8gaHR0cHM6Ly92aXRlanMuZGV2L2NvbmZpZy9cclxuZXhwb3J0IGRlZmF1bHQgZGVmaW5lQ29uZmlnKHtcclxuICAgIHBsdWdpbnM6IFtwbHVnaW4oKV0sXHJcbiAgICByZXNvbHZlOiB7XHJcbiAgICAgICAgYWxpYXM6IHtcclxuICAgICAgICAgICAgJ0AnOiBmaWxlVVJMVG9QYXRoKG5ldyBVUkwoJy4vc3JjJywgaW1wb3J0Lm1ldGEudXJsKSlcclxuICAgICAgICB9XHJcbiAgICB9LFxyXG4gICAgc2VydmVyOiB7XHJcbiAgICAgICAgcHJveHk6IHtcclxuICAgICAgICAgICAgJ14vYXBpLyc6IHtcclxuICAgICAgICAgICAgICAgIHRhcmdldCxcclxuICAgICAgICAgICAgICAgIGNoYW5nZU9yaWdpbjogdHJ1ZSxcclxuICAgICAgICAgICAgICAgIHNlY3VyZTogZmFsc2UsXHJcbiAgICAgICAgICAgICAgICBjb25maWd1cmU6IChwcm94eSwgb3B0aW9ucykgPT4ge1xyXG4gICAgICAgICAgICAgICAgICAgIHByb3h5Lm9uKCdwcm94eVJlcScsIChwcm94eVJlcSwgcmVxLCByZXMpID0+IHtcclxuICAgICAgICAgICAgICAgICAgICAgICAgY29uc29sZS5sb2coJ1Byb3h5aW5nIHJlcXVlc3QgdG86JywgdGFyZ2V0KTtcclxuICAgICAgICAgICAgICAgICAgICAgICAgY29uc29sZS5sb2coJ1JlcXVlc3QgSGVhZGVyczonLCBwcm94eVJlcS5nZXRIZWFkZXJzKCkpO1xyXG4gICAgICAgICAgICAgICAgICAgIH0pO1xyXG4gICAgICAgICAgICAgICAgICAgIHByb3h5Lm9uKCdwcm94eVJlcycsIChwcm94eVJlcywgcmVxLCByZXMpID0+IHtcclxuICAgICAgICAgICAgICAgICAgICAgICAgY29uc29sZS5sb2coJ1Jlc3BvbnNlIEhlYWRlcnM6JywgcHJveHlSZXMuaGVhZGVycyk7XHJcbiAgICAgICAgICAgICAgICAgICAgfSk7XHJcbiAgICAgICAgICAgICAgICB9XHJcbiAgICAgICAgICAgIH1cclxuICAgICAgICB9LFxyXG4gICAgICAgIHBvcnQ6IDUxNzMsXHJcbiAgICAgICAgaHR0cHM6IHtcclxuICAgICAgICAgICAga2V5OiBmcy5yZWFkRmlsZVN5bmMoa2V5RmlsZVBhdGgpLFxyXG4gICAgICAgICAgICBjZXJ0OiBmcy5yZWFkRmlsZVN5bmMoY2VydEZpbGVQYXRoKSxcclxuICAgICAgICB9LFxyXG4gICAgICAgIG9wZW46IHRydWUgLy8gVGhpcyB3aWxsIG9wZW4gdGhlIGJyb3dzZXIgYXV0b21hdGljYWxseSB3aGVuIFZpdGUgc2VydmVyIHN0YXJ0c1xyXG4gICAgfVxyXG59KVxyXG4iXSwKICAibWFwcGluZ3MiOiAiO0FBQTBkLFNBQVMsZUFBZSxXQUFXO0FBRTdmLFNBQVMsb0JBQW9CO0FBQzdCLE9BQU8sWUFBWTtBQUNuQixPQUFPLFFBQVE7QUFDZixPQUFPLFVBQVU7QUFDakIsT0FBTyxtQkFBbUI7QUFDMUIsU0FBUyxXQUFXO0FBUDJSLElBQU0sMkNBQTJDO0FBU2hXLElBQU0sYUFDRixJQUFJLFlBQVksVUFBYSxJQUFJLFlBQVksS0FDdkMsR0FBRyxJQUFJLE9BQU8sbUJBQ2QsR0FBRyxJQUFJLElBQUk7QUFFckIsSUFBTSxrQkFBa0I7QUFDeEIsSUFBTSxlQUFlLEtBQUssS0FBSyxZQUFZLEdBQUcsZUFBZSxNQUFNO0FBQ25FLElBQU0sY0FBYyxLQUFLLEtBQUssWUFBWSxHQUFHLGVBQWUsTUFBTTtBQUVsRSxJQUFJLENBQUMsR0FBRyxXQUFXLFlBQVksS0FBSyxDQUFDLEdBQUcsV0FBVyxXQUFXLEdBQUc7QUFDN0QsTUFBSSxNQUFNLGNBQWMsVUFBVSxVQUFVO0FBQUEsSUFDeEM7QUFBQSxJQUNBO0FBQUEsSUFDQTtBQUFBLElBQ0E7QUFBQSxJQUNBO0FBQUEsSUFDQTtBQUFBLElBQ0E7QUFBQSxFQUNKLEdBQUcsRUFBRSxPQUFPLFVBQVcsQ0FBQyxFQUFFLFFBQVE7QUFDOUIsVUFBTSxJQUFJLE1BQU0sK0JBQStCO0FBQUEsRUFDbkQ7QUFDSjtBQUVBLElBQU0sU0FBUyxJQUFJLHdCQUF3QixxQkFBcUIsSUFBSSxxQkFBcUIsS0FDckYsSUFBSSxrQkFBa0IsSUFBSSxnQkFBZ0IsTUFBTSxHQUFHLEVBQUUsQ0FBQyxJQUFJO0FBRTlELFFBQVEsSUFBSSxhQUFhLE1BQU07QUFHL0IsSUFBTyxzQkFBUSxhQUFhO0FBQUEsRUFDeEIsU0FBUyxDQUFDLE9BQU8sQ0FBQztBQUFBLEVBQ2xCLFNBQVM7QUFBQSxJQUNMLE9BQU87QUFBQSxNQUNILEtBQUssY0FBYyxJQUFJLElBQUksU0FBUyx3Q0FBZSxDQUFDO0FBQUEsSUFDeEQ7QUFBQSxFQUNKO0FBQUEsRUFDQSxRQUFRO0FBQUEsSUFDSixPQUFPO0FBQUEsTUFDSCxVQUFVO0FBQUEsUUFDTjtBQUFBLFFBQ0EsY0FBYztBQUFBLFFBQ2QsUUFBUTtBQUFBLFFBQ1IsV0FBVyxDQUFDLE9BQU8sWUFBWTtBQUMzQixnQkFBTSxHQUFHLFlBQVksQ0FBQyxVQUFVLEtBQUssUUFBUTtBQUN6QyxvQkFBUSxJQUFJLHdCQUF3QixNQUFNO0FBQzFDLG9CQUFRLElBQUksb0JBQW9CLFNBQVMsV0FBVyxDQUFDO0FBQUEsVUFDekQsQ0FBQztBQUNELGdCQUFNLEdBQUcsWUFBWSxDQUFDLFVBQVUsS0FBSyxRQUFRO0FBQ3pDLG9CQUFRLElBQUkscUJBQXFCLFNBQVMsT0FBTztBQUFBLFVBQ3JELENBQUM7QUFBQSxRQUNMO0FBQUEsTUFDSjtBQUFBLElBQ0o7QUFBQSxJQUNBLE1BQU07QUFBQSxJQUNOLE9BQU87QUFBQSxNQUNILEtBQUssR0FBRyxhQUFhLFdBQVc7QUFBQSxNQUNoQyxNQUFNLEdBQUcsYUFBYSxZQUFZO0FBQUEsSUFDdEM7QUFBQSxJQUNBLE1BQU07QUFBQTtBQUFBLEVBQ1Y7QUFDSixDQUFDOyIsCiAgIm5hbWVzIjogW10KfQo=
