import AjaxRequest, { RequestMethods } from "./AjaxRequest"
import { NetworkResponse } from "./Types/NetworkDefinitions";

type VerificationTokenRequest = {
	__RequestVerificationToken?: string;
}

export default class PostRequest<TRequest, TResponse> extends AjaxRequest<TRequest, TResponse> {

	constructor(url: string) {
		super(url, RequestMethods.POST);
	}

	public async send(requestData?: TRequest, attachVerificationToken: boolean = true): Promise<NetworkResponse<TResponse>> {

		const postData: TRequest & VerificationTokenRequest = requestData ?? ({} as TRequest & VerificationTokenRequest);

		if (attachVerificationToken) {
			const tokenHolder = document.getElementsByName("__RequestVerificationToken")[0] as HTMLInputElement;

			postData.__RequestVerificationToken = tokenHolder.value;
		}

		return super.send(postData)
	}
}