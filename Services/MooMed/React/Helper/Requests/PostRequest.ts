import AjaxRequest, { RequestMethods } from "./AjaxRequest"
import { NetworkResponse } from "./Types/NetworkDefinitions";

type VerificationTokenRequest = {
	__RequestVerificationToken?: string;
}

export default class PostRequest<TRequest, TResponse> extends AjaxRequest<TRequest, TResponse> {

	constructor(url: string) {
		super(url, RequestMethods.POST);
	}

	public async post(requestData?: TRequest, attachVerificationToken: boolean = true): Promise<NetworkResponse<TResponse>> {
        
		const postData: TRequest & VerificationTokenRequest = requestData ?? ({} as TRequest & VerificationTokenRequest);

		let token: string = null;
		if (attachVerificationToken) {
			const tokenHolder = document.getElementsByName("__RequestVerificationToken")[0] as HTMLInputElement;

			token = tokenHolder.value;
		}

		return super.send(postData, token);
	}
}

export class VoidPostRequest<TRequest> extends PostRequest<TRequest, void> {}