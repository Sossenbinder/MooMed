export async function asyncForEach<T>(array: Array<T>, callback: (x: T) => Promise<void>) {
	array.forEach(async (item) => {
		await callback(item);
	});
}

export function asyncForEachParallel<T>(array: Array<T>, callback: (x: T) => Promise<void>): Promise<void[]> {
	return Promise.all(array.map(item => {
		callback(item);
	}));

}