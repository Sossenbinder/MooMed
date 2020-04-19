// Framework
import { Action } from "redux";

// Functionality
import { store, ReduxStore } from "data/store";

export default abstract class ModuleService {
	
	public abstract async start();

	private Store: ReduxStore;

	protected constructor() {
		this.Store = store;
	}

	protected getStore(): ReduxStore {
		return this.Store.getState();
	}

	protected dispatch(dispatchAction: Action){
		this.Store.dispatch(dispatchAction);
	}
}