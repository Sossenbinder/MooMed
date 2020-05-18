export type ChatRoom = {
	partnerId: number;
	messages?: Array<ChatMessage>;
}

export type ChatMessage = {
	content: string;
	senderId: number;
}

export type SendMessageUiModel = {
	receiverId: number;
	message: string;
}

export type ReceivedChatMessageNotification = {
	senderId: number;
	message: string;
}