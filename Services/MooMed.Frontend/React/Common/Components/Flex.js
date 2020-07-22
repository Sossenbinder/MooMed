"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const classnames_1 = require("classnames");
require("./Styles/Flex.less");
var FlexDirections;
(function (FlexDirections) {
    FlexDirections[FlexDirections["Column"] = 0] = "Column";
    FlexDirections[FlexDirections["ColumnReverse"] = 1] = "ColumnReverse";
    FlexDirections[FlexDirections["Row"] = 2] = "Row";
    FlexDirections[FlexDirections["RowReverse"] = 3] = "RowReverse";
})(FlexDirections || (FlexDirections = {}));
var FlexWrap;
(function (FlexWrap) {
    FlexWrap[FlexWrap["NoWrap"] = 0] = "NoWrap";
    FlexWrap[FlexWrap["Wrap"] = 1] = "Wrap";
    FlexWrap[FlexWrap["WrapReverse"] = 2] = "WrapReverse";
})(FlexWrap || (FlexWrap = {}));
var FlexAlign;
(function (FlexAlign) {
    FlexAlign[FlexAlign["Center"] = 0] = "Center";
    FlexAlign[FlexAlign["Start"] = 1] = "Start";
    FlexAlign[FlexAlign["End"] = 2] = "End";
})(FlexAlign || (FlexAlign = {}));
var FlexSpace;
(function (FlexSpace) {
    FlexSpace[FlexSpace["Around"] = 0] = "Around";
    FlexSpace[FlexSpace["Between"] = 1] = "Between";
})(FlexSpace || (FlexSpace = {}));
exports.Flex = ({ className, style, direction = "Row", wrap, mainAlign, crossAlign, space, children, onClick = () => { }, onScroll = () => { }, ref }) => {
    const classes = classnames_1.default({
        "flex": true,
        "flexColumn": direction === "Column",
        "flexColumnReverse": direction === "ColumnReverse",
        "flexRow": direction === "Row",
        "flexRowReverse": direction === "RowReverse",
        "flexNoWrap": wrap === "NoWrap",
        "flexWrap": wrap === "Wrap",
        "flexWrapReverse": wrap === "WrapReverse",
        "flexMainCenter": mainAlign === "Center",
        "flexMainStart": mainAlign === "Start",
        "flexMainEnd": mainAlign === "End",
        "flexAround": space === "Around",
        "flexBetween": space === "Between",
        "flexCrossCenter": crossAlign === "Center",
        "flexCrossStart": crossAlign === "Start",
        "flexCrossEnd": crossAlign === "End",
    });
    return (React.createElement("div", { className: `${classes} ${className}`, style: style, onClick: onClick, onScroll: onScroll, ref: ref }, children));
};
exports.default = exports.Flex;
//# sourceMappingURL=Flex.js.map