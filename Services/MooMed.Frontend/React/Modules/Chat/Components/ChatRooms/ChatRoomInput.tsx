// Framework
import * as React from "react";

// Components
import Flex from "common/components/Flex";

// Functionality
import useServices from "hooks/useServices";

import "./Styles/ChatRoomInput.less";

type Props = {
	receiverId: number;
}

export const ChatRoomInput: React.FC<Props> = ({ receiverId }) => {

	const [message, setMessage] = React.useState("");
	const [isSending, setIsSending] = React.useState(false);

	const { ChatService } = useServices();

	return (
		<Flex
			className={"ChatRoomInput"}
			direction={"Row"}>
			<input 
				className={"Input"}
				onInput={event => setMessage(event.currentTarget.value)}
				disabled={isSending}>
			</input>
			<Flex 
				className={"SendButton"}
				onClick={async () => {
					setIsSending(true);
					await ChatService.sendMessage(message, receiverId);
					setIsSending(false);
					}}>
				<Flex
					className={"SendText"}
					crossAlign={"Center"}
					mainAlign={"Center"}>
					Send
				</Flex>
			</Flex>
		</Flex>
	);
}

export default ChatRoomInput;