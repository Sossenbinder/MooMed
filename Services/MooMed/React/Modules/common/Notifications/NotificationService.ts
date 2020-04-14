// Framework
import * as signalR from "@microsoft/signalr";

// Functionality
import { INotificationService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";

export default class NotificationService extends ModuleService implements INotificationService {

	private m_hubConnection: signalR.HubConnection

	public async start() {
		this.m_hubConnection = new signalR.HubConnectionBuilder()
			.withUrl("/notificationHub")
			.configureLogging(signalR.LogLevel.Debug)
			.build();

		this.m_hubConnection.on("IncomingNotification", this.onIncomingNotification);

		await this.m_hubConnection.start();

		await this.m_hubConnection.send("NewMessage", "test123", "wetwetw");
	}

	private async onIncomingNotification(notification: string){
		console.log(notification);
	}
}