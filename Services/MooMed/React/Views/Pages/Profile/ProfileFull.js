"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
class ProfileFull extends React.Component {
    render() {
        return (React.createElement("div", { className: "profileFull" },
            React.createElement("div", { className: "header" },
                React.createElement("img", { className: "picture", src: this.props.account.profilePicturePath, alt: "Profile picture" }),
                React.createElement("h2", { className: "name" }, this.props.account.userName))));
    }
}
const mapStateToProps = state => {
    return {
        account: state.accountReducer.account
    };
};
exports.default = react_redux_1.connect(mapStateToProps)(ProfileFull);
//# sourceMappingURL=ProfileFull.js.map