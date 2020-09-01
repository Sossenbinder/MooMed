// Framework
import * as React from "react";
import classNames from "classnames";

import "./Styles/Grid.less";
import "./Styles/Cell.less";

type CellStyleKeys = 'gridColumn' | 'gridRow' | 'gridArea';

type CellStyles = Pick<React.CSSProperties,  CellStyleKeys>;

type CellProps = {
	className?: string;
	cellStyles?: CellStyles;

	children?: React.ReactNode;
	onClick?: () => void;
	ref?: React.Ref<any>;
}

export const Cell: React.FC<CellProps> = ({ 
	className,
	cellStyles,
	children,
	onClick,
	ref
}) => {

	const classes = classNames({
		"grid": true,
	});

	return (
		<div 
			className={`${classes} ${typeof className !== "undefined" ? className : ""}`}
			style={cellStyles}
			onClick={onClick}
			ref={ref}>
			{ children }
		</div>
	)
}

export default Cell;