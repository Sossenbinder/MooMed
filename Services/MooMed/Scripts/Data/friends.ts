﻿import * as $ from "jquery";

export let friendsObj = [];

import requestUrls from "../Helper/requestUrls.js";

export function fetch() {
    $.ajax({
        url: requestUrls.friends.getAllFriends,
        method: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            result.forEach((item) => {
                friendsObj.push(item);
            });
        }
    });
};