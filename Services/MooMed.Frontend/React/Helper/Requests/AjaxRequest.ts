import { NetworkResponse } from "./Types/NetworkDefinitions";

export enum RequestMethods {
	GET = "GET",
	POST = "POST"
}

export default class AjaxRequest<TRequest, TResponse> {

	private m_url: string;

	private m_requestMethod: string;

	constructor(
		url: string, 
		requestMethod: RequestMethods = RequestMethods.POST) {
		this.m_url = url;
		this.m_requestMethod = requestMethod;
	}

	public async send(requestData?: TRequest, verificationToken?: string): Promise<NetworkResponse<TResponse>> {

		const requestInit: RequestInit = {
			method: this.m_requestMethod,
			cache: "no-cache",
			headers: {
				'Accept': 'application/json, text/javascript, */*',
				'Content-Type': 'application/json'
			},
			credentials: 'include'
		}

		if (verificationToken) {
			requestInit.headers["AntiForgery"] = verificationToken;
		}

		if (this.m_requestMethod === RequestMethods.POST && typeof requestData !== "undefined") {
			requestInit.body = JSON.stringify(requestData);
		}

		const response = await fetch(this.m_url, requestInit);

		const jsonResponse = response.ok ? await response.json() : undefined;

		const payload: TResponse = typeof jsonResponse !== "undefined" ? jsonResponse?.data as TResponse : undefined;
		
		return {
			success: jsonResponse.success,
			payload,
		};
	}
}