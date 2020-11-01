// Framework
import * as React from "react";

// Components
import Flex from "common/components/Flex";
import Icon from "common/components/Icon";
import FriendListImage from "modules/Friends/Components/FriendsList/FriendListImage";
import ChatRoomInput from "./ChatRoomInput";
import ChatMessage from "./ChatMessage";

// Functionality
import { Friend } from "modules/Friends/types";
import { ChatRoom as Room, ChatMessage as Message} from "modules/Chat/types";

import "./Styles/ChatRoom.less";

type MessageNode = {
	ref: React.RefObject<HTMLDivElement>;
	message: Message;
	node: JSX.Element;
}

type Props = {
	friend: Friend;
	setActiveChatPartnerId: React.Dispatch<number>;

	chatRoom: Room;
}

export const ChatRoom: React.FC<Props> = ({ friend, setActiveChatPartnerId, chatRoom }) => {

	const messages = React.useMemo((): Array<MessageNode> => {

		if (typeof chatRoom.messages !== "undefined") {

			const sortedMessages = chatRoom.messages.sort((x, y) => x.timestamp.getTime() - y.timestamp.getTime());

			const timestampMap = new Map<string, Array<Message>>();
			sortedMessages.forEach(msg => {
				const dateString = msg.timestamp.toDateString();

				if (!timestampMap.has(dateString)) {
					timestampMap.set(dateString, []);
				}

				timestampMap.get(dateString).push(msg);
			});

			const ref = React.createRef<HTMLDivElement>();
			
			const elements = new Array<MessageNode>();

			let index = 0;
			timestampMap.forEach((messages, date) => {
				elements.push(
					{
						ref: undefined,
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
							ref: undefined,
							node: (
								<ChatMessage 
									message={msg.message}
									sentByMe={msg.senderId != friend.id}
									timestamp={msg.timestamp}
									key={`${msg.senderId}_${index}`}/>
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
	}, [chatRoom.messages]);

	const bottomScrollDivRef = React.useRef<any>();

	const onScroll = React.useCallback(() => {

	}, []);

	React.useEffect(() => {		
		bottomScrollDivRef.current?.scrollIntoView()
	}, [])

	React.useEffect(() => {
		bottomScrollDivRef.current?.scrollIntoView({ behavior: "smooth"})
	}, [ chatRoom.messages ])

	return (
		<Flex
			className={"ChatRoomContainer"}
			direction={"Column"}>
			<Flex 
				className={"Header"}
				direction={"Row"}
				mainAlign={"Start"}>
				<Icon
					className={"BackButton"}
					iconName={"BackButton"}
					size={24}
					onClick={() => setActiveChatPartnerId(0)}/>
				<Flex
					className={"Receiver"}
					direction={"Row"}>
					<FriendListImage
						onlineState={friend.onlineState}
						profilePicturePath={friend.profilePicturePath}
						size={24} />
					<span className={"Name"}>
						{friend.userName}
					</span>
				</Flex>
			</Flex>
			<Flex
				className={"Messages"}
				direction={"Column"}>
				{messages?.map(msg => msg.node)}
				<div ref={bottomScrollDivRef}></div>
			</Flex>
			<Flex
				mainAlign={"End"}
				direction={"Row"}
				className={"Input"}>
				<ChatRoomInput 
					receiverId={friend.id}/>
			</Flex>
		</Flex>
	);
}

export default ChatRoom;