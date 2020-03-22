var path = require("path");
const statements = require('tsx-control-statements').default;
const { CheckerPlugin } = require('awesome-typescript-loader');

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
	devtool: "eval-source-map",
	module: {
		rules: [
			{
				test: /\.less/,
				use: [
					"style-loader",
					"css-loader",
					"less-loader",
				],
			},
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
    plugins: [
        //new CheckerPlugin()
    ],
	resolve: {
		extensions: [".tsx", ".ts", ".js", ".jsx"],
		plugins: [new TsconfigPathsPlugin({configFile: "./tsconfig.json"})],
		modules: ["node_modules"]
	},
};