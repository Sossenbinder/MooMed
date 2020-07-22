"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
const Flex_1 = require("common/components/Flex");
const ExchangeTradedList_1 = require("./Grid/ExchangeTradedList");
exports.StocksDialog = ({ exchangeTradeds }) => {
    return (React.createElement(Flex_1.default, { direction: "Column" },
        React.createElement("h2", null, "Here are some stocks to choose from:"),
        React.createElement(ExchangeTradedList_1.ExchangeTradedList, { exchangeTradeds: exchangeTradeds })));
};
const mapStateToProps = (store) => {
    return {
        exchangeTradeds: store.exchangeTradedsReducer.data,
    };
};
exports.default = react_redux_1.connect(mapStateToProps)(exports.StocksDialog);
//# sourceMappingURL=StocksDialog.js.map