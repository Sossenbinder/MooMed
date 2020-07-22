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
const PostRequest_1 = require("helper/Requests/PostRequest");
function search(searchRequest) {
    return __awaiter(this, void 0, void 0, function* () {
        const request = new PostRequest_1.default("/Search/SearchForQuery");
        return yield request.post(searchRequest);
    });
}
exports.search = search;
//# sourceMappingURL=SearchCommunication.js.map