// Framework
import * as React from "react";
import classNames from "classnames";

import "./Styles/Grid.less";

enum GridDisplay {
	Regular,
	Inline
}

type ColumnGridPropsSelection = "gridColumnGap" | "gridRowGap" | 'gridTemplateColumns' | 'gridTemplateRows' | 'justifyContent' | 'alignContent';

type ColumnGridProps = Pick<React.CSSProperties, ColumnGridPropsSelection>;

type GridProps = {
	className?: string;
	display?: GridDisplay;

	gridProperties?: ColumnGridProps;

	children?: Array<JSX.Element>;
}

export const Grid: React.FC<GridProps> = ({ 
	className, 
	gridProperties,
	display = GridDisplay.Regular,
	children
}) => {

	const classes = classNames({
		"grid": display === GridDisplay.Regular,
		"inlineGrid": display === GridDisplay.Inline,
	});

	// const limitInjectedChildren = React.useMemo(() => children?.map(
	// 	child => React.cloneElement(child, { 
	// 		columnLimit, 
	// 		rowLimit }
	// 	)
	// ), [children]);

	return (
		<div 
			className={`${classes} ${className}`}
			style={gridProperties}>
			{ children }
		</div>
	)
}

export default Grid;