const path = require('path');

const rootPath = path.resolve(__dirname, '../../');

const serverPath = path.join(rootPath, 'server', 'JobPortal.Web');
const publicPath = path.join(serverPath, 'Content');

const clientPath = path.join(rootPath, 'client');
const srcPath = path.join(clientPath, 'src');
const appPath = path.join(srcPath, 'app');
const themesPath = path.join(appPath, 'themes');

const backendPort = process.env.BACKEND_SERVER_PORT || 8000;
const webpackDevServerPort = process.env.WEBPACK_DEV_SERVER_PORT || 8080;

module.exports = {
  Server: {
    Path: serverPath,
    BackendServerPort: backendPort,
    WebpackDevServerPort: webpackDevServerPort
  },
  AppPath: appPath,
  EntryPath: path.join(appPath, 'index.js'),
  VendorPath: path.join(appPath, 'vendor.js'),
  PolyfillsPath: path.join(appPath, 'polyfills.js'),
  PublicPath: publicPath,
  OutputPath: path.join(publicPath, 'dist'),
  OutputPublicPath: '/Content/dist/',
  NodeModulesPath: path.join(clientPath, 'node_modules'),
  getThemePath: (themeName) => {
    return path.join(themesPath, themeName + '.scss')
  },
};
