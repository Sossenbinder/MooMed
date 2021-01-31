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

export const register = async (email: string, userName: string, password: string, confirmPassword: string) => {

	const registerModel: Network.Register.Request = {
		Email: email,
		UserName: userName,
		Password: password,
		ConfirmPassword: confirmPassword,
	};

	const request = new PostRequest<Network.Register.Request, Network.Register.Response>(logonRequests.register);
	return await request.post(registerModel);
}

export const logOff = async () => {
	const request = new PostRequest(logonRequests.logoff);
	return await request.post();
}