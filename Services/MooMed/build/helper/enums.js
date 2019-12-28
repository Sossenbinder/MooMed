"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var LoginValidationResult;
(function (LoginValidationResult) {
    LoginValidationResult[LoginValidationResult["None"] = 0] = "None";
    LoginValidationResult[LoginValidationResult["Success"] = 1] = "Success";
    LoginValidationResult[LoginValidationResult["EmailNullOrEmpty"] = 2] = "EmailNullOrEmpty";
    LoginValidationResult[LoginValidationResult["PasswordNullOrEmpty"] = 3] = "PasswordNullOrEmpty";
    LoginValidationResult[LoginValidationResult["EmailNotValidated"] = 4] = "EmailNotValidated";
})(LoginValidationResult = exports.LoginValidationResult || (exports.LoginValidationResult = {}));
var AccountValidationResult;
(function (AccountValidationResult) {
    AccountValidationResult[AccountValidationResult["None"] = 0] = "None";
    AccountValidationResult[AccountValidationResult["Success"] = 1] = "Success";
    AccountValidationResult[AccountValidationResult["AlreadyValidated"] = 2] = "AlreadyValidated";
    AccountValidationResult[AccountValidationResult["ValidationGuidInvalid"] = 3] = "ValidationGuidInvalid";
    AccountValidationResult[AccountValidationResult["TokenInvalid"] = 4] = "TokenInvalid";
    AccountValidationResult[AccountValidationResult["AccountNotFound"] = 5] = "AccountNotFound";
})(AccountValidationResult = exports.AccountValidationResult || (exports.AccountValidationResult = {}));
//# sourceMappingURL=enums.js.map