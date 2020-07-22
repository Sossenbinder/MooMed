"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
require("./Styles/DataGridBody.less");
exports.DataGridBody = ({ entries, cellConfigs, idField, }) => {
    const tableEntries = React.useMemo(() => {
        return entries === null || entries === void 0 ? void 0 : entries.map(entry => {
            const trKey = String(entry[idField]);
            const tableCells = cellConfigs.map((cellConfig, i) => {
                const { key, cellValueIfNull, customValueGenerator, customCell } = cellConfig;
                if (customCell) {
                    return (React.createElement("td", { key: `customCell_${i}` }, customCell(entry)));
                }
                let value = undefined;
                if (key) {
                    value = String(entry[key]);
                }
                if (!value) {
                    value = cellValueIfNull;
                }
                if (customValueGenerator) {
                    value = customValueGenerator(entry);
                }
                return (React.createElement("td", { key: `${trKey}_${i}` }, value));
            });
            return (React.createElement("tr", { className: "TableRow", key: trKey }, tableCells));
        });
    }, [entries]);
    return (React.createElement(React.Fragment, null, tableEntries));
};
exports.default = exports.DataGridBody;
//# sourceMappingURL=DataGridBody.js.map