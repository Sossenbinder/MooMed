// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import Flex from "common/components/Flex";

// Functionality
import { PopUpNotification } from "definitions/PopUpNotificationDefinitions";

// Types
import PopUpMessage from "./PopUpMessage";

import "./Styles/PopUpMessageHolder.less";

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
		<Flex
			direction="Column"
			className="PopUpMessageHolder">
			{popupMessageRenders}
		</Flex>
	);
}


const mapStateToProps = state => {
	return {
		popupMessages: state.popUpNotificationReducer.popUpNotifications
	};
}

export default connect(mapStateToProps)(PopUpMessageHolder);