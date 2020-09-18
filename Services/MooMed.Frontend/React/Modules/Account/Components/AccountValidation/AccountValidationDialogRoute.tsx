// Framework
import * as React from "react"
import { Route } from "react-router";

// Components
import AccountValidationDialog from "./AccountValidationDialog";

export const AccountValidationDialogRoute: React.FC = () => {
	return (
		<>		
			<Route
				exact={true}
				path="/AccountValidation"
				render={props => {
					const params = new URLSearchParams(props.location.search);

					const accountId: number = parseInt(params.get("accountId"));
					const token: string = params.get("token");
					
					return <AccountValidationDialog
						accountId={accountId}
						token={token} />
				}}
			/>
		</>
	);
}

export default AccountValidationDialogRoute