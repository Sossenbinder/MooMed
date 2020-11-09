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
	disabled?: boolean;
	toolTip?: string;
}

export const NavigationArrow: React.FC<Props> = ({ className, direction, onClick, color = "black", disabled = true, toolTip}) => {

	const cn = classNames({
		"NavigationArrow": true,
		"Disabled": disabled,
	}, className);

	const onArrowClick = React.useCallback(() => {
		if (!disabled) {
			onClick();
		}
	}, [onClick]);

	const getBackgroundColor = React.useCallback((verticalDirection: string) => {		
		let actualColor = color;

		if (disabled) {
			actualColor = "grey";
		}

		return `linear-gradient(to ${verticalDirection} ${direction == "Left" ? "right" : "left"}, ${backgroundGrey} 0%, ${backgroundGrey} 50%, ${actualColor} 50%, ${actualColor} 70%, ${backgroundGrey} 70%, ${backgroundGrey} 100%)`;
	}, [disabled, direction, color]);

	return (
		<Flex
			className={cn}
			direction="Column"
			onClick={onArrowClick}>
			<Flex 
				className="ArrowRender"
				style={{ background: getBackgroundColor("bottom")}}
				title={toolTip} />
			<Flex 
				className="ArrowRender"
				style={{ background: getBackgroundColor("top")}} 
				title={toolTip} />
		</Flex>
	);
}

export default NavigationArrow;