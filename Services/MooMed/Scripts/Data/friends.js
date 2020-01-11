"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const $ = require("jquery");
exports.friendsObj = [];
const requestUrls_js_1 = require("../Helper/requestUrls.js");
function fetch() {
    $.ajax({
        url: requestUrls_js_1.default.friends.getAllFriends,
        method: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            result.forEach((item) => {
                exports.friendsObj.push(item);
            });
        }
    });
}
exports.fetch = fetch;
;
//# sourceMappingURL=friends.js.map