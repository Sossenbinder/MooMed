/** @type {import("snowpack").SnowpackUserConfig } */
module.exports = {
	mount: {
		public: '/',
		src: '/_dist_',
	},
	plugins: [
		"snowpack-plugin-less",
		'@snowpack/plugin-typescript',
	],
	install: [
		/* ... */
	],
	installOptions: {
		/* ... */
	},
	devOptions: {
		/* ... */
	},
	buildOptions: {
		/* ... */
	},
	proxy: {
		/* ... */
	},
	alias: {
		/* ... */
	},
	scripts: {
		"run:tsc": "tsc --noEmit",
		"run:tsc::watch": "$1 --watch"
	},
};
