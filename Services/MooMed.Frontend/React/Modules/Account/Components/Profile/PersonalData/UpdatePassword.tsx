// Framework
import * as React from "react"

// Components
import Flex from "common/components/Flex"
import Grid from "common/components/Grid/Grid";
import Cell from "common/components/Grid/Cell";
import Button from "common/components/general/input/Buttons/Button";
import LabelledErrorAttachedTextInput from "common/components/general/input/ErrorAttached/LabelledErrorAttachedTextInput";

// Functionality
import useFormState from "hooks/useFormState";
import useServices from "hooks/useServices";

// Types
import { Account } from "modules/Account/types";
import { FormData } from "definitions/Forms";

import "./Styles/UpdatePassword.less";

type Props = {
	account: Account;
}

export const UpdatePassword: React.FC<Props> = () => {

	const [oldPassword, setOldPassword] = useFormState<string>("");
	const [newPassword, setNewPassword] = useFormState<string>("");
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
			className="UpdatePassword"
			gridProperties={{
				gridTemplateColumns: "repeat(1fr)",
				gridRowGap: "10px",
				gridTemplateRows: "1fr 2fr",
			}}>
			<Cell
				cellStyles={{ gridColumn: "1 / 3" }}>
				<p><u>Change your password:</u></p>
			</Cell>
			<Cell>
				<LabelledErrorAttachedTextInput
					className="LabelledInput"
					name="Old password"
					labelPosition="Left"
					formData={oldPassword}
					inputType="password"
					setFormData={data => onEdit(setOldPassword, data)}
					errorMessage="Please provide a valid password"
					errorFunc={(currentVal) => currentVal === ""}/>
			</Cell>
			<Cell>
				<LabelledErrorAttachedTextInput
					className="LabelledInput"
					name="New password"
					labelPosition="Left"
					formData={newPassword}
					inputType="password"
					setFormData={data => onEdit(setNewPassword, data)}
					errorMessage="Please provide a valid password."
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
						onClick={async () => await AccountService.updatePassword({
							oldPassword: oldPassword.Value,
							newPassword: newPassword.Value,
						})}/>
				</Flex>
			</Cell>
		</Grid>
	);
}

export default UpdatePassword;