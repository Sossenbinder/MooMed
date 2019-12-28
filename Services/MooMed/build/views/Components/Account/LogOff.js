"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const PostRequest_1 = require("helper/requests/PostRequest");
const requestUrls_1 = require("helper/requestUrls");
class LogOff extends React.Component {
    constructor(props) {
        super(props);
        this._handleLogOff = () => __awaiter(this, void 0, void 0, function* () {
            const request = new PostRequest_1.default(requestUrls_1.default.logOn.logOff);
            const response = yield request.send();
            if (response.success) {
                location.href = "/";
            }
        });
    }
    render() {
        return (React.createElement("div", { onClick: this._handleLogOff, className: "logOffIoBtnContainer" },
            React.createElement("div", { className: "logOffIoBtn" })));
    }
}
exports.default = LogOff;
//# sourceMappingURL=LogOff.js.map