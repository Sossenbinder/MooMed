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
    direction?: keyof typeof FlexDirections;
    wrap?: keyof typeof FlexWrap;
    mainAlign?: keyof typeof FlexAlign;
    crossAlign?: keyof typeof FlexAlign;
    space?: keyof typeof FlexSpace;
    children: React.ReactNode;
}

export const Flex: React.FC<FlexProps> = ({ className, direction = FlexDirections.Row, wrap, mainAlign = FlexAlign.Start, crossAlign = FlexAlign.Start, space, children }) => {

    const classes = classNames({
        "flex": true,
        "flexColumn": direction === FlexDirections.Column.toString(),
        "flexRow": direction === FlexDirections.Row.toString(),
        "flexRowReverse": direction === FlexDirections.RowReverse.toString(),
        "flexNoWrap": wrap === FlexWrap.NoWrap.toString(),
        "flexWrap": wrap === FlexWrap.Wrap.toString(),
        "flexWrapReverse": wrap === FlexWrap.WrapReverse.toString(),
        "flexMainCenter": mainAlign === FlexAlign.Center.toString(),
        "flexMainStart": mainAlign === FlexAlign.Start.toString(),
        "flexMainEnd": mainAlign === FlexAlign.End.toString(),
        "flexAround": space === FlexSpace.Around.toString(),
        "flexBetween": space === FlexSpace.Between.toString(),
        "flexCrossCenter": crossAlign === FlexAlign.Center.toString(),
        "flexCrossStart": crossAlign === FlexAlign.Start.toString(),
        "flexCrossEnd": crossAlign === FlexAlign.End.toString(),
        className
    });

    return (
        <div className={classes}>
            { children }
        </div>
    )
}

export default Flex;