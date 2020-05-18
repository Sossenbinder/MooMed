// Framework
import * as signalR from "@microsoft/signalr";

// Functionality
import { SendMessageUiModel } from "modules/Chat/types";

export const sendMessage = async (message: string, receiverId: number, signalRConnection: signalR.HubConnection) => {

	const dataModel: SendMessageUiModel = {
		message,
		receiverId
	}

	await signalRConnection.send("SendMessage", dataModel);
}