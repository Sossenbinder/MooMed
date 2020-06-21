import { CouldBeArray } from "data/commonTypes";

export const ensureArray = <T>(data: CouldBeArray<T>) => {
	if (data instanceof Array) {
		return data;
	} else {
		return [data];
	}
}

export const removeAt = <T>(arr: Array<T>, index: number) => {
    arr.splice(index, 1);
}

export const split = <T>(arr: Array<T>, splitSize: number) => {

	const copiedArray = [ ...arr ];

	const result: Array<Array<T>> = [];

	while (copiedArray.length > 0) {
		result.push(copiedArray.splice(0, splitSize));
	}

	return result;
}