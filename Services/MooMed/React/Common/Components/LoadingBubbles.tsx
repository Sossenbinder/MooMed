// Framework
import * as React from "react";

// Components
import Flex from "./Flex";

import "./Styles/LoadingBubbles.less";

type Props = {
	amountOfBubbles?: number;
}

export const LoadingBubbles: React.FC<Props> = ({ amountOfBubbles = 3 }) => {

	const bubbles = React.useMemo(() => {
		return [...Array(amountOfBubbles).keys()].map(x => <Flex className="LoadingButtonBubble" />);
	}, [ amountOfBubbles ])

	return (
		<Flex className="LoadingButtonBubbleContainer">
			{ bubbles }
		</Flex>
	);
}

export default LoadingBubbles;