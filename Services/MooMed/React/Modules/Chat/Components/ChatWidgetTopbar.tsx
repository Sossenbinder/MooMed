// Framework
import * as React from "react";

// Components
import Flex from "Common/Components/Flex";

import "./Styles/ChatWidgetTopBar.less";

type Props = {
	onClick: () => void;
}

export const ChatWidgetTopBar: React.FC<Props> = ({ onClick }) => {
	return (
		<Flex
			className={"ChatWidgetTopBar"} 
			onClick={onClick}
			direction={"Column"}
			mainAlign={"Center"}>
			<span className={"Heading"}>
				{ Translation.Chat }
			</span>			
		</Flex>
	);
}

export default ChatWidgetTopBar;