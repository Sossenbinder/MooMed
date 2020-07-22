"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
const classnames_1 = require("classnames");
const PopUpNotificationDefinitions_1 = require("definitions/PopUpNotificationDefinitions");
const Flex_1 = require("Common/Components/Flex");
const popUpNotificationReducer_1 = require("data/reducers/popUpNotificationReducer");
require("Views/Components/Main/PopUpMessage/Styles/PopUpMessage.less");
exports.PopUpMessage = ({ popupNotification, deletePopUpNotification }) => {
    const classNames = classnames_1.default({
        "Info": popupNotification.messageLevel === PopUpNotificationDefinitions_1.PopUpMessageLevel.Info,
        "Warning": popupNotification.messageLevel === PopUpNotificationDefinitions_1.PopUpMessageLevel.Warning,
        "Error": popupNotification.messageLevel === PopUpNotificationDefinitions_1.PopUpMessageLevel.Error,
        "popUpMessageContainer": true
    });
    React.useEffect(() => {
        if (popupNotification.timeToLive > 0) {
            setTimeout(() => {
                deletePopUpNotification(popupNotification);
            }, popupNotification.timeToLive);
        }
    }, []);
    return (React.createElement(Flex_1.default, { className: classNames },
        React.createElement(Flex_1.default, { className: "popUpMessageContentContainer" },
            React.createElement(If, { condition: popupNotification.heading !== undefined },
                React.createElement("h4", null, popupNotification.heading)),
            React.createElement("p", { className: "popupMessageText" }, popupNotification.message),
            React.createElement("div", { className: "popUpMessageCloseBtn", onClick: () => deletePopUpNotification(popupNotification) }))));
};
const mapDispatchToProps = dispatch => {
    return {
        deletePopUpNotification: (popUpNotification) => dispatch(popUpNotificationReducer_1.deletePopUpNotification(popUpNotification))
    };
};
exports.default = react_redux_1.connect(null, mapDispatchToProps)(exports.PopUpMessage);
//# sourceMappingURL=PopUpMessage.js.map