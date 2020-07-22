"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const classnames_1 = require("classnames");
require("./Styles/Grid.less");
require("./Styles/Cell.less");
exports.Cell = ({ className, cellStyles, children, onClick, ref }) => {
    const classes = classnames_1.default({
        "grid": true,
    });
    return (React.createElement("div", { className: `${classes} ${className}`, style: cellStyles, onClick: onClick, ref: ref }, children));
};
exports.default = exports.Cell;
//# sourceMappingURL=Cell.js.map