var path = require("path");
var MiniCssExtractPlugin = require("mini-css-extract-plugin");
const statements = require('tsx-control-statements').default;

const TsconfigPathsPlugin = require('tsconfig-paths-webpack-plugin');
var scriptCfg = {
	mode: "development",
	entry: {
		bundle: "./React/index.ts",
		logonBundle: "./React/logonIndex.ts",
		//otherBundle: "./React/otherEntryPoint.ts",
	},
	output: {
		filename: "[name].js",
		path: path.resolve(__dirname, "wwwroot/dist/")
	},
	devtool: "hidden-source-map",
	module: {
		rules: [
			{
				test: /\.tsx?$/,
				loader: "awesome-typescript-loader",
				exclude: /node_modules/,
				options: {
					getCustomTransformers: () => ({ before: [statements()] })
				}
			}
		]
	},
	resolve: {
		extensions: [".tsx", ".ts", ".js", ".jsx"],
		plugins: [new TsconfigPathsPlugin({configFile: "./tsconfig.json"})],
		modules: ["node_modules"]
	},
};

var lessCfg = {
	mode: "development",
	entry: {
		cssBundle: "./CSS/Main.less",
		logonCssBundle: "./CSS/LogOn.less",
		otherCssBundle: "./CSS/Other.less",
	},
	output: {
		path: path.resolve(__dirname, "wwwroot/dist/")
	},
	module: {
		rules: [
			{
				test: /\.less/,
				use: [
					{
						loader: MiniCssExtractPlugin.loader,
						options:{
							publicPath: path.resolve(__dirname, "wwwroot/dist/")
						},
					},
					{
						loader: "css-loader",
					},
					{
						loader: "less-loader"
					}
				]
			}
		]
	},
	plugins: [
		new MiniCssExtractPlugin({
			// Options similar to the same options in webpackOptions.output
			// both options are optional
			filename: '[name].css',
		})
	],
};

module.exports = [
	scriptCfg,
	lessCfg
];