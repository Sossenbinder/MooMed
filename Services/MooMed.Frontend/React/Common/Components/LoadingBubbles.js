"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("./Flex");
require("./Styles/LoadingBubbles.less");
exports.LoadingBubbles = ({ amountOfBubbles = 3 }) => {
    const bubbles = React.useMemo(() => {
        return [...Array(amountOfBubbles).keys()].map((x, i) => (React.createElement(Flex_1.default, { className: "LoadingButtonBubble", key: `loadingBubble_${i}` })));
    }, [amountOfBubbles]);
    return (React.createElement(Flex_1.default, { className: "LoadingButtonBubbleContainer" }, bubbles));
};
exports.default = exports.LoadingBubbles;
//# sourceMappingURL=LoadingBubbles.js.map