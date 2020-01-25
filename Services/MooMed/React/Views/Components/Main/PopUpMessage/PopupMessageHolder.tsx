import * as React from "react";
import { connect } from "react-redux";

import { PopUpNotification } from "definitions/PopUpNotificationDefinitions";

import PopUpMessage from "./PopUpMessage";

interface IProps {
    PopUpMessages: Array<PopUpNotification>;
}

interface IState {

}

//Move partial into this and listen to posts inside
class PopUpMessageHolderImpl extends React.Component<IProps, IState> {

    render() {
        let popUpMessages = [];

        this.props.PopUpMessages.forEach(popUp => {
            popUpMessages.push(
                <PopUpMessage
					PopUpNotification={popUp}
					key={popUp.Id}
                />
            );
        });

        return (
            <div className="popUpMessageHolderContainer">
                <div className="popUpMessageHolder">
                    {popUpMessages}
                </div>
            </div>
        );
    }
}


const mapStateToProps = state => {
    return {
        PopUpMessages: state.popUpNotificationReducer.popUpNotifications
    };
}

const PopUpMessageHolder = connect(mapStateToProps)(PopUpMessageHolderImpl);

export default PopUpMessageHolder;