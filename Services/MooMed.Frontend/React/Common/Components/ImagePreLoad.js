"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("./Flex");
const LoadingBubbles_1 = require("./LoadingBubbles");
require("./Styles/ImagePreLoad.less");
exports.ImagePreLoad = ({ imagePath, containerClassName, imageClassName, altName }) => {
    const [imageLoaded, setImageLoaded] = React.useState(false);
    return (React.createElement(Flex_1.default, { className: containerClassName },
        React.createElement(If, { condition: !imageLoaded },
            React.createElement(Flex_1.default, { className: "PlaceHolder" },
                React.createElement(LoadingBubbles_1.default, null))),
        React.createElement("img", { className: imageClassName, src: imagePath, alt: altName !== null && altName !== void 0 ? altName : "image", onLoad: () => setImageLoaded(true) })));
};
exports.default = exports.ImagePreLoad;
//# sourceMappingURL=ImagePreLoad.js.map