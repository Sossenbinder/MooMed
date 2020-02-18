import { NetworkResponse } from "./Types/NetworkDefinitions";

export enum RequestMethods {
    GET = "GET",
    POST = "POST"
}

export default abstract class AjaxRequest<TRequest, TResponse> {

    private m_url: string;

    private m_requestMethod: string;

    constructor(
        url: string, 
        requestMethod: RequestMethods = RequestMethods.POST) {
        this.m_url = url;
        this.m_requestMethod = requestMethod;
    }

    public async send(): Promise<NetworkResponse<TResponse>> {

        const response = await fetch(this.m_url,
			{
				method: this.m_requestMethod,
				headers: {
					'Accept': 'application/json, text/javascript, */*',
					'Content-Type': 'application/x-www-form-urlencoded'
				},
				credentials: 'include'
            }
        );
        
        return {
            success: response.ok,
            errorMessage: response.statusText,
            statusCode: response.status,
            payload: response.ok ? <TResponse>JSON.parse(await response.json()) : undefined,
        };
    }
}