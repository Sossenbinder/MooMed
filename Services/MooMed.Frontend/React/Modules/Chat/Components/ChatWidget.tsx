// Framework
import * as React from "react";
import { connect } from "react-redux";
import classNames from "classnames";

// Components
import Flex from "common/components/Flex";
import ChatWidgetTopBar from "./ChatWidgetTopBar";
import ChatWidgetContent from "./ChatWidgetContent";

// Functionality
import useServices from "hooks/useServices";
import { ReduxStore } from "Data/store";
import { Friend } from "modules/Friends/types";
import { ChatRoom } from "modules/Chat/types";

import "./Styles/ChatWidget.less";

type Props = {
	friends: Array<Friend>;

	chatRooms: Array<ChatRoom>;
}

export const ChatWidget: React.FC<Props> = ({ friends, chatRooms }) => {

	const { ChatService } = useServices();

	const [maximized, setMaximized] = React.useState(false);
	const [initiallyMinimized, setInitiallyRendered] = React.useState(false);
	const [activeChatPartnerId, setActiveChatPartnerId] = React.useState(0);

	React.useEffect(() => {
		ChatService.registerForActiveChatChange(partnerId => {
			if (!maximized) {
				setMaximized(true);
			}

			setActiveChatPartnerId(partnerId);
		});
	}, []);

	const classes = classNames({
		"ChatWidget": true,
		"maximized": maximized,
		"minimized": !maximized && initiallyMinimized,
	});

	return (
		<Flex 
			className={classes}
			direction={"Column"}>
			<ChatWidgetTopBar
				onClick={() => {
					setMaximized(!maximized);
					setInitiallyRendered(true);
				}} />
			<If condition={maximized}>
				<ChatWidgetContent 
					activeChatId={activeChatPartnerId}
					friends={friends}
					setActiveChatPartnerId={setActiveChatPartnerId}
					chatRooms={chatRooms}/>
			</If>
		</Flex>
	);
}

const mapStateToProps = (state: ReduxStore) => ({
	friends: state.friendsReducer.data,
	chatRooms: state.chatRoomsReducer.data,
});

export default connect(mapStateToProps)(ChatWidget);