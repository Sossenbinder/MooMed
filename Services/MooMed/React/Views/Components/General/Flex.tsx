// Framework
import * as React from "react";
import classNames from "classnames";

import "views/Components/General/Styles/Flex.less";

enum FlexDirections {
    Column,
    Row,
    RowReverse
}

enum FlexWrap {
    NoWrap,
    Wrap,
    WrapReverse
}

enum FlexAlign {
    Center,
    Start,
    End
}

enum FlexSpace {
    Around,
    Between
}

export type FlexProps = {
    className?: string;
    styles?: React.CSSProperties;
    direction?: keyof typeof FlexDirections;
    wrap?: keyof typeof FlexWrap;
    mainAlign?: keyof typeof FlexAlign;
    crossAlign?: keyof typeof FlexAlign;
    space?: keyof typeof FlexSpace;
    children?: React.ReactNode;
    onClick?: () => void;
}

export const Flex: React.FC<FlexProps> = ({ className, styles, direction = "Row", wrap, mainAlign, crossAlign, space, children, onClick = () => {}}) => {

    const classes = classNames({
        "flex": true,
        "flexColumn": direction === "Column",
        "flexRow": direction === "Row",
        "flexRowReverse": direction === "RowReverse",
        "flexNoWrap": wrap === "NoWrap",
        "flexWrap": wrap === "Wrap",
        "flexWrapReverse": wrap === "WrapReverse",
        "flexMainCenter": mainAlign === "Center",
        "flexMainStart": mainAlign === "Start",
        "flexMainEnd": mainAlign === "End",
        "flexAround": space === "Around",
        "flexBetween": space === "Between",
        "flexCrossCenter": crossAlign === "Center",
        "flexCrossStart": crossAlign === "Start",
        "flexCrossEnd": crossAlign === "End",
    });

    return (
        <div 
            className={`${classes} ${className}`}
            style={styles}
            onClick={onClick}>
            { children }
        </div>
    )
}

export default Flex;