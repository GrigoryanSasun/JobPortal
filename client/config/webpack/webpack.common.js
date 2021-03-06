const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');

const config = require('../config');
const themes = require('../../../server/JobPortal.Web/Themes.json');

const NODE_ENV = process.env.NODE_ENV || 'development';

const entry = {
  main: config.EntryPath,
  vendor: config.VendorPath,
  polyfills: config.PolyfillsPath
};

// Sets all the theme files as entry points
for (let i = 0; i < themes.length; i++) {
  const theme = themes[i];
  entry[theme.filename] = config.getThemePath(theme.filename);
}

module.exports = {
  entry,
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
    publicPath: config.OutputPublicPath,
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
