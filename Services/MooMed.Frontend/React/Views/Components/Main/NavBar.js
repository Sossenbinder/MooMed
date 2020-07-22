"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
const react_router_dom_1 = require("react-router-dom");
const Flex_1 = require("common/Components/Flex");
const SearchBar_1 = require("modules/Search/View/SearchBar");
const SmallAccountManager_1 = require("views/Components/Account/SmallAccountManager");
require("./Styles/NavBar.less");
exports.NavBar = () => (React.createElement(Flex_1.default, { className: "MainNavBar navbar navbar-expand-md navbar-dark bg-dark navbar-fixed-top", direction: "Row" },
    React.createElement(react_router_dom_1.Link, { to: "/", className: "Heading" }, "MooMed - Finance done right"),
    React.createElement(react_router_dom_1.Link, { to: "/", className: "SubItem" }, "Home"),
    React.createElement(react_router_dom_1.Link, { to: "/Stocks", className: "SubItem" }, "Stocks"),
    React.createElement(react_router_dom_1.Link, { to: "/Contact", className: "SubItem" }, "Contact"),
    React.createElement("div", { className: "searchBar nav navbar-nav ml-auto" },
        React.createElement(SearchBar_1.default, null)),
    React.createElement(SmallAccountManager_1.default, null)));
const mapStateToProps = (store) => {
    return {
        account: store.accountReducer.data[0],
    };
};
exports.default = react_redux_1.connect(mapStateToProps)(exports.NavBar);
;
//# sourceMappingURL=NavBar.js.map