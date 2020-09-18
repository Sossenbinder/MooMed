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
import { NotificationType } from "enums/moomedEnums";
import { SignalRNotification } from "data/notifications";
import { CouldBeArray } from "data/commonTypes";
import { ensureArray } from "helper/arrayUtils";

export default class ChatService extends ModuleService implements IChatService {

	private _signalRConnection: signalR.HubConnection;

	private _openChatHandler: (partnerId: number) => void;
	
	constructor(signalRConnectionProvider: ISignalRConnectionProvider) {
		super();

		this._signalRConnection = signalRConnectionProvider.getSignalRConnection();

		const notificationService = services.NotificationService;

		notificationService.subscribe<ChatMessage>(
			NotificationType.NewChatMessage, 
			this.onChatMessageReceived);
	}

	public async start() {
		this.initChatRooms();
	}

	public openChat = (partnerId: number) => this._openChatHandler(partnerId);

	public registerForActiveChatChange = (handler: (partnerId: number) => void) => this._openChatHandler = handler;
	
	public async sendMessage(message: string, receiverId: number): Promise<void> {
		const timestamp = new Date();

		var sendMessageSuccess = await chatCommunication.sendMessage(message, receiverId, timestamp, this._signalRConnection);

		if (sendMessageSuccess) {
			const senderId = this.getStore().accountReducer.data[0].id;

			const newMessage: ChatMessage = {
				message,
				senderId,
				timestamp
			};

			this.addMessage(newMessage, receiverId);
		}
	}

	private initChatRooms = () => {

		const friends = this.getStore().friendsReducer.data;
		const chatRooms: Array<ChatRoom> = friends.map(friend => ({ roomId: friend.id }));

		chatRooms.forEach(async room => {
			const messagesRequest = await chatCommunication.getMessages(room.roomId);

			if (messagesRequest.success)
			{
				const payload = messagesRequest.payload;

				if (payload.messages != null && payload.messages.length > 0)
				{
					payload.messages.forEach(x => x.timestamp = new Date(x.timestamp));

					room.messages = payload.messages;
					room.messageContinuationToken = payload.continuationToken;
				}
			}
		});

		this.dispatch(chatRoomsReducer.add(chatRooms));
	}
	
	private onChatMessageReceived = (message: SignalRNotification<ChatMessage>) => {
		const senderId = message.data.senderId;
		const newMessage: ChatMessage = {
			message: message.data.message,
			senderId,
			timestamp: new Date(message.data.timestamp),
		};
		
		this.addMessage(newMessage, senderId);
	}

	private addMessage = (messages: CouldBeArray<ChatMessage>, roomId: number) => {

		const messageArray = ensureArray(messages);

		const chatRoom = this.getStore().chatRoomsReducer.data.find(x => x.roomId === roomId);

		const newRoom = { ...chatRoom };

		if (typeof chatRoom.messages !== "undefined")
		{
			let messages = [ ...newRoom.messages ];

			messageArray.forEach(message => messages.push(message));
			
			newRoom.messages = messages;

		} else {
			newRoom.messages = messageArray;
		}

		this.dispatch(chatRoomsReducer.update(newRoom));
	}
}