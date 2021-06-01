// Functionality
import * as Array from "helper/arrayUtils";

type ListenerInfo<T> = {
	listenerId: number;
	listener: (args: T) => Promise<void>;
}

export default class AsyncEvent<T> {

	registeredListeners: Array<ListenerInfo<T>>;

	listenerCounter = 1;

	constructor() {
		this.registeredListeners = [];
	}

	public Register(func: (args: T) => Promise<void>): number {

		const listenerId = this.listenerCounter;

		this.listenerCounter++;

		this.registeredListeners.push({
			listenerId,
			listener: func,
		});

		return listenerId;
	}

	public Unregister(listenerId: number): void {
		const respectiveListenerInfoIndex = this.registeredListeners.findIndex(x => x.listenerId === listenerId);

		Array.removeAt(this.registeredListeners, respectiveListenerInfoIndex);
	}

	public async Raise(args: T): Promise<void> {
		await Promise.all(this.registeredListeners.map(registeredListener => {
			registeredListener.listener(args);
		}));
	}
}