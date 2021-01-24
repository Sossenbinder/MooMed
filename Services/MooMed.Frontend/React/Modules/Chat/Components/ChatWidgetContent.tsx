// Framework
import * as React from "react";

// Components
import Flex from "common/components/Flex";
import ChatRoom from "./ChatRooms/ChatRoom";
import ChatRoomPreview from "./ChatRooms/ChatRoomPreview";

// Functionality
import { Friend } from "modules/Friends/types";
import { ChatRoom as Room } from "modules/Chat/types";

import "./Styles/ChatWidgetContent.less";

type Props = {
	activeChatId: number;
	friends: Array<Friend>;
	setActiveChatPartnerId: React.Dispatch<number>;

	chatRooms: Array<Room>;
}

export const ChatWidgetContent: React.FC<Props> = ({ activeChatId, friends, setActiveChatPartnerId, chatRooms }) => {

	const chatRoomPreviews = React.useMemo(() => {
		return friends.map(friend =>
			<ChatRoomPreview
				friend={friend}
				onClick={setActiveChatPartnerId}
				key={friend.id} />)
	}, [friends])

	return (
		<Flex
			className={"ChatWidgetContent"}
			direction={"Column"}
			space={"Between"}>
			<If condition={activeChatId !== 0}>
				<ChatRoom
					friend={friends.find(x => x.id === activeChatId)}
					setActiveChatPartnerId={setActiveChatPartnerId}
					chatRoom={chatRooms.find(x => x.roomId == activeChatId)} />
			</If>
			<If condition={activeChatId === 0}>
				<Flex
					direction={"Column"}
					className={"ChatRoomPreviewContainer"}>
					{chatRoomPreviews}
				</Flex>
			</If>
		</Flex>
	);
}

export default ChatWidgetContent;