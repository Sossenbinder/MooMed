// Framework
import * as React from "react";

// Components
import Flex from "Common/Components/Flex";

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
			mainAlign={sentByMe ? "Start" : "End"}>
			<Flex 
				direction={sentByMe ? "Row" : "RowReverse"}>
				<Flex
					className={`ChatMessageContainer ${sentByMe ? "Mine" : "Theirs"}`}
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