"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const AjaxRequest_1 = require("./AjaxRequest");
class GetRequest extends AjaxRequest_1.default {
    constructor(url) {
        super(url, AjaxRequest_1.RequestMethods.GET);
    }
}
exports.default = GetRequest;
//# sourceMappingURL=GetRequest.js.map