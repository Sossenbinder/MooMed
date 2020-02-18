export type NetworkResponse<TPayload> = {

    success: boolean;
    statusCode: number;

    errorMessage?: string;
    payload?: TPayload;
}