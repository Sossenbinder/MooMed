"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("common/Components/Flex");
const arrayUtils_1 = require("helper/arrayUtils");
require("./Styles/Dropdown.less");
var LabelPosition;
(function (LabelPosition) {
    LabelPosition[LabelPosition["Above"] = 0] = "Above";
    LabelPosition[LabelPosition["Left"] = 1] = "Left";
    LabelPosition[LabelPosition["Right"] = 2] = "Right";
    LabelPosition[LabelPosition["Below"] = 3] = "Below";
})(LabelPosition || (LabelPosition = {}));
exports.Dropdown = ({ label, labelPosition = "Above", entries, onSelect }) => {
    var _a;
    const entriesArray = arrayUtils_1.ensureArray(entries);
    const dropdownEntries = React.useMemo(() => {
        return entriesArray.map((entry, i) => (React.createElement("option", { value: entry.value, key: i }, entry.title)));
    }, [entries]);
    const dir = React.useMemo(() => {
        switch (labelPosition) {
            default:
            case "Above":
                return "Column";
            case "Below":
                return "ColumnReverse";
            case "Left":
                return "Row";
            case "Right":
                return "RowReverse";
        }
    }, [labelPosition]);
    return (React.createElement(Flex_1.default, { mainAlign: "Start", direction: dir, className: "Dropdown" },
        React.createElement(Flex_1.default, null, label),
        React.createElement(Flex_1.default, { className: "Input" },
            React.createElement("select", { value: (_a = entriesArray.find(x => x.selected)) === null || _a === void 0 ? void 0 : _a.value, onChange: event => onSelect(Number(event.currentTarget.value)) }, dropdownEntries))));
};
exports.default = exports.Dropdown;
//# sourceMappingURL=Dropdown.js.map