"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const ReactDOM = require("react-dom");
const react_redux_1 = require("react-redux");
const store_1 = require("data/store");
const react_router_dom_1 = require("react-router-dom");
const react_router_1 = require("react-router");
const NavBar_1 = require("views/Components/Main/NavBar");
//Route components
const AboutDialog_1 = require("views/Pages/AboutDialog");
const ProfileFull_1 = require("views/Pages/Profile/ProfileFull");
const ChatWidget_1 = require("views/Components/Friends/ChatWidget");
const PopUpMessageHolder_1 = require("views/Components/Main/PopUpMessage/PopUpMessageHolder");
class Main extends React.Component {
    render() {
        return (React.createElement("div", null,
            React.createElement(NavBar_1.default, null),
            React.createElement(PopUpMessageHolder_1.default, null),
            React.createElement("div", { className: "container-fluid contentContainer" },
                React.createElement("div", { className: "row" },
                    React.createElement("div", { className: "bodyContent col-11" },
                        React.createElement(react_router_1.Route, { path: "/about", render: props => React.createElement(AboutDialog_1.default, Object.assign({}, props)) }),
                        React.createElement(react_router_1.Route, { path: "/editProfile", render: props => React.createElement(ProfileFull_1.default, Object.assign({}, props)) })),
                    React.createElement("div", { id: "friendList", className: "col-1" })),
                React.createElement(ChatWidget_1.default, null))));
    }
}
function renderMainView() {
    ReactDOM.render(React.createElement(react_redux_1.Provider, { store: store_1.store },
        React.createElement(react_router_dom_1.BrowserRouter, null,
            React.createElement(Main, null))), document.getElementById("content"));
}
exports.default = renderMainView;
;
//# sourceMappingURL=Main.js.map