"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
const Flex_1 = require("Views/Components/General/Flex");
exports.ProfileFull = ({ account }) => {
    return (React.createElement("div", { className: "profileFull" },
        React.createElement("div", { className: "header" },
            React.createElement("img", { className: "picture", src: account.profilePicturePath, alt: "Profile picture" }),
            React.createElement("h2", { className: "name" }, account.userName)),
        React.createElement(Flex_1.default, null)));
};
const mapStateToProps = state => {
    return {
        account: state.accountReducer.account
    };
};
exports.default = react_redux_1.connect(mapStateToProps)(exports.ProfileFull);
//# sourceMappingURL=ProfileFull.js.map