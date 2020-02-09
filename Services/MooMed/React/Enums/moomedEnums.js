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
//# sourceMappingURL=moomedEnums.js.map