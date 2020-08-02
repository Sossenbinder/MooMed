// Framework
import * as React from "react";
import { Route } from "react-router";

// Components
import AccountValidationDialog from "views/Pages/Other/AccountValidationDialog";
import AccountValidationSuccess from "views/Pages/Other/AccountValidationSuccess";
import AccountValidationFailure from "views/Pages/Other/AccountValidationFailure";
import CommonLandingPage from "Views/Pages/Common/CommonLandingPage";

export const OtherMain: React.FC = () => (
	<CommonLandingPage>
		<Route
			exact={true}
			path="/AccountValidation"
			render={props => {
				const params = new URLSearchParams(props.location.search);

				const accountId: number = parseInt(params.get("accountId"));
				const token: string = params.get("token");
				
				return <AccountValidationDialog
					accountValidationData={{
						accountId,
						token,
					}} />
			}}
		/>
		<Route path="/AccountValidation/Success" render={() => <AccountValidationSuccess />} />
		<Route path="/AccountValidation/Failure" render={() => <AccountValidationFailure />} />
	</CommonLandingPage>
);

export default OtherMain;