"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const ts_enum_util_1 = require("ts-enum-util");
const Flex_1 = require("common/Components/Flex");
const Dropdown_1 = require("common/Components/General/Input/Dropdown");
const moomedEnums_1 = require("enums/moomedEnums");
require("./Styles/ExchangeTradedListFilters.less");
exports.ExchangeTradedListFilters = ({ filters, setFilters }) => {
    const exchangeTradedDropdownEntries = React.useMemo(() => {
        const exchangeTradedEnum = ts_enum_util_1.$enum(moomedEnums_1.ExchangeTradedType);
        return exchangeTradedEnum
            .getKeys()
            .map((x) => {
            const enumVal = exchangeTradedEnum.getValueOrDefault(x);
            return {
                title: x,
                value: enumVal.toString(),
                selected: (filters === null || filters === void 0 ? void 0 : filters.type) === enumVal,
            };
        });
    }, [filters]);
    return (React.createElement(Flex_1.default, { className: "ExchangeTradedListFilters", direction: "Column" },
        React.createElement(Dropdown_1.default, { label: "Type", labelPosition: "Left", entries: exchangeTradedDropdownEntries, onSelect: (x) => setFilters(Object.assign(Object.assign({}, filters), { type: x })) })));
};
exports.default = exports.ExchangeTradedListFilters;
//# sourceMappingURL=ExchangeTradedListFilters.js.map