"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const ICON_BASE_PATH = "/Resources/Icons/";
exports.Icon = ({ iconName, onClick, className, alt, iconExtension = "png", size = 64 }) => {
    const iconPath = React.useMemo(() => `${ICON_BASE_PATH}/${iconName}.${iconExtension}`, [iconName, iconExtension]);
    return (React.createElement("img", { className: className, alt: alt, style: { height: size, width: size }, onClick: onClick, src: iconPath }));
};
exports.default = exports.Icon;
//# sourceMappingURL=Icon.js.map