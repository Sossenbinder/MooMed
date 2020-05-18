// Framework
import * as React from "react";
import { connect } from "react-redux";
import classnames from "classnames";

//Components
import { PopUpMessageLevel, PopUpNotification } from "definitions/PopUpNotificationDefinitions";
import Flex from "Common/Components/Flex";

// Functionality
import { deletePopUpNotification } from "data/reducers/popUpNotificationReducer";

import "Views/Components/Main/PopUpMessage/Styles/PopUpMessage.less";

type Props = {
    popupNotification: PopUpNotification;

    deletePopUpNotification: (notification: PopUpNotification) => void;
}

export const PopUpMessage: React.FC<Props> = ({ popupNotification, deletePopUpNotification }) => {

    const classNames = classnames({
        "Info": popupNotification.messageLevel === PopUpMessageLevel.Info,
        "Warning": popupNotification.messageLevel === PopUpMessageLevel.Warning,
        "Error": popupNotification.messageLevel === PopUpMessageLevel.Error,
        "popUpMessageContainer": true
    });

    React.useEffect(() => {
        if (popupNotification.timeToLive > 0) {
            setTimeout(() => {
                deletePopUpNotification(popupNotification);
            }, popupNotification.timeToLive);
        }
    }, []);
 
    return (
        <Flex className={classNames}>
            <Flex className="popUpMessageContentContainer">
                <If condition={popupNotification.heading !== undefined}>
                    <h4>
                        {popupNotification.heading}
                    </h4>
                </If>
                <p className="popupMessageText">
                    {popupNotification.message}
                </p>
                <div className="popUpMessageCloseBtn" onClick={() => deletePopUpNotification(popupNotification)}></div>
            </Flex>
        </Flex>
    );
}

const mapDispatchToProps = dispatch => {
    return {
        deletePopUpNotification: (popUpNotification: PopUpNotification) => dispatch(deletePopUpNotification(popUpNotification))
    }
}

export default connect(null, mapDispatchToProps)(PopUpMessage);