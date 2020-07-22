"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const LoadingBubbles_1 = require("common/Components/LoadingBubbles");
const hookUtils = require("helper/Utils/hookUtils");
require("./Styles/Controls.less");
exports.Button = (props) => {
    const [loading, setLoading] = React.useState(false);
    const onClick = (event) => __awaiter(void 0, void 0, void 0, function* () {
        event.preventDefault();
        yield hookUtils.usingBoolAsync(setLoading, props.handleClick);
    });
    return (React.createElement("button", { className: "MooMedButton" + (props.customStyles !== undefined ? (" " + props.customStyles) : ""), onClick: onClick, disabled: props.disabled },
        React.createElement(Choose, null,
            React.createElement(When, { condition: loading },
                React.createElement(LoadingBubbles_1.default, null)),
            React.createElement(When, { condition: !loading },
                React.createElement("div", null,
                    React.createElement("p", { className: "ButtonParagraph" }, props.title))))));
};
exports.default = exports.Button;
//# sourceMappingURL=Button.js.map