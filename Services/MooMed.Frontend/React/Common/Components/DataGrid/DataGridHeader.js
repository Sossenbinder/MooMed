"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
require("./Styles/DataGridHeader.less");
exports.DataGridHeader = ({ headers }) => {
    const tableHeaders = React.useMemo(() => {
        return headers.map((h, i) => (React.createElement("th", { key: `${h}_${i}` }, h)));
    }, [headers]);
    return (React.createElement("tr", { className: "DataGridHeader" }, tableHeaders));
};
exports.default = exports.DataGridHeader;
//# sourceMappingURL=DataGridHeader.js.map