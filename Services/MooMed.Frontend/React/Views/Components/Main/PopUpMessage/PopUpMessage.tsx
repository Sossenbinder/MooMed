// Framework
import * as React from "react";
import { connect } from "react-redux";
import classnames from "classnames";

//Components
import { PopUpMessageLevel, PopUpNotification } from "definitions/PopUpNotificationDefinitions";
import Flex from "common/components/Flex";
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
		"PopUpMessage": true,
	});

	const deletePopUp = React.useCallback(() => {
		deletePopUpNotification(popupNotification);
	}, [popupNotification]);

	React.useEffect(() => {
		if (popupNotification.timeToLive > 0) {
			window.setTimeout(() => {
				deletePopUp();
			}, popupNotification.timeToLive);
		}
	}, []);
 
	return (
		<Flex 
			direction="Row"
			className={classNames}>
			<Flex 
				direction="Row"
				space="Between"
				className="Content">
				<Flex
					direction="Column">
					<If condition={typeof popupNotification.heading !== "undefined"}>
						<h4 className="MessageHeading">
							{popupNotification.heading}
						</h4>
					</If>
					<p className="Message">
						{popupNotification.message}
					</p>
				</Flex>
				<Flex
					className="CloseButton"
					onClick={deletePopUp} />
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