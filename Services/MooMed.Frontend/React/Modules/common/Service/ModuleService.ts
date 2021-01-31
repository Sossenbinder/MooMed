// Framework
import { Action } from "redux";

// Functionality
import { store, ReduxStore } from "data/store";

// Types
import { IModuleService } from "definitions/Service";

export default abstract class ModuleService implements IModuleService {

	public abstract start(): Promise<void>;

	private Store: ReduxStore;

	protected constructor() {
		this.Store = store;
	}

	protected getStore(): ReduxStore {
		return this.Store.getState();
	}

	protected dispatch(dispatchAction: Action) {
		this.Store.dispatch(dispatchAction);
	}
}