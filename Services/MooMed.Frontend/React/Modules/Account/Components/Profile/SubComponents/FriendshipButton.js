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
const Flex_1 = require("Common/Components/Flex");
const useServices_1 = require("hooks/useServices");
const translationHelper_1 = require("helper/translationHelper");
const useTranslations_1 = require("hooks/useTranslations");
exports.FriendshipButton = ({ targetAccountId, friends }) => {
    const { FriendsService } = useServices_1.default();
    const Translation = useTranslations_1.useTranslations();
    const friend = React.useMemo(() => {
        const possibleFriends = friends.filter(x => x.id === targetAccountId);
        if (possibleFriends.length === 0) {
            return undefined;
        }
        else {
            return possibleFriends[0];
        }
    }, [targetAccountId, friends]);
    const isAlreadyFriend = React.useMemo(() => typeof friend !== "undefined", [targetAccountId, friends]);
    return (React.createElement(Flex_1.default, null,
        React.createElement(If, { condition: !isAlreadyFriend },
            React.createElement("button", { onClick: () => __awaiter(void 0, void 0, void 0, function* () { return yield FriendsService.addFriend(targetAccountId); }) }, Translation.AddAsFriend)),
        React.createElement(If, { condition: isAlreadyFriend }, translationHelper_1.formatTranslation(Translation.AlreadyFriendsWith, friend.userName))));
};
const mapStateToProps = (store) => {
    return {
        friends: store.friendsReducer.data,
    };
};
exports.default = react_redux_1.connect(mapStateToProps)(exports.FriendshipButton);
//# sourceMappingURL=FriendshipButton.js.map