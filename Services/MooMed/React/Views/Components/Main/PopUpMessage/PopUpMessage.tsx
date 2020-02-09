import * as React from "react";
import { connect } from "react-redux";

import { deletePopUpNotification } from "data/reducers/popUpNotificationReducer";

import { PopUpMessageLevel, PopUpNotification } from "definitions/PopUpNotificationDefinitions";

import "Views/Components/Main/PopUpMessage/Styles/PopUpMessage.less";

interface IProps {
    PopUpNotification: PopUpNotification;

    deletePopUpNotification: (notification: PopUpNotification) => void;
}

interface IState {

}

class PopUpMessageImpl extends React.Component<IProps, IState> {

    IsSelfClosing: boolean;

    constructor(props) {
        super(props);

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
            case PopUpMessageLevel.Info:
                break;
            case PopUpMessageLevel.Warning:
                break;
            case PopUpMessageLevel.Error:
                break;
            default:
                errorLevelStyle = "";
                break;
        }

        return (
            <div className="popUpMessageContainer" style={errorLevelStyle}>
				<div className="popUpMessageContentContainer">
					<If condition={this.props.PopUpNotification.Heading !== undefined}>
	                    <h4>
	                        {this.props.PopUpNotification.Heading}
						</h4>
					</If>
                    <p className="popupMessageText">
                        {this.props.PopUpNotification.Message}
                    </p>
                    <div className="popUpMessageCloseBtn" onClick={this._closePopUpMessage}></div>
                </div>
            </div>
        );
    }

    _closePopUpMessage = () => {
        this.props.deletePopUpNotification(this.props.PopUpNotification);
    }
}

const mapDispatchToProps = dispatch => {
    return {
        deletePopUpNotification: (popUpNotification: PopUpNotification) => dispatch(deletePopUpNotification(popUpNotification))
    }
}

export default connect(null, mapDispatchToProps)(PopUpMessageImpl);