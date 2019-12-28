export default class GetRequest<TResponse> {

	private m_url: string;

	constructor(url: string) {
		this.m_url = url;
	}

	public async send(): Promise<RequestPayload<TResponse>> {
		
		return fetch(this.m_url,
			{
				method: "GET",
				headers: {
					'Accept': 'application/json, text/javascript, */*',
					'Content-Type': 'application/x-www-form-urlencoded'
				},
				credentials: 'include'
			}).then(async response => {
				const responseJson = response.ok ? await response.json() : undefined;

				return {
					success: response.ok,
					errorMessage: response.statusText,
					statusCode: response.status,
					payload: responseJson,
				};
			}
		);
	}
}