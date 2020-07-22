"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("common/Components/Flex");
require("./Styles/DataGridPageSelector.less");
exports.DataGridPageSelector = ({ currentPage, pageCount, setPage }) => {
    return (React.createElement(Flex_1.default, { className: "DataGridPageSelector", direction: "Row" },
        React.createElement(Flex_1.default, { className: "PageScroller", onClick: () => setPage(0) }, "≪"),
        React.createElement(Flex_1.default, { className: "PageScroller", onClick: () => setPage(currentPage - 1) }, "<"),
        currentPage + 1,
        React.createElement(Flex_1.default, { className: "PageScroller", onClick: () => setPage(currentPage + 1) }, ">"),
        React.createElement(Flex_1.default, { className: "PageScroller", onClick: () => setPage(pageCount - 1) }, "≫")));
};
exports.default = exports.DataGridPageSelector;
//# sourceMappingURL=DataGridPageSelector.js.map