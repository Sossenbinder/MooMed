"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const requestUrls = {
    account: {
        getOwnAccount: "Account/GetOwnAccount",
        getAccount: "Account/GetAccount",
    },
    accountValidation: {
        validateRegistration: "AccountValidation/ValidateRegistration",
    },
    friends: {
        getAllFriends: "Friends/GetAllFriends",
    },
    profile: {
        getProfilePicturePath: "Profile/GetProfilePicturePath",
        uploadProfilePicture: "Profile/UploadProfilePicture",
    },
    logOn: {
        login: "/Logon/Login",
        logOff: "/Logon/LogOff",
        register: "/Logon/Register",
    },
};
exports.default = requestUrls;
//# sourceMappingURL=requestUrls.js.map