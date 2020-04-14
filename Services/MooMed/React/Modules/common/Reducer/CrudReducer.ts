// Framework
import * as Redux from "redux";

// Functionality
import { CouldBeArray } from "data/commonTypes";
import { ensureArray } from "helper/arrayUtils";

type ReducerParams<T> = {
	actionIdentifier: string;
	key: keyof T;
}

export type ReducerState<T> = {
	data: Array<T>;
}

type ReducerAction<T> = Redux.Action & {
	payload: CouldBeArray<T>;
}

type Reducer<T> = {
	add: (data: CouldBeArray<T>) => ReducerAction<T>;
	update: (data: CouldBeArray<T>) => ReducerAction<T>;
	delete: (data: CouldBeArray<T>) => ReducerAction<T>;
	replace: (data: CouldBeArray<T>) => ReducerAction<T>;
	reducer: Redux.Reducer<ReducerState<T>, ReducerAction<T>>;
}

export const createReducer = <T>(params: ReducerParams<T>): Reducer<T> => {

	const ADD_IDENTIFIER = `${params.actionIdentifier}_ADD`;
	const UPDATE_IDENTIFIER = `${params.actionIdentifier}_UPDATE`;
	const DELETE_IDENTIFIER = `${params.actionIdentifier}_DELETE`;
	const REPLACE_IDENTIFIER = `${params.actionIdentifier}_REPLACE`;

	const initialState: ReducerState<T> = {
		data: []
	};

	const reducer = (state = initialState, action: ReducerAction<T>): ReducerState<T> => {
		switch (action.type) {
			case ADD_IDENTIFIER:
				const payloadAsArray = ensureArray(action.payload); 
				const newState = { 
					...state, 
					data: [
						...state.data, 
						...payloadAsArray
					],
				};
				return newState;
			case UPDATE_IDENTIFIER:
				return {
					...state,
				};
			case DELETE_IDENTIFIER:
				return {
					...state,
				};
			case REPLACE_IDENTIFIER:
				return {
					...state,
				};
			default:
				return state;
		}
	}

	const addAction = (data: CouldBeArray<T>): ReducerAction<T> => {
		return {
			type: ADD_IDENTIFIER,
			payload: data,
		}
	};

	const updateAction = (data: CouldBeArray<T>): ReducerAction<T> => {
		return {
			type: UPDATE_IDENTIFIER,
			payload: data,
		}
	};

	const deleteAction = (data: CouldBeArray<T>): ReducerAction<T> => {
		return {
			type: DELETE_IDENTIFIER,
			payload: data,
		}
	};

	const replaceAction = (data: CouldBeArray<T>): ReducerAction<T> => {
		return {
			type: REPLACE_IDENTIFIER,
			payload: data,
		}
	};

	return {
		add: addAction,
		update: updateAction,
		delete: deleteAction,
		replace: replaceAction,
		reducer: reducer,
	}
}