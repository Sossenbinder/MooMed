"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const ReactDOM = require("react-dom");
const react_redux_1 = require("react-redux");
const store_1 = require("data/store");
const react_router_dom_1 = require("react-router-dom");
const NavBar_1 = require("views/Components/Main/NavBar");
const PopUpMessageHolder_1 = require("views/Components/Main/PopUpMessage/PopUpMessageHolder");
const GlobalClickCapturer_1 = require("views/Components/Helper/GlobalClickCapturer");
const Flex_1 = require("Common/Components/Flex");
const MainContent_1 = require("./MainContent");
const ChatWidget_1 = require("modules/Chat/Components/ChatWidget");
require("./Styles/Home.less");
const Main = () => (React.createElement(Flex_1.default, { direction: "Column", className: "pageContainer" },
    React.createElement(NavBar_1.default, null),
    React.createElement(PopUpMessageHolder_1.default, null),
    React.createElement(MainContent_1.default, null),
    React.createElement(ChatWidget_1.default, null)));
function renderMainView() {
    ReactDOM.render(React.createElement(react_redux_1.Provider, { store: store_1.store },
        React.createElement(react_router_dom_1.BrowserRouter, null,
            React.createElement(GlobalClickCapturer_1.default, null,
                React.createElement(Main, null)))), document.getElementById("content"));
}
exports.default = renderMainView;
;
//# sourceMappingURL=Main.js.map