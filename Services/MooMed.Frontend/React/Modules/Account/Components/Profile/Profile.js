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
const React = require("react");
const react_redux_1 = require("react-redux");
const ProfileFull_1 = require("./ProfileFull");
const Flex_1 = require("Common/Components/Flex");
const useServices_1 = require("hooks/useServices");
require("./Styles/Profile.less");
exports.Profile = ({ accountId, account }) => {
    const [displayAccount, setDisplayAccount] = React.useState(null);
    const { AccountService } = useServices_1.default();
    const isSelf = React.useCallback(() => !accountId || account.id === accountId, [account, accountId]);
    const fetchOtherAccount = React.useCallback((accountId) => __awaiter(void 0, void 0, void 0, function* () {
        const account = yield AccountService.getAccount(accountId);
        setDisplayAccount(account);
    }), []);
    React.useEffect(() => {
        isSelf() ? setDisplayAccount(account) : fetchOtherAccount(accountId);
    }, [accountId, account]);
    return (React.createElement(Flex_1.default, { className: "Profile" },
        React.createElement(If, { condition: displayAccount != null },
            React.createElement(ProfileFull_1.default, { account: displayAccount, isSelf: isSelf() }))));
};
const mapStateToProps = (store) => {
    return {
        account: store.accountReducer.data[0],
    };
};
exports.default = react_redux_1.connect(mapStateToProps)(exports.Profile);
//# sourceMappingURL=Profile.js.map