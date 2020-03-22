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