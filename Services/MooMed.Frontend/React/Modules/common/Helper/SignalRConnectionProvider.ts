// Framework
import * as signalR from "@microsoft/signalr";

// Functionality
import ISignalRConnectionProvider from "./Interface/ISignalRConnectionProvider";

export class SignalRConnectionProvider implements ISignalRConnectionProvider {

	public _hubConnection: signalR.HubConnection;

	public constructor() {
		this._hubConnection = new signalR.HubConnectionBuilder() 
			.withUrl("/signalRHub")
			.configureLogging(signalR.LogLevel.Error)
			.build();
	}

	async start(): Promise<void> {	
		await this._hubConnection.start();
	}

	getSignalRConnection(): signalR.HubConnection {
		return this._hubConnection;
	}
}

export default SignalRConnectionProvider;