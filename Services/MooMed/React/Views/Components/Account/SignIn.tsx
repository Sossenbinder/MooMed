// Framework
import * as React from "react";
import { Route, Switch } from "react-router";
import { NavLink } from "react-router-dom";

// Components
import RegisterDialog from "./RegisterDialog";
import LoginDialog from "./LoginDialog";
import Flex from "Common/Components/Flex";

import "views/Components/Account/Styles/SignIn.less";

export const SignIn: React.FC = () => 
	<Flex 
		direction={"Column"}
		className={"signInContainer container"}>
		<Flex className={"signInMethodPicker row"}>				
			<NavLink
				to={"/login"}
				className={"signInMethodBtn loginBtn col"}
				activeClassName={"selectedBtn"}>
				{Translation.Login}
			</NavLink>
			<NavLink
				to={"/register"}
				className={"signInMethodBtn registerBtn col"}
				activeClassName={"selectedBtn"}>
				{Translation.Register}
			</NavLink>
		</Flex>
		<hr />
		<Flex
			mainAlign={"Center"}>
			<Switch>
				<Route 
					exact={true} 
					path={"/"} 
					render={() => { return <LoginDialog />}} />
				<Route 
					exact={true} 
					path={"/register"} 
					render={() => { return <RegisterDialog /> }} />
				<Route 
					exact={true} 
					path={"/login"} 
					render={() => { return <LoginDialog /> }} />
			</Switch>
		</Flex>
	</Flex>;

export default SignIn;