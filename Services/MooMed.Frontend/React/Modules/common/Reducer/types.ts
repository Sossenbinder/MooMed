// Framework
import * as Redux from "redux";

export type ReducerState<T> = {
	data: T;
}

export type MultiReducerState<T> = ReducerState<Array<T>>;

export type ReducerAction<T> = Redux.Action & {
	payload: T;
}

export type Reducer<T> = {
	add: (data: T) => ReducerAction<T>;
	update: (data: T) => ReducerAction<T>;
	delete: (data: T) => ReducerAction<T>;
	replace: (data: T) => ReducerAction<T>;
	reducer: Redux.Reducer<ReducerState<T>, ReducerAction<T>>;
}