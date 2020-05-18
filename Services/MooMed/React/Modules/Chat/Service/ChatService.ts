// Framework
import * as signalR from "@microsoft/signalr";

// Functionality
import { IChatService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import { reducer as chatRoomsReducer } from "modules/Chat/Reducer/ChatRoomsReducer";
import { ChatRoom, ChatMessage } from "modules/Chat/types";
import ISignalRConnectionProvider from "modules/common/Helper/Interface/ISignalRConnectionProvider";
import * as chatCommunication from "modules/Chat/Communication/chatCommunication";
import { services } from "helper/serviceRegistry";
import { ReceivedChatMessageNotification } from "modules/Chat/types";
import { NotificationType } from "enums/moomedEnums";
import { SignalRNotification } from "data/notifications";

export default class ChatService extends ModuleService implements IChatService {

	private _signalRConnection: signalR.HubConnection;

	private _openChatHandler: (partnerId: number) => void;
	
	constructor(signalRConnectionProvider: ISignalRConnectionProvider) {
		super();

		this._signalRConnection = signalRConnectionProvider.getSignalRConnection();

		const notificationService = services.NotificationService;

		notificationService.subscribe<ReceivedChatMessageNotification>(
			NotificationType.NewChatMessage, 
			this.onChatMessageReceived);
	}

	public async start() {
		
		const friends = this.getStore().friendsReducer.data;

		const chatRooms: Array<ChatRoom> = friends.map(friend => ({ partnerId: friend.id }));

		this.dispatch(chatRoomsReducer.add(chatRooms));
	}

	public openChat = (partnerId: number) => this._openChatHandler(partnerId);

	public registerForActiveChatChange = (handler: (partnerId: number) => void) => this._openChatHandler = handler;
	
	public async sendMessage(message: string, receiverId: number): Promise<void> {
		return chatCommunication.sendMessage(message, receiverId, this._signalRConnection);
	}
	
	private onChatMessageReceived = (message: SignalRNotification<ReceivedChatMessageNotification>) => {
		
		const newMessage: ChatMessage = {
			content: message.data.message,
			senderId: message.data.senderId,
		};
		
		const chatRoom = this.getStore().chatRoomsReducer.data.find(x => x.partnerId === message.data.senderId);

		const newRoom = { ...chatRoom };

		if (typeof chatRoom.messages !== "undefined")
		{
			let messages = [ ...newRoom.messages ];
			messages.push(newMessage);
			newRoom.messages = messages;

		} else {
			newRoom.messages = [ newMessage ];
		}

		this.dispatch(chatRoomsReducer.update(newRoom));
	}
}