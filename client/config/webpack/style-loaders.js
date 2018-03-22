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
    },
  ]);

  const sassLoaders = cssLoaders.concat([
    {
      loader: 'sass-loader',
    },
  ]);

  return {
    cssLoaders,
    sassLoaders,
  };
};
