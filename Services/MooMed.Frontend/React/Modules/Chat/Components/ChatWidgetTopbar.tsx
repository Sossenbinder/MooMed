// Framework
import * as React from "react";

// Components
import Flex from "common/components/Flex";

// Functionality
import { useTranslations } from "hooks/useTranslations";

import "./Styles/ChatWidgetTopBar.less";

type Props = {
	onClick: () => void;
}

export const ChatWidgetTopBar: React.FC<Props> = ({ onClick }) => {

	const Translation = useTranslations();

	return (
		<Flex
			className={"ChatWidgetTopBar"} 
			onClick={onClick}
			direction={"Column"}
			mainAlign={"Center"}>
			<span className={"Heading"}>
				Chat
			</span>			
		</Flex>
	);
}

export default ChatWidgetTopBar;