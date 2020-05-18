// Framework
import * as React from "react";

// Components
import Flex from "common/Components/Flex";

// Functionality
import useServices from "hooks/useServices";

import "./Styles/ChatRoomInput.less";

type Props = {
	receiverId: number;
}

export const ChatRoomInput: React.FC<Props> = ({ receiverId }) => {

	const [message, setMessage] = React.useState("");

	const { ChatService } = useServices();

	return (
		<Flex
			className={"ChatRoomInput"}
			direction={"Row"}>
			<input 
				className={"Input"}
				onInput={event => setMessage(event.currentTarget.value)}>
			</input>
			<Flex 
				className={"SendButton"}
				onClick={async () => await ChatService.sendMessage(message, receiverId)}>
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