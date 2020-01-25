"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
const popUpNotificationReducer_1 = require("data/reducers/popUpNotificationReducer");
const PopUpNotificationDefinitions_1 = require("definitions/PopUpNotificationDefinitions");
class PopUpMessageImpl extends React.Component {
    constructor(props) {
        super(props);
        this._closePopUpMessage = () => {
            this.props.deletePopUpNotification(this.props.PopUpNotification);
        };
        this.IsSelfClosing = this.props.PopUpNotification.TimeToLive > 0;
    }
    componentDidUpdate() {
        if (this.IsSelfClosing) {
            setTimeout(() => {
                this._closePopUpMessage();
            }, this.props.PopUpNotification.TimeToLive);
        }
    }
    render() {
        let errorLevelStyle;
        switch (this.props.PopUpNotification.MessageLevel) {
            case PopUpNotificationDefinitions_1.PopUpMessageLevel.Info:
                break;
            case PopUpNotificationDefinitions_1.PopUpMessageLevel.Warning:
                break;
            case PopUpNotificationDefinitions_1.PopUpMessageLevel.Error:
                break;
            default:
                errorLevelStyle = "";
                break;
        }
        return (React.createElement("div", { className: "popUpMessageContainer", style: errorLevelStyle },
            React.createElement("div", { className: "popUpMessageContentContainer" },
                React.createElement(If, { condition: this.props.PopUpNotification.Heading !== undefined },
                    React.createElement("h4", null, this.props.PopUpNotification.Heading)),
                React.createElement("p", { className: "popupMessageText" }, this.props.PopUpNotification.Message),
                React.createElement("div", { className: "popUpMessageCloseBtn", onClick: this._closePopUpMessage }))));
    }
}
const mapDispatchToProps = dispatch => {
    return {
        deletePopUpNotification: (popUpNotification) => dispatch(popUpNotificationReducer_1.deletePopUpNotification(popUpNotification))
    };
};
exports.default = react_redux_1.connect(null, mapDispatchToProps)(PopUpMessageImpl);
//# sourceMappingURL=PopUpMessage.js.map