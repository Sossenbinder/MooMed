// Functionality
import { Network } from "modules/Logon/types";
import PostRequest from "helper/requests/PostRequest";

const logonRequests = {
	login: "/Logon/Login",
	register: "/Logon/Register",
	logoff: "/Logon/LogOff"
};

export const login = async (email: string, password: string, rememberMe: boolean) => {

	const loginModel: Network.Login.Request = {
		Email: email,
		Password: password,
		RememberMe: rememberMe,
	};

	const request = new PostRequest<Network.Login.Request, Network.Login.Response>(logonRequests.login);
	return await request.post(loginModel);
}