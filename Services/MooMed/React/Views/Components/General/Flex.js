"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const classnames_1 = require("classnames");
require("views/Components/General/Styles/Flex.less");
var Directions;
(function (Directions) {
    Directions[Directions["Column"] = 0] = "Column";
    Directions[Directions["Row"] = 1] = "Row";
    Directions[Directions["RowReverse"] = 2] = "RowReverse";
})(Directions || (Directions = {}));
exports.Flex = ({ direction, children }) => {
    const classes = classnames_1.default({
        "flex": true,
        "flexColumn": direction === Directions.Column.toString(),
        "flexRow": direction === Directions.Row.toString(),
        "flexRowReverse": direction === Directions.RowReverse.toString(),
    });
    return (React.createElement("div", { className: classes }, children));
};
exports.default = exports.Flex;
//# sourceMappingURL=Flex.js.map