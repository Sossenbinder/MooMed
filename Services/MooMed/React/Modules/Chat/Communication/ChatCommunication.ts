// Framework
import * as signalR from "@microsoft/signalr";

// Functionality
import { Network, SendMessageUiModel } from "modules/Chat/types";
import PostRequest from "helper/requests/PostRequest";

const chatRequests = {
	getMessages: "Chat/GetMessages",
};

export const getMessages = async (receiverId: number, continuationToken?: string) => {
	const request = new PostRequest<Network.GetMessagesRequest, Network.GetMessagesResponse>(chatRequests.getMessages);

	const requestData: Network.GetMessagesRequest = {
		receiverId,
	};

	if (typeof continuationToken !== "undefined") {
		requestData.continuationToken = continuationToken;
	}

	return await request.send(requestData);	
}

export const sendMessage = async (message: string, receiverId: number, timestamp: Date, signalRConnection: signalR.HubConnection): Promise<boolean> => {

	const dataModel: SendMessageUiModel = {
		message,
		receiverId,
		timestamp
	}

	var response = await signalRConnection.invoke<Promise<boolean>>("SendMessage", dataModel);

	return response;
}