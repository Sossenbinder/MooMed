import * as React from "react";
import { connect } from "react-redux";

import { PopUpNotification } from "definitions/PopUpNotificationDefinitions";

import PopUpMessage from "./PopUpMessage";

type Props = {
    popupMessages: Array<PopUpNotification>;
}

//Move partial into this and listen to posts inside
export const PopUpMessageHolder: React.FC<Props> = ({ popupMessages }) => {

    const popupMessageRenders = React.useMemo(() => {
        return popupMessages?.map((msg, index) => 
            <PopUpMessage
                popupNotification={msg}
                key={index}
            />
        );
    }, [popupMessages]);
    
    return (
        <div className="popUpMessageHolderContainer">
            <div className="popUpMessageHolder">
                {popupMessageRenders}
            </div>
        </div>
    );
}


const mapStateToProps = state => {
    return {
        popupMessages: state.popUpNotificationReducer.popUpNotifications
    };
}

export default connect(mapStateToProps)(PopUpMessageHolder);