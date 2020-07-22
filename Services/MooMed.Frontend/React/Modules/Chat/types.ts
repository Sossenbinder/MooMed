export type ChatRoom = {
	roomId: number;
	messages?: Array<ChatMessage>;
	messageContinuationToken?: string;
};

export type ChatMessage = {
	message: string;
	senderId: number;
	timestamp: Date;
};

export type SendMessageUiModel = {
	receiverId: number;
	message: string;
	timestamp: Date;
};

export namespace Network {

	export type GetMessagesRequest = {
		receiverId: number;
		continuationToken?: string;
	};

	export type GetMessagesResponse = {
		messages: Array<ChatMessage>;
		continuationToken: string;
	};
}