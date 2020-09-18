var path = require("path");
const statements = require('tsx-control-statements').default;
const keysTransformer = require('ts-transformer-keys/transformer').default;

const TsconfigPathsPlugin = require('tsconfig-paths-webpack-plugin');

module.exports = {
	mode: "development",
	entry: {
		bundle: "./React/index.ts",
		logonBundle: "./React/logonIndex.tsx",
		otherBundle: "./React/otherEntryPoint.tsx",
	},
	output: {
		filename: "[name].js",
		path: path.resolve(__dirname, "wwwroot/dist/")
	},
	devtool: "source-map",
	module: {
		rules: [
			{
				test: /\.less$/,
				use: [
					"style-loader",// : MiniCssExtractPlugin.loader,
					"css-loader",
					"postcss-loader",
					"less-loader",
				]
			},
			{
				test: /\.tsx?$/,
				loader: "awesome-typescript-loader",
				exclude: /node_modules/,
				options: {
					getCustomTransformers: program => ({ 
						before: [
							statements(),
							keysTransformer(program)
						], 
					})
				}
			}
		]
	},
	resolve: {
		extensions: [".tsx", ".ts", ".js", ".jsx"],
		plugins: [new TsconfigPathsPlugin({configFile: "./tsconfig.json"})],
		modules: ["node_modules", "React/Common/"]
	},
};