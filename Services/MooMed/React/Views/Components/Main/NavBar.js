"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
const react_router_dom_1 = require("react-router-dom");
const SearchBar_1 = require("./SearchBar/SearchBar");
const SmallAccountManager_1 = require("views/Components/Account/SmallAccountManager");
class NavBar extends React.Component {
    render() {
        return (React.createElement("div", { id: "navBar", className: "mainNavBar navbar navbar-expand-md navbar-dark bg-dark navbar-fixed-top" },
            React.createElement(react_router_dom_1.Link, { to: "/", className: "navbar-brand" }, "MooMed - Finance done right"),
            React.createElement("div", { className: "navBarDiv collapse navbar-collapse" },
                React.createElement("ul", { className: "navbar-nav mr-auto" },
                    React.createElement("li", { className: "nav-item" },
                        React.createElement(react_router_dom_1.Link, { to: "/", className: "nav-link" }, "Home")),
                    React.createElement("li", { className: "nav-item" },
                        React.createElement(react_router_dom_1.Link, { to: "/About", className: "nav-link" }, "About")),
                    React.createElement("li", { className: "nav-item" },
                        React.createElement(react_router_dom_1.Link, { to: "/Contact", className: "nav-link" }, "Contact"))),
                React.createElement("div", { className: "searchBar nav navbar-nav ml-auto" },
                    React.createElement(SearchBar_1.default, null))),
            React.createElement(SmallAccountManager_1.default, null)));
    }
}
const mapStateToProps = (store) => {
    return {
        account: store.accountReducer.account
    };
};
exports.default = react_redux_1.connect(mapStateToProps)(NavBar);
;
//# sourceMappingURL=NavBar.js.map