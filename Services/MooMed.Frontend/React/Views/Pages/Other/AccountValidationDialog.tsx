// Framework
import * as React from "react";

// Components
import Flex from "Common/Components/Flex";

// Functionality
import { VoidPostRequest } from "helper/requests/PostRequest";

import "Views/Pages/Other/Styles/AccountValidation.less";

type AccountValidationData = {
	accountId: number;
	token: string;
}

type Props = {
	accountValidationData: AccountValidationData;
}

export const AccountValidationDialog: React.FC<Props> = ({ accountValidationData }) => {


	const onValidationClicked = React.useCallback(async (event) => {
		event.preventDefault();

		var request = new VoidPostRequest<AccountValidationData>("/AccountValidation/ValidateRegistration");
		await request.send(accountValidationData);
	}, []);

	return (
		<Flex className="validationContainer">
			<Flex className="validationContent">
				<h2>Account validation</h2>
				Do you want to validate your account?
				
				<input type="button" className="btn btn-primary" value="Validate" onClick={onValidationClicked}/>

				<Flex className="validationBackToLoginBtnContainer">
					<a className="btn btn-primary validationBackToLoginBtn" href="/">Back to login</a>
				</Flex>
			</Flex>
		</Flex>
	);
}

export default AccountValidationDialog;