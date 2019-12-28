interface RequestPayload<TResponse> {
	success: boolean;
	statusCode: number;
	payload: TResponse;
	errorMessage: string;
}