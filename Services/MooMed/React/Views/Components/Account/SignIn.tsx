import * as React from "react";
import { Route, Switch } from "react-router";
import { NavLink } from "react-router-dom";

import RegisterDialog from "./RegisterDialog";
import LoginDialog from "./LoginDialog";

import "./Styles/SignIn";

interface IProps {

}

interface IState {
}

export default class SignIn extends React.Component<IProps, IState> {
	render() {
        return (
            <div className="signInContainer container">
                <div className="signInMethodPicker row">
					<NavLink
						to="/register"
						className="signInMethodBtn registerBtn col"
						activeClassName="selectedBtn">
						{Translation.Register}
					</NavLink>
					<NavLink
						to="/login"
						className="signInMethodBtn loginBtn col"
						activeClassName="selectedBtn">
						{Translation.Login}
					</NavLink>
                </div>
                <hr />
                <div>
                    <Switch>
                        <Route exact path="/" render={() => { return <RegisterDialog />}} />
                        <Route exact path="/register" render={() => { return <RegisterDialog /> }} />
                        <Route exact path="/login" render={() => { return <LoginDialog /> }} />
                    </Switch>
                </div>
            </div>
        );
	}
}