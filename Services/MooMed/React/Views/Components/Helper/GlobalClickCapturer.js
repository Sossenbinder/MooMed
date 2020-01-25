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
const MooEvent_1 = require("helper/Events/MooEvent");
exports.ClickEvent = new MooEvent_1.default();
class GlobalClickCapturer extends React.Component {
    componentDidMount() {
        document.body.addEventListener('click', this._onClick);
    }
    componentWillUnmount() {
        document.body.removeEventListener('click', this._onClick);
    }
    _onClick(event) {
        return __awaiter(this, void 0, void 0, function* () {
            yield exports.ClickEvent.Raise(event);
        });
    }
    render() {
        return (React.createElement("div", null, this.props.children));
    }
}
exports.default = GlobalClickCapturer;
//# sourceMappingURL=GlobalClickCapturer.js.map