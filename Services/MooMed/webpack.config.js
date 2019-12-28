var path = require("path");
var MiniCssExtractPlugin = require("mini-css-extract-plugin");
var WebpackShellPlugin = require("webpack-shell-plugin");
const statements = require('tsx-control-statements').default;

const TsconfigPathsPlugin = require('tsconfig-paths-webpack-plugin');

var commonWebPackCopyPlugin = new WebpackShellPlugin({
	onBuildExit: [
		'echo "Transfering files ... "',
		'robocopy wwwroot/dist "P:/Coding/Service Fabric/Data/ImageStoreShare/Store/MooMed.FabricType/MooMed.StatelessService.WebPkg.Code.1.0.0/wwwroot/dist" /MIR /njh /njs /ndl /nc /ns',
		'echo "DONE ... "',
	],
});

var scriptCfg = {
	mode: "development",
	entry: {
		bundle: "./Scripts/index.ts",
		logonBundle: "./Scripts/logonIndex.ts",
		//otherBundle: "./Scripts/otherEntryPoint.ts",
	},
	output: {
		filename: "[name].js",
		path: path.resolve(__dirname, "wwwroot/dist/")
	},
	devtool: "eval-source-map",
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
	plugins: [
		commonWebPackCopyPlugin
	],
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
	devtool: "eval-source-map",
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
		}),
		commonWebPackCopyPlugin
	],
};

module.exports = [
	scriptCfg,
	lessCfg
];