const webpack = require('webpack');
const config = require('../config');

// Style loader is used to facilitate HMR in development
const styleLoaders = require('./style-loaders')(/* useStyleLoader */ true);

const { cssLoaders, sassLoaders } = styleLoaders;

const backendServerPort = config.Server.BackendServerPort;
const webpackDevServerPort = config.Server.WebpackDevServerPort;

module.exports = {
  devtool: 'inline-source-map',
  entry: {
    main: [
      config.EntryPath,
    ],
  },
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: ['babel-loader'],
      },
      {
        test: /\.css$/,
        use: cssLoaders,
      },
      {
        test: /\.scss$/,
        use: sassLoaders,
      },
    ],
  },
  output: {
    filename: '[name].js',
  },
  devServer: {
    // enable HMR on the server
    hot: true,

    port: webpackDevServerPort,

    contentBase: config.PublicPath,

    publicPath: '/dist/',

    // For 404 responses redirect to /
    historyApiFallback: {
      index: '/',
    },
    proxy: {
      /*
          Proxies the / and /api url requests to the backend
      */
      '/': `http://localhost:${ backendServerPort }`,
      '/api': `http://localhost:${ backendServerPort }/api`,
    },
    overlay: true,
    stats: {
      assets: true,
      children: false,
      chunks: false,
      hash: false,
      modules: false,
      publicPath: false,
      timings: true,
      version: false,
      warnings: true,
      colors: true,
    },
  },
  plugins: [
    new webpack.HotModuleReplacementPlugin(),
    new webpack.NamedModulesPlugin(),
  ],
};
