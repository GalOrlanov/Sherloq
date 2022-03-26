import TsconfigPathsPlugin from "tsconfig-paths-webpack-plugin";

export default {
  //other rules
  resolve: {
    plugins: [
      new TsconfigPathsPlugin({
        extensions: [".js", ".jsx", ".json", ".ts", ".tsx"],
      }),
    ],
  },
};
