"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const DataGridHeader_1 = require("./DataGridHeader");
const DataGridBody_1 = require("./DataGridBody");
const Flex_1 = require("common/Components/Flex");
const DataGridPageSelector_1 = require("./DataGridPageSelector");
const arrayUtils_1 = require("helper/arrayUtils");
exports.DataGrid = ({ gridConfig, entries }) => {
    const [page, setPage] = React.useState(0);
    const headerTexts = React.useMemo(() => gridConfig.columns.map(x => x.headerText), [gridConfig.columns]);
    const cellConfigs = React.useMemo(() => gridConfig.columns.map(x => x.cellconfig), [gridConfig.columns]);
    const preparedEntries = React.useMemo(() => {
        if (!gridConfig.pagingInfo) {
            return [entries];
        }
        return arrayUtils_1.split(entries, gridConfig.pagingInfo.entriesPerPage);
    }, [entries]);
    const pageCount = React.useMemo(() => preparedEntries.length, [preparedEntries]);
    React.useEffect(() => setPage(0), [entries]);
    return (React.createElement(Flex_1.default, null,
        React.createElement("table", null,
            React.createElement("thead", null,
                React.createElement(DataGridHeader_1.default, { headers: headerTexts })),
            React.createElement("tbody", null,
                React.createElement(DataGridBody_1.default, { entries: preparedEntries[page], cellConfigs: cellConfigs, idField: gridConfig.idField }))),
        React.createElement(If, { condition: typeof gridConfig.pagingInfo !== "undefined" },
            React.createElement(DataGridPageSelector_1.default, { currentPage: page, pageCount: pageCount, setPage: setPage }))));
};
exports.default = exports.DataGrid;
//# sourceMappingURL=DataGrid.js.map