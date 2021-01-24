// Framework
import * as React from "react";

// Functionality
import classNames from "classnames";

type GridStyles = Pick<React.CSSProperties, "fontSize">;

type Props = {
	iconName: string;

	onClick?(): void;
	className?: string;
	style?: GridStyles;
}

export const MaterialIcon: React.FC<Props> = ({ className, iconName, style, onClick }) => {

	const classes = classNames({
		"material-icons": true,
	}, className ?? "");

	return (
		<i
			onClick={() => onClick?.()}
			className={classes}
			style={style}>
			{iconName}
		</i>
	);
}

export default MaterialIcon;