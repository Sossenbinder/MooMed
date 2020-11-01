// Framework
import * as React from "react";

// Components
import Flex from "common/components/Flex";

import "./Styles/ChatMessage.less";

type Props = {
	sentByMe: boolean;
	timestamp: Date;
	message: string;
}

export const ChatMessage: React.FC<Props> = ({ sentByMe, timestamp, message }) => {

	const timestampString = React.useMemo(() => {
		const hours = timestamp.getHours();
		const minutes = timestamp.getMinutes();

		return `${hours < 10 ? `0${hours}` : hours}:${minutes < 10 ? `0${minutes}` : minutes}`;
	}, [ timestamp ])

	return (
		<Flex 
			className={"ChatMessageFlex"}
			direction={"Row"}
			mainAlign={sentByMe ? "End" : "Start"}>
			<Flex 
				direction={sentByMe ? "RowReverse" : "Row"}>
				<Flex
					className={`ChatMessageContainer ${sentByMe ? "Theirs" : "Mine"}`}
					mainAlign={"End"}>
					<span
						className={"ChatMessage"}>
						{ message }
					</span>
				</Flex>
				<Flex
					mainAlign={"Start"}>
					<span className={"Timestamp"}>
						{ timestampString }
					</span>
				</Flex>
			</Flex>
		</Flex>
	);
}

export default ChatMessage;