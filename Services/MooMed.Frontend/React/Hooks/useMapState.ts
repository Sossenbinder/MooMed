// Framework
import * as React from "react";

export const useMapState = <TKey, TValue>() => {

	const [ map ] = React.useState<Map<TKey, TValue>>(new Map<TKey, TValue>());

	const get = React.useCallback((key: TKey): TValue | undefined => map.get(key), [map]);

	const set = React.useCallback((key: TKey, value: TValue) => map.set(key, value), [map]);

	const has = React.useCallback((key: TKey) => map.has(key), [map]);

	return {
		get,
		set,
		has,
	};
}

export default useMapState;