module.exports = (useStyleLoader) => {
  let cssLoaders = useStyleLoader ? [{
    loader: 'style-loader',
  }] : [];
  cssLoaders = cssLoaders.concat([
    {
      loader: 'css-loader',
    },
    {
      loader: 'postcss-loader',
      options: {
        // bootstrap sass requires precision 8 (https://github.com/twbs/bootstrap-sass)
        precision: 8
      },
    },
  ]);

  const sassLoaders = cssLoaders.concat([
    {
      loader: 'sass-loader',
      options: {
        // bootstrap sass requires precision 8 (https://github.com/twbs/bootstrap-sass)
        precision: 8
      },
    },
  ]);

  return {
    cssLoaders,
    sassLoaders,
  };
};
