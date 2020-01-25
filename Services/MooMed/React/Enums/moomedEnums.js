"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var LoginResponseCodes;
(function (LoginResponseCodes) {
    LoginResponseCodes[LoginResponseCodes["None"] = 0] = "None";
    LoginResponseCodes[LoginResponseCodes["Success"] = 1] = "Success";
    LoginResponseCodes[LoginResponseCodes["EmailNullOrEmpty"] = 2] = "EmailNullOrEmpty";
    LoginResponseCodes[LoginResponseCodes["PasswordNullOrEmpty"] = 3] = "PasswordNullOrEmpty";
    LoginResponseCodes[LoginResponseCodes["EmailNotValidated"] = 4] = "EmailNotValidated";
    LoginResponseCodes[LoginResponseCodes["AccountNotFound"] = 5] = "AccountNotFound";
})(LoginResponseCodes = exports.LoginResponseCodes || (exports.LoginResponseCodes = {}));
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