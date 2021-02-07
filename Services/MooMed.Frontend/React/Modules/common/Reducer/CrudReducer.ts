// Functionality
import { ensureArray, removeAt } from "helper/arrayUtils";

// Types
import { CouldBeArray } from "data/commonTypes";
import { Reducer, ReducerState, MultiReducerState, ReducerAction } from "./types";

// No keys needed here. We only have one entry
type SingleReducerParams = {
	actionIdentifier: string;
}

// Keys are necessary here, so identifying updates for specific items are possible
type MultiReducerParams<T> = SingleReducerParams & {
	key: keyof T;
}

type CrudActions<TDataType, TReducerState extends ReducerState<TDataType>> = {
	addAction: (state: TReducerState, action: ReducerAction<TDataType>) => TReducerState;
	updateAction: (state: TReducerState, action: ReducerAction<TDataType>) => TReducerState;
	deleteAction: (state: TReducerState, action: ReducerAction<TDataType>) => TReducerState;
}

export const createSingleReducer = <T>(params: SingleReducerParams) => createReducerInternal<T, ReducerState<T>>(
	{
		...params,
		actions: {
			addAction: (_, action) => {
				return {
					data: { ...action.payload }
				}
			},
			updateAction: (_, action) => {
				return {
					data: { ...action.payload }
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

export const createReducer = <T>(params: MultiReducerParams<T>) => createReducerInternal<CouldBeArray<T>, MultiReducerState<T>>(
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
				const updatedData = [...state.data];

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
				const dataToDelete = [...state.data];

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

type GenericReducerParams<TDataType, TReducerState extends ReducerState<TDataType>> = SingleReducerParams & {
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

	const replaceAction = (state: TReducerState, action: ReducerAction<TDataType>): TReducerState => ({ ...state, data: action.payload });

	const reducerActionMap = new Map<string, (state: TReducerState, action: ReducerAction<TDataType>) => TReducerState>([
		[ADD_IDENTIFIER, addAction],
		[UPDATE_IDENTIFIER, updateAction],
		[DELETE_IDENTIFIER, deleteAction],
		[REPLACE_IDENTIFIER, replaceAction]
	]);

	const reducer = (state = initialState, action: ReducerAction<TDataType>): TReducerState => {
		const reducerAction = reducerActionMap.get(action.type);

		if (!reducerAction) {
			return state;
		}

		return reducerAction(state, action);
	}

	const actionGenerator = (type: string) => (payload: TDataType) => ({
		type,
		payload,
	});

	return {
		add: actionGenerator(ADD_IDENTIFIER),
		update: actionGenerator(UPDATE_IDENTIFIER),
		delete: actionGenerator(DELETE_IDENTIFIER),
		replace: actionGenerator(REPLACE_IDENTIFIER),
		reducer: reducer,
	}
}