export type Operation = {
	Create,
	Update,
	Delete,
}

export type SignalRNotification<T> = {
	data: T;
	operation: Operation;
}