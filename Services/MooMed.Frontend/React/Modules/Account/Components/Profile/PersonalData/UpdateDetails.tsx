// Framework
import * as React from "react"

// Components
import Flex from "common/components/Flex";
import Grid from "common/components/Grid/Grid";
import Cell from "common/Components/Grid/Cell";
import LabelledErrorAttachedTextInput from "common/components/General/Input/ErrorAttached/LabelledErrorAttachedTextInput";
import Button from "common/Components/General/Input/Buttons/Button";

// Functionality
import useFormState from "hooks/useFormState";
import useServices from "hooks/useServices";

// Types
import { Account } from "modules/Account/types";
import { FormData } from "definitions/Forms";

import "./Styles/UpdateDetails.less";

type Props = {
	account: Account;
}

export const UpdateDetails: React.FC<Props> = ({ account }) => {

	const [email, setEmail] = useFormState<string>(account.email);
	const [userName, setUserName] = useFormState<string>(account.userName);
	const [changesMade, setChangesMade] = React.useState<boolean>(false);

	const { AccountService } = useServices();

	const onEdit = React.useCallback((cb: React.Dispatch<FormData<string>>, value: FormData<string>) => {
		cb(value);		
		if (!changesMade) {
			setChangesMade(true);
		}
	}, [changesMade]);

	return (
		<Grid
			gridProperties={{
				gridTemplateColumns: "repeat(1fr)",
				gridRowGap: "10px",
				gridTemplateRows: "1fr 2fr",
			}}>
			<Cell
				cellStyles={{ gridColumn: "1 / 3" }}>
				<h2>
					<u>
						Personal Data
					</u>
				</h2>
			</Cell>
			<Cell>
				<LabelledErrorAttachedTextInput
					className="LabelledInput"
					name="Email"
					labelPosition="Left"
					formData={email}
					setFormData={data => onEdit(setEmail, data)}
					errorMessage="Please provide a valid email address"
					errorFunc={(currentVal) => currentVal === "" || currentVal.search(/^\S+@\S+$/) === -1}/>
			</Cell>
			<Cell>
				<LabelledErrorAttachedTextInput
					className="LabelledInput"
					name="Username"
					labelPosition="Left"
					formData={userName}
					setFormData={data => onEdit(setUserName, data)}
					errorMessage="Please provide a valid display name."
					errorFunc={(currentVal) => currentVal === ""} />
			</Cell>				
			<Cell
				cellStyles={{ gridColumn: "2 / 3" }}>
				<Flex
					direction="Row"
					mainAlign="End">
					<Button
						classname="SaveButton"
						title="Save"
						disabled={!changesMade}
						onClick={() => AccountService.updatePersonalData({
							email: email.Value,
							userName: userName.Value,
						})}/>
				</Flex>
			</Cell>
		</Grid>
	);
}

export default UpdateDetails;