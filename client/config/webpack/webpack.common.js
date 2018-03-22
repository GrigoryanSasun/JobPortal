const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');

const config = require('../config');

const NODE_ENV = process.env.NODE_ENV || 'development';

module.exports = {
  entry: {
    main: config.EntryPath,
    vendor: config.VendorPath,
    polyfills: config.PolyfillsPath,
  },
  resolve: {
    extensions: ['.js', '.jsx'],
  },
  module: {
    rules: [
      {
        enforce: 'pre',
        test: /\.(jsx?)$/,
        use: ['source-map-loader'],
      },
      { test: /\.(png|jpe?g|gif|svg|woff|woff2|ttf|eot|ico)$/, use: 'file-loader' },
      {
        test: /\.html$/,
        use: [
          { loader: 'ngtemplate-loader?relativeTo=' + config.AppPath },
          { loader: 'html-loader' }
        ]
      },
    ],
  },
  output: {
    path: config.OutputPath,
    publicPath: '/dist/',
  },
  plugins: [
    new webpack.optimize.CommonsChunkPlugin({
      name: 'vendor',
      chunks: ['main'],
      minChunks: (module) => {
        return module.context &&
          module.context.indexOf('node_modules') !== -1;
      },
    }),

    new webpack.optimize.CommonsChunkPlugin({
      name: 'polyfills',
      chunks: ['vendor'],
    }),

    new webpack.optimize.CommonsChunkPlugin({
      name: 'manifest',
    }),

    new webpack.DefinePlugin({
      'process.env': {
        NODE_ENV: JSON.stringify(NODE_ENV),
      },
    })
  ],
};
