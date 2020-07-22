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
const ModuleService_1 = require("modules/common/Service/ModuleService");
const ChatRoomsReducer_1 = require("modules/Chat/Reducer/ChatRoomsReducer");
const chatCommunication = require("modules/Chat/Communication/chatCommunication");
const serviceRegistry_1 = require("helper/serviceRegistry");
const moomedEnums_1 = require("enums/moomedEnums");
const arrayUtils_1 = require("helper/arrayUtils");
class ChatService extends ModuleService_1.default {
    constructor(signalRConnectionProvider) {
        super();
        this.openChat = (partnerId) => this._openChatHandler(partnerId);
        this.registerForActiveChatChange = (handler) => this._openChatHandler = handler;
        this.initChatRooms = () => {
            const friends = this.getStore().friendsReducer.data;
            const chatRooms = friends.map(friend => ({ roomId: friend.id }));
            chatRooms.forEach((room) => __awaiter(this, void 0, void 0, function* () {
                const messagesRequest = yield chatCommunication.getMessages(room.roomId);
                if (messagesRequest.success) {
                    const payload = messagesRequest.payload;
                    if (payload.messages != null && payload.messages.length > 0) {
                        payload.messages.forEach(x => x.timestamp = new Date(x.timestamp));
                        room.messages = payload.messages;
                        room.messageContinuationToken = payload.continuationToken;
                    }
                }
            }));
            this.dispatch(ChatRoomsReducer_1.reducer.add(chatRooms));
        };
        this.onChatMessageReceived = (message) => {
            const senderId = message.data.senderId;
            const newMessage = {
                message: message.data.message,
                senderId,
                timestamp: new Date(message.data.timestamp),
            };
            this.addMessage(newMessage, senderId);
        };
        this.addMessage = (messages, roomId) => {
            const messageArray = arrayUtils_1.ensureArray(messages);
            const chatRoom = this.getStore().chatRoomsReducer.data.find(x => x.roomId === roomId);
            const newRoom = Object.assign({}, chatRoom);
            if (typeof chatRoom.messages !== "undefined") {
                let messages = [...newRoom.messages];
                messageArray.forEach(message => messages.push(message));
                newRoom.messages = messages;
            }
            else {
                newRoom.messages = messageArray;
            }
            this.dispatch(ChatRoomsReducer_1.reducer.update(newRoom));
        };
        this._signalRConnection = signalRConnectionProvider.getSignalRConnection();
        const notificationService = serviceRegistry_1.services.NotificationService;
        notificationService.subscribe(moomedEnums_1.NotificationType.NewChatMessage, this.onChatMessageReceived);
    }
    start() {
        return __awaiter(this, void 0, void 0, function* () {
            yield this.initChatRooms();
        });
    }
    sendMessage(message, receiverId) {
        return __awaiter(this, void 0, void 0, function* () {
            const timestamp = new Date();
            var sendMessageSuccess = yield chatCommunication.sendMessage(message, receiverId, timestamp, this._signalRConnection);
            if (sendMessageSuccess) {
                const senderId = this.getStore().accountReducer.data[0].id;
                const newMessage = {
                    message,
                    senderId,
                    timestamp
                };
                this.addMessage(newMessage, receiverId);
            }
        });
    }
}
exports.default = ChatService;
//# sourceMappingURL=ChatService.js.map