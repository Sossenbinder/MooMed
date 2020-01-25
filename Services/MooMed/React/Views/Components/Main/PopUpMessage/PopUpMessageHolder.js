"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
const PopUpMessage_1 = require("./PopUpMessage");
//Move partial into this and listen to posts inside
class PopUpMessageHolderImpl extends React.Component {
    render() {
        let popUpMessages = [];
        this.props.PopUpMessages.forEach(popUp => {
            popUpMessages.push(React.createElement(PopUpMessage_1.default, { PopUpNotification: popUp, key: popUp.Id }));
        });
        return (React.createElement("div", { className: "popUpMessageHolderContainer" },
            React.createElement("div", { className: "popUpMessageHolder" }, popUpMessages)));
    }
}
const mapStateToProps = state => {
    return {
        PopUpMessages: state.popUpNotificationReducer.popUpNotifications
    };
};
const PopUpMessageHolder = react_redux_1.connect(mapStateToProps)(PopUpMessageHolderImpl);
exports.default = PopUpMessageHolder;
//# sourceMappingURL=PopupMessageHolder.js.map