import AjaxRequest, { RequestMethods } from "./AjaxRequest"

export default class GetRequest<TResponse, TRequest = void> extends AjaxRequest<TRequest, TResponse> {

	constructor(url: string) {
		super(url, RequestMethods.GET);
	}	
}