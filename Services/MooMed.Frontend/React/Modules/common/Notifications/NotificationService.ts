// Framework
import * as signalR from "@microsoft/signalr";

// Functionality
import { INotificationService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import { SignalRNotification } from "data/notifications";
import { NotificationType } from "enums/moomedEnums";
import ISignalRConnectionProvider from "modules/common/Helper/Interface/ISignalRConnectionProvider";

export default class NotificationService extends ModuleService implements INotificationService {

	private _hubConnection: signalR.HubConnection

	public constructor(connectionProvider: ISignalRConnectionProvider) {
		super();

		this._hubConnection = connectionProvider.getSignalRConnection();
	}

	public async start() { }

	public subscribe<T>(notificationType: NotificationType,	onNotify: (notification: SignalRNotification<T>) => void) {

		const notificationName = this.getNotificationTypeName(notificationType);
		
		this._hubConnection.on(notificationName, onNotify);
	}

	public unsubscribe(notificationType: NotificationType): void {
		this._hubConnection.off(this.getNotificationTypeName(notificationType));
	}

	private getNotificationTypeName = (notificationType: NotificationType) => NotificationType[notificationType];
}