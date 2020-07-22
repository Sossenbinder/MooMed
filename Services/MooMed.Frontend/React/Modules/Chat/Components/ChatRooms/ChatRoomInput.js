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
const Flex_1 = require("common/Components/Flex");
const useServices_1 = require("hooks/useServices");
require("./Styles/ChatRoomInput.less");
exports.ChatRoomInput = ({ receiverId }) => {
    const [message, setMessage] = React.useState("");
    const [isSending, setIsSending] = React.useState(false);
    const { ChatService } = useServices_1.default();
    return (React.createElement(Flex_1.default, { className: "ChatRoomInput", direction: "Row" },
        React.createElement("input", { className: "Input", onInput: event => setMessage(event.currentTarget.value), disabled: isSending }),
        React.createElement(Flex_1.default, { className: "SendButton", onClick: () => __awaiter(void 0, void 0, void 0, function* () {
                setIsSending(true);
                yield ChatService.sendMessage(message, receiverId);
                setIsSending(false);
            }) },
            React.createElement(Flex_1.default, { className: "SendText", crossAlign: "Center", mainAlign: "Center" }, "Send"))));
};
exports.default = exports.ChatRoomInput;
//# sourceMappingURL=ChatRoomInput.js.map