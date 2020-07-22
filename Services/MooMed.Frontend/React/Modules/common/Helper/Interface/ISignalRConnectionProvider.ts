// Framework
import * as signalR from "@microsoft/signalr";

export interface ISignalRConnectionProvider {
	start(): Promise<void>;
	getSignalRConnection(): signalR.HubConnection;
}

export default ISignalRConnectionProvider;