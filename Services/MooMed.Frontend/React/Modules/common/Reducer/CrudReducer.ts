// Functionality
import { ensureArray, removeAt } from "helper/arrayUtils";

// Types
import { CouldBeArray } from "data/commonTypes";
import { Reducer, ReducerState, MultiReducerState, ReducerAction } from "./types";

type ReducerParamsWithoutKey = {
	actionIdentifier: string;
}

type ReducerParams<T> = ReducerParamsWithoutKey & {
	key: keyof T;
}

type CrudActions<TDataType, TReducerState extends ReducerState<TDataType>> = {
	addAction: (state: TReducerState, action: ReducerAction<TDataType>) => TReducerState;
	updateAction: (state: TReducerState, action: ReducerAction<TDataType>) => TReducerState;
	deleteAction: (state: TReducerState, action: ReducerAction<TDataType>) => TReducerState;
}

export const createReducer = <T>(params: ReducerParams<T>) => createReducerInternal<CouldBeArray<T>, MultiReducerState<T>>(
	{ 
		...params,
		actions: {
			addAction: (state, action) => {
				const addPayloadAsArray = ensureArray(action.payload);

				return { 
					...state, 
					data: [...state.data].concat(addPayloadAsArray),
				};
			},
			updateAction: (state, action) => {
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
			},
			deleteAction: (state, action) => {
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
			}
		},
		initialState: {
			data: []
		},
	}
);

export const createSingleReducer = <T>(params: ReducerParamsWithoutKey) => createReducerInternal<T, ReducerState<T>>(
	{ 
		...params,
		actions: {
			addAction: (_, action) => {
				return {
					data: action.payload
				}
			},
			updateAction: (_, action) => {
				return {
					data: action.payload
				}
			},
			deleteAction: (_, __) => {
				return {
					data: undefined,
				}
			}
		},
		initialState: {
			data: undefined
		},
	}
);

type GenericReducerParams<TDataType, TReducerState extends ReducerState<TDataType>> = ReducerParamsWithoutKey & {
	actions: CrudActions<TDataType, TReducerState>;
	initialState: TReducerState;
}

const createReducerInternal = <TDataType, TReducerState extends ReducerState<TDataType>>(
	params: GenericReducerParams<TDataType, TReducerState>
): Reducer<TDataType> => {

	const actionIdentifier = params.actionIdentifier;

	const ADD_IDENTIFIER = `${actionIdentifier}_ADD`;
	const UPDATE_IDENTIFIER = `${actionIdentifier}_UPDATE`;
	const DELETE_IDENTIFIER = `${actionIdentifier}_DELETE`;
	const REPLACE_IDENTIFIER = `${actionIdentifier}_REPLACE`;

	const initialState: TReducerState = params.initialState;

	const { addAction, deleteAction, updateAction } = params.actions;

	const reducer = (state = initialState, action: ReducerAction<TDataType>): TReducerState => {
		switch (action.type) {
			case ADD_IDENTIFIER:
				return addAction(state, action);
			case UPDATE_IDENTIFIER:
				return updateAction(state, action);
			case DELETE_IDENTIFIER:
				return deleteAction(state, action);
			case REPLACE_IDENTIFIER:
				return {
					...state,
					data: action.payload,
				};
			default:
				return state;
		}
	}

	const addActionGenerator = (data: TDataType): ReducerAction<TDataType> => {
		return {
			type: ADD_IDENTIFIER,
			payload: data,
		}
	};

	const updateActionGenerator = (data: TDataType): ReducerAction<TDataType> => {
		return {
			type: UPDATE_IDENTIFIER,
			payload: data,
		}
	};

	const deleteActionGenerator = (data: TDataType): ReducerAction<TDataType> => {
		return {
			type: DELETE_IDENTIFIER,
			payload: data,
		}
	};

	const replaceActionGenerator = (data: TDataType): ReducerAction<TDataType> => {
		return {
			type: REPLACE_IDENTIFIER,
			payload: data,
		}
	};

	return {
		add: addActionGenerator,
		update: updateActionGenerator,
		delete: deleteActionGenerator,
		replace: replaceActionGenerator,
		reducer: reducer,
	}
}