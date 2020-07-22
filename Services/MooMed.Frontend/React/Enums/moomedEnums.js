"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var LoginResponseCode;
(function (LoginResponseCode) {
    LoginResponseCode[LoginResponseCode["None"] = 0] = "None";
    LoginResponseCode[LoginResponseCode["Success"] = 1] = "Success";
    LoginResponseCode[LoginResponseCode["EmailNullOrEmpty"] = 2] = "EmailNullOrEmpty";
    LoginResponseCode[LoginResponseCode["PasswordNullOrEmpty"] = 3] = "PasswordNullOrEmpty";
    LoginResponseCode[LoginResponseCode["EmailNotValidated"] = 4] = "EmailNotValidated";
    LoginResponseCode[LoginResponseCode["AccountNotFound"] = 5] = "AccountNotFound";
})(LoginResponseCode = exports.LoginResponseCode || (exports.LoginResponseCode = {}));
var AccountValidationResult;
(function (AccountValidationResult) {
    AccountValidationResult[AccountValidationResult["None"] = 0] = "None";
    AccountValidationResult[AccountValidationResult["Success"] = 1] = "Success";
    AccountValidationResult[AccountValidationResult["AlreadyValidated"] = 2] = "AlreadyValidated";
    AccountValidationResult[AccountValidationResult["ValidationGuidInvalid"] = 3] = "ValidationGuidInvalid";
    AccountValidationResult[AccountValidationResult["TokenInvalid"] = 4] = "TokenInvalid";
    AccountValidationResult[AccountValidationResult["AccountNotFound"] = 5] = "AccountNotFound";
})(AccountValidationResult = exports.AccountValidationResult || (exports.AccountValidationResult = {}));
var AccountOnlineState;
(function (AccountOnlineState) {
    AccountOnlineState[AccountOnlineState["Offline"] = 0] = "Offline";
    AccountOnlineState[AccountOnlineState["Online"] = 1] = "Online";
})(AccountOnlineState = exports.AccountOnlineState || (exports.AccountOnlineState = {}));
var NotificationType;
(function (NotificationType) {
    NotificationType[NotificationType["None"] = 0] = "None";
    NotificationType[NotificationType["FriendOnlineStateChange"] = 1] = "FriendOnlineStateChange";
    NotificationType[NotificationType["NewChatMessage"] = 2] = "NewChatMessage";
})(NotificationType = exports.NotificationType || (exports.NotificationType = {}));
var ExchangeTradedType;
(function (ExchangeTradedType) {
    ExchangeTradedType[ExchangeTradedType["Etf"] = 0] = "Etf";
    ExchangeTradedType[ExchangeTradedType["Etc"] = 1] = "Etc";
    ExchangeTradedType[ExchangeTradedType["Etn"] = 2] = "Etn";
    ExchangeTradedType[ExchangeTradedType["ActiveEtf"] = 3] = "ActiveEtf";
})(ExchangeTradedType = exports.ExchangeTradedType || (exports.ExchangeTradedType = {}));
//# sourceMappingURL=moomedEnums.js.map