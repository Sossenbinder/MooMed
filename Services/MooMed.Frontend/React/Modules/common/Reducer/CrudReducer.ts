// Framework
import * as Redux from "redux";

// Functionality
import { CouldBeArray } from "data/commonTypes";
import { ensureArray, removeAt } from "helper/arrayUtils";

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

	const actionIdentifier = params.actionIdentifier;

	const ADD_IDENTIFIER = `${actionIdentifier}_ADD`;
	const UPDATE_IDENTIFIER = `${actionIdentifier}_UPDATE`;
	const DELETE_IDENTIFIER = `${actionIdentifier}_DELETE`;
	const REPLACE_IDENTIFIER = `${actionIdentifier}_REPLACE`;

	const initialState: ReducerState<T> = {
		data: []
	};

	const reducer = (state = initialState, action: ReducerAction<T>): ReducerState<T> => {
		switch (action.type) {
			case ADD_IDENTIFIER:
				const addPayloadAsArray = ensureArray(action.payload);

				return { 
					...state, 
					data: [
						...state.data, 
						...addPayloadAsArray
					],
				};
			case UPDATE_IDENTIFIER:

				const updatePayloadAsArray = ensureArray(action.payload);
				const updatedData = [ ...state.data ];

				updatePayloadAsArray.forEach(val => {
					const existingItemIndex = updatedData.findIndex(x => x[params.key] === val[params.key]);
					updatedData[existingItemIndex] = val;
				});

				return {
					...state,
					data: updatedData
				};
			case DELETE_IDENTIFIER:

				const deletePayloadAsArray = ensureArray(action.payload);
				const dataToDelete = [ ...state.data ];

				deletePayloadAsArray.forEach(val => {
					const indexToDelete = dataToDelete.findIndex(x => x[params.key] === val[params.key]);

					if (indexToDelete > -1) {
						removeAt(dataToDelete, indexToDelete);
					}
				});

				return {
					...state,
					data: dataToDelete
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