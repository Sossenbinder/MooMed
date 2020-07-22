"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const classnames_1 = require("classnames");
require("./Styles/Grid.less");
var GridDisplay;
(function (GridDisplay) {
    GridDisplay[GridDisplay["Regular"] = 0] = "Regular";
    GridDisplay[GridDisplay["Inline"] = 1] = "Inline";
})(GridDisplay || (GridDisplay = {}));
exports.Grid = ({ className, gridProperties, display = GridDisplay.Regular, children }) => {
    const classes = classnames_1.default({
        "grid": display === GridDisplay.Regular,
        "inlineGrid": display === GridDisplay.Inline,
    });
    return (React.createElement("div", { className: `${classes} ${className}`, style: gridProperties }, children));
};
exports.default = exports.Grid;
//# sourceMappingURL=Grid.js.map