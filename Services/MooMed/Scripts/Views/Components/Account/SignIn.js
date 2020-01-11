"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_router_1 = require("react-router");
const react_router_dom_1 = require("react-router-dom");
const RegisterDialog_1 = require("./RegisterDialog");
const LoginDialog_1 = require("./LoginDialog");
class SignIn extends React.Component {
    render() {
        return (React.createElement("div", { className: "signInContainer container" },
            React.createElement("div", { className: "signInMethodPicker row" },
                React.createElement(react_router_dom_1.NavLink, { to: "/register", className: "signInMethodBtn registerBtn col", activeClassName: "selectedBtn" }, Translation.Register),
                React.createElement(react_router_dom_1.NavLink, { to: "/login", className: "signInMethodBtn loginBtn col", activeClassName: "selectedBtn" }, Translation.Login)),
            React.createElement("hr", null),
            React.createElement("div", null,
                React.createElement(react_router_1.Switch, null,
                    React.createElement(react_router_1.Route, { exact: true, path: "/", render: props => { return React.createElement(RegisterDialog_1.default, null); } }),
                    React.createElement(react_router_1.Route, { exact: true, path: "/register", render: props => { return React.createElement(RegisterDialog_1.default, null); } }),
                    React.createElement(react_router_1.Route, { exact: true, path: "/login", render: props => { return React.createElement(LoginDialog_1.default, null); } })))));
    }
}
exports.default = SignIn;
//# sourceMappingURL=SignIn.js.map