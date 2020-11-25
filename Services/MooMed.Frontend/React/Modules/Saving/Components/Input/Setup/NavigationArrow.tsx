// Framework
import * as React from "react"
import classNames from "classnames";
import { Link } from "react-router-dom";

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
	navTarget: string;
	color?: string;
	disabled?: boolean;
	toolTip?: string;
}

export const NavigationArrow: React.FC<Props> = ({ className, direction, navTarget, color = "black", disabled = true, toolTip}) => {

	const cn = classNames({
		"NavigationArrow": true,
		"Disabled": disabled,
	}, className);

	const getBackgroundColor = React.useCallback((verticalDirection: string) => {		
		let actualColor = color;

		if (disabled) {
			actualColor = "grey";
		}

		return `linear-gradient(to ${verticalDirection} ${direction == "Left" ? "right" : "left"}, ${backgroundGrey} 0%, ${backgroundGrey} 50%, ${actualColor} 50%, ${actualColor} 70%, ${backgroundGrey} 70%, ${backgroundGrey} 100%)`;
	}, [disabled, direction, color]);

	return (
		<Link
			className={cn}
			to={disabled ? null : navTarget}>
			<Flex 
				className="Arrow"
				direction="Column">
				<Flex 
					className="ArrowRender"
					style={{ background: getBackgroundColor("bottom")}}
					title={disabled ? toolTip : null} />
				<Flex 
					className="ArrowRender"
					style={{ background: getBackgroundColor("top")}} 
					title={disabled ? toolTip : null} />
			</Flex>
		</Link>
	);
}

export default NavigationArrow;