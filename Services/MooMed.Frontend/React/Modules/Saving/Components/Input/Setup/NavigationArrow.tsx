// Framework
import * as React from "react"
import classNames from "classnames";

// Components
import Flex from "common/components/Flex"
import { backgroundGrey } from "common/Styles/Colors";

// Functionality

// Types

import "./Styles/NavigationArrow.less";

enum Direction {
	Left,
	Right,
}

type Props = {
	className?: string;
	direction: keyof typeof Direction;
	onClick(): void;
	color?: string;
}

export const NavigationArrow: React.FC<Props> = ({ className, direction, onClick, color = "black"}) => {

	const cn = classNames({
		"NavigationArrow": true,		
	}, className);

	return (
		<Flex
			className={cn}
			direction="Column"
			onClick={onClick}>
			<Flex 
				className="ArrowRender"
				style={{ background: `linear-gradient(to bottom ${direction == "Left" ? "right" : "left"}, ${backgroundGrey} 0%, ${backgroundGrey} 50%, ${color} 50%, ${color} 70%, ${backgroundGrey} 70%, ${backgroundGrey} 100%)`}} />
			<Flex 
				className="ArrowRender"
				style={{ background: `linear-gradient(to top ${direction == "Right" ? "left" : "right"}, ${backgroundGrey} 0%, ${backgroundGrey} 50%, ${color} 50%, ${color} 70%, ${backgroundGrey} 70%, ${backgroundGrey} 100%)`}} />
		</Flex>
	);
}

export default NavigationArrow;