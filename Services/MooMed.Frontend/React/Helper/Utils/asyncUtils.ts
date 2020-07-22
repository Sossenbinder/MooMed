export async function asyncForEach<T>(array: Array<T>, callback: (x: T) => Promise<void>) {
	for (let i = 0; i < array.length; ++i)
	{
		await callback(array[i]);
	}
}

export function asyncForEachParallel<T>(array: Array<T>, callback: (x: T) => Promise<void>): Promise<void[]> {
	return Promise.all(array.map(item => {
		callback(item);
	}));

}