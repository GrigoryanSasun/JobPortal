const webpackMerge = require('webpack-merge');

module.exports = function (env) {
  const webpackConfigPath = './config/webpack';
  const commonConfig = require(`${ webpackConfigPath }/webpack.common.js`);
  const envSpecificConfig = require(`${ webpackConfigPath }/webpack.${ env }.js`);

  const config = webpackMerge(commonConfig, envSpecificConfig);

  return config;
};
