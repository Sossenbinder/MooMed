import * as $ from "jquery";

export default class PostRequest<TRequest, TResponse> {

	private m_url: string;

	constructor(url: string) {
		this.m_url = url;
	}


	public async send(requestData?: TRequest, attachVerificationToken: boolean = true): Promise<RequestPayload<TResponse>> {

		let params = undefined;

		if (attachVerificationToken) {
			if (requestData === undefined) {
				requestData = Object.assign({});
			}

			requestData["__RequestVerificationToken"] = $("input[name='__RequestVerificationToken']", $("#__AjaxAntiForgeryToken")).val();
		}

		if (requestData !== undefined) {
			params = Object.keys(requestData).map((key) => {
				return encodeURIComponent(key) + '=' + encodeURIComponent(requestData[key]);
			}).join('&');
		}

		return fetch(this.m_url,
			{
				method: "POST",
				body: params,
				headers: {
					'Accept': 'application/json, text/javascript, */*',
					'Content-Type': 'application/x-www-form-urlencoded'
				},
				credentials: 'include'
			}).then(async response => {
				const responseJson = await response.json();

				const responseJsonData = responseJson.data;

				return {
					success: response.ok,
					errorMessage: responseJsonData.errorMessage !== undefined ? responseJsonData.errorMessage : response.statusText,
					statusCode: response.status,
					payload: responseJson.data !== "undefined" ? responseJson.data : responseJson,
				};
			}
		);
	}
}