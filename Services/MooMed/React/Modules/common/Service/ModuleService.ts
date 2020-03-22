// Framework
import { Action } from "redux";

// Functionality
import { store } from "data/store";

export default abstract class ModuleService {
	
	public abstract async start();

	protected dispatch(dispatchAction: Action){
		store.dispatch(dispatchAction);
	}
}