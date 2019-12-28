import { asyncForEachParallel } from "Helper/Utils/asyncUtils";

export default class MooEvent<T> {

	registeredListeners: Array<(args: T) => Promise<void>>;

	constructor() {
		this.registeredListeners = [];
	}

	public Register(func: (args: T) => Promise<void>): void {
		this.registeredListeners.push(func);
	}

	public async Raise(args: T): Promise<void> {
		await Promise.all(this.registeredListeners.map(registeredListenerFunc => {
			registeredListenerFunc(args);
		}));
	}
}