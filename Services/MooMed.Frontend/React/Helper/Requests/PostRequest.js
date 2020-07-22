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
const AjaxRequest_1 = require("./AjaxRequest");
class PostRequest extends AjaxRequest_1.default {
    constructor(url) {
        super(url, AjaxRequest_1.RequestMethods.POST);
    }
    post(requestData, attachVerificationToken = true) {
        const _super = Object.create(null, {
            send: { get: () => super.send }
        });
        return __awaiter(this, void 0, void 0, function* () {
            const postData = requestData !== null && requestData !== void 0 ? requestData : {};
            let token = null;
            if (attachVerificationToken) {
                const tokenHolder = document.getElementsByName("__RequestVerificationToken")[0];
                token = tokenHolder.value;
            }
            return _super.send.call(this, postData, token);
        });
    }
}
exports.default = PostRequest;
class VoidPostRequest extends PostRequest {
}
exports.VoidPostRequest = VoidPostRequest;
//# sourceMappingURL=PostRequest.js.map