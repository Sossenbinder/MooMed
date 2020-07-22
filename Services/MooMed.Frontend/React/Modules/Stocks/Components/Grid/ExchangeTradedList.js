"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("common/components/Flex");
const ExchangeTradedListFilters_1 = require("./ExchangeTradedListFilters");
const ActionCell_1 = require("./Cells/ActionCell");
const DataGrid_1 = require("common/Components/DataGrid/DataGrid");
const useUiState_1 = require("hooks/useUiState");
const moomedEnums_1 = require("enums/moomedEnums");
require("./Styles/ExchangeTradedList.less");
exports.ExchangeTradedList = ({ exchangeTradeds }) => {
    const [filters, setFilters] = useUiState_1.default({});
    const gridConfig = {
        columns: [
            {
                headerText: "Type",
                cellconfig: {
                    customValueGenerator: x => moomedEnums_1.ExchangeTradedType[x.type],
                },
            },
            {
                headerText: "Isin",
                cellconfig: {
                    key: "isin",
                },
            },
            {
                headerText: "Product Family",
                cellconfig: {
                    key: "productFamily"
                },
            },
            {
                headerText: "Xetra Symbol",
                cellconfig: {
                    key: "xetraSymbol",
                },
            },
            {
                headerText: "Fee Percentage",
                cellconfig: {
                    key: "feePercentage",
                },
            },
            {
                headerText: "Ongoing Charges",
                cellconfig: {
                    key: "ongoingCharges",
                },
            },
            {
                headerText: "Profit Use",
                cellconfig: {
                    key: "profitUse",
                },
            },
            {
                headerText: "Replication Method",
                cellconfig: {
                    key: "replicationMethod",
                },
            },
            {
                headerText: "Fund Currency",
                cellconfig: {
                    key: "fundCurrency",
                },
            },
            {
                cellconfig: {
                    customCell: x => (React.createElement(ActionCell_1.ActionCell, { rowData: x })),
                }
            }
        ],
        idField: "isin",
        pagingInfo: {
            entriesPerPage: 20,
        }
    };
    const filteredEntries = React.useMemo(() => {
        const { type } = filters;
        let filtered = [...exchangeTradeds];
        if (type) {
            filtered = filtered.filter(x => x.type === type);
        }
        return filtered;
    }, [filters, exchangeTradeds]);
    const grid = React.useMemo(() => (React.createElement(Flex_1.Flex, { className: "ExchangeTradedList" },
        React.createElement(DataGrid_1.default, { entries: filteredEntries, gridConfig: gridConfig }))), [filteredEntries, gridConfig]);
    return (React.createElement(Flex_1.Flex, { direction: "Row" },
        React.createElement(ExchangeTradedListFilters_1.ExchangeTradedListFilters, { filters: filters, setFilters: setFilters }),
        grid));
};
exports.default = exports.ExchangeTradedList;
//# sourceMappingURL=ExchangeTradedList.js.map