// Framework
import * as React from "react"

// Components
import Flex from "common/components/Flex"
import ErrorAttachedTextInput from "common/components/General/Input/ErrorAttached/ErrorAttachedTextInput";

// Functionality
import useFormState from "hooks/useFormState";

// Types
import { Account } from "modules/Account/types";

import "./Styles/PersonalData.less";

type Props = {
	account: Account;
}

export const PersonalData: React.FC<Props> = ({ account }) => {

	const [email, setEmail] = useFormState<string>("");

	return (
		<Flex className="PersonalData">
			<ErrorAttachedTextInput
				name="Email"
				formData={email}
				setFormData={setEmail}
				errorMessage="Please provide a valid email address"
				errorFunc={(currentVal) => currentVal === "" || currentVal.search(/^\S+@\S+$/) === -1}/>
		</Flex>
	);
}

export default PersonalData