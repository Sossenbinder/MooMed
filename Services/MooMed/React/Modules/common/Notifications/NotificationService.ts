// Framework
import * as signalR from "@microsoft/signalr";

// Functionality
import { INotificationService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import { SignalRNotification } from "data/notifications";
import { NotificationType } from "enums/moomedEnums";

export default class NotificationService extends ModuleService implements INotificationService {

	private _hubConnection: signalR.HubConnection

	public async start() {
		this._hubConnection = new signalR.HubConnectionBuilder()
			.withUrl("/notificationHub")
			.configureLogging(signalR.LogLevel.Debug)
			.build();

		await this._hubConnection.start();
	}

	public subscribe<T>(notificationType: NotificationType,	onNotify: (notification: SignalRNotification<T>) => void) {		
		const notificationName = this.getNotificationTypeName(notificationType);
		this._hubConnection.on(notificationName, onNotify);
	}

	public unsubscribe(notificationType: NotificationType): void {
		this._hubConnection.off(this.getNotificationTypeName(notificationType));
	}

	private getNotificationTypeName = (notificationType: NotificationType) => NotificationType[notificationType];
}