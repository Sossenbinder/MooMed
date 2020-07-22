"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_router_1 = require("react-router");
const react_router_dom_1 = require("react-router-dom");
const RegisterDialog_1 = require("./RegisterDialog");
const LoginDialog_1 = require("./LoginDialog");
const Flex_1 = require("Common/Components/Flex");
const useTranslations_1 = require("hooks/useTranslations");
require("views/Components/Account/Styles/SignIn.less");
exports.SignIn = () => {
    const Translation = useTranslations_1.default();
    return (React.createElement(Flex_1.default, { direction: "Column", className: "signInContainer container" },
        React.createElement(Flex_1.default, { className: "signInMethodPicker row" },
            React.createElement(react_router_dom_1.NavLink, { to: "/login", className: "signInMethodBtn loginBtn col", activeClassName: "selectedBtn" }, Translation.Login),
            React.createElement(react_router_dom_1.NavLink, { to: "/register", className: "signInMethodBtn registerBtn col", activeClassName: "selectedBtn" }, Translation.Register)),
        React.createElement("hr", null),
        React.createElement(Flex_1.default, { mainAlign: "Center" },
            React.createElement(react_router_1.Switch, null,
                React.createElement(react_router_1.Route, { exact: true, path: "/", render: () => { return React.createElement(LoginDialog_1.default, null); } }),
                React.createElement(react_router_1.Route, { exact: true, path: "/register", render: () => { return React.createElement(RegisterDialog_1.default, null); } }),
                React.createElement(react_router_1.Route, { exact: true, path: "/login", render: () => { return React.createElement(LoginDialog_1.default, null); } })))));
};
exports.default = exports.SignIn;
//# sourceMappingURL=SignIn.js.map