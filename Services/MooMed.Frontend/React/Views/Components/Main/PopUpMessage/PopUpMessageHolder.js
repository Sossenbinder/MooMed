"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
const PopUpMessage_1 = require("./PopUpMessage");
exports.PopUpMessageHolder = ({ popupMessages }) => {
    const popupMessageRenders = React.useMemo(() => {
        return popupMessages === null || popupMessages === void 0 ? void 0 : popupMessages.map((msg, index) => React.createElement(PopUpMessage_1.default, { popupNotification: msg, key: index }));
    }, [popupMessages]);
    return (React.createElement("div", { className: "popUpMessageHolderContainer" },
        React.createElement("div", { className: "popUpMessageHolder" }, popupMessageRenders)));
};
const mapStateToProps = state => {
    return {
        popupMessages: state.popUpNotificationReducer.popUpNotifications
    };
};
exports.default = react_redux_1.connect(mapStateToProps)(exports.PopUpMessageHolder);
//# sourceMappingURL=PopUpMessageHolder.js.map