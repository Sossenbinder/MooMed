// Framework
import * as React from "react";

// Components
import Flex from "common/components/Flex";
import FriendListImage from "modules/Friends/Components/FriendsList/FriendListImage";
import ChatRoomInput from "./ChatRoomInput";
import ChatMessage from "./ChatMessage";
import MaterialIcon from "common/components/MaterialIcon";
import LoadingBubbles from "common/components/LoadingBubbles";

// Functionality
import { Friend } from "modules/Friends/types";
import { ChatRoom as Room, ChatMessage as Message } from "modules/Chat/types";
import useServices from "hooks/useServices";

import "./Styles/ChatRoom.less";

type MessageNode = {
	message: Message;
	node: JSX.Element;
}

type Props = {
	friend: Friend;
	setActiveChatPartnerId: React.Dispatch<number>;

	chatRoom: Room;
}

export const ChatRoom: React.FC<Props> = ({ friend, setActiveChatPartnerId, chatRoom }) => {

	const { ChatService } = useServices();

	const messages = React.useMemo((): Array<MessageNode> => {

		if (chatRoom?.messages) {

			const sortedMessages = chatRoom.messages.sort((x, y) => x.timestamp.getTime() - y.timestamp.getTime());

			const timestampMap = new Map<string, Array<Message>>();
			sortedMessages.forEach(msg => {
				const dateString = msg.timestamp.toDateString();

				if (!timestampMap.has(dateString)) {
					timestampMap.set(dateString, []);
				}

				timestampMap.get(dateString).push(msg);
			});

			const elements = new Array<MessageNode>();

			let index = 0;
			timestampMap.forEach((messages, date) => {
				elements.push(
					{
						node: (
							<Flex
								className={"DateHeader"}
								mainAlign={"Center"}
								key={date}>
								{date}
							</Flex>
						),
						message: undefined,
					}
				);

				messages.forEach((msg, i) => {
					elements.push(
						{
							node: (
								<ChatMessage
									message={msg.message}
									sentByMe={msg.senderId != friend.id}
									timestamp={msg.timestamp}
									key={`${msg.senderId}_${index}`} />
							),
							message: null,
						}
					);

					index++;
				})
			})

			return elements;
		}

		return undefined;
	}, [chatRoom?.messages]);

	const bottomScrollDivRef = React.useRef<any>();

	const onScroll = React.useCallback(() => {

	}, []);

	React.useEffect(() => {
		async function initRoom() {
			await ChatService.initChatRoom(friend.id);
		}

		if (!chatRoom) {
			initRoom();
		}
	}, [friend.id])

	React.useEffect(() => {
		bottomScrollDivRef.current?.scrollIntoView({ behavior: "smooth" })
	}, [chatRoom?.messages])

	return (
		<Flex
			className="ChatRoomContainer"
			direction="Column">
			<If condition={!chatRoom}>
				<div className="Loading">
					<LoadingBubbles />
				</div>
			</If>
			<If condition={!!chatRoom}>
				<Flex
					className="Header"
					direction="Row"
					mainAlign="Start">
					<MaterialIcon
						style={{ fontSize: '24px' }}
						className="BackButton"
						onClick={() => setActiveChatPartnerId(0)}
						iconName="keyboard_backspace" />
					<Flex
						className="Receiver"
						direction="Row">
						<FriendListImage
							onlineState={friend.onlineState}
							profilePicturePath={friend.profilePicturePath}
							size={24} />
						<span className="Name">
							{friend.userName}
						</span>
					</Flex>
				</Flex>
				<Flex
					className="Messages"
					direction="Column">
					{messages?.map(msg => msg.node)}
					<div ref={bottomScrollDivRef}></div>
				</Flex>
				<Flex
					mainAlign="End"
					direction="Row"
					className="Input">
					<ChatRoomInput
						receiverId={friend.id} />
				</Flex>
			</If>
		</Flex>
	);
}

export default ChatRoom;