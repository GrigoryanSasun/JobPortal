const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const config = require('../config');

const styleLoaders = require('./style-loaders')(/* useStyleLoader */ false);

const { cssLoaders: cssLoaders, sassLoaders: sassLoaders } = styleLoaders;

const backendServerPort = config.Server.BackendServerPort;
const webpackDevServerPort = config.Server.WebpackDevServerPort;

module.exports = {
  devtool: 'inline-source-map',
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: ['babel-loader'],
      },
      {
        test: /\.css$/,
        use: ['css-hot-loader'].concat(ExtractTextPlugin.extract({
          use: cssLoaders,
        }))
      },
      {
        test: /\.scss$/,
        use: ['css-hot-loader'].concat(ExtractTextPlugin.extract({
          use: sassLoaders,
        }))
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

    publicPath: config.OutputPublicPath,

    // For 404 responses redirect to /
    historyApiFallback: {
      index: '/',
    },
    proxy: {
      /*
          Proxies the / and /api url requests to the backend
      */
      '/': `http://localhost:${backendServerPort}`,
      '/api': `http://localhost:${backendServerPort}/api`,
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
    new ExtractTextPlugin({
      filename: '[name].css',
      allChunks: true
    }),
    new webpack.HotModuleReplacementPlugin(),
    new webpack.NamedModulesPlugin(),
  ],
};
