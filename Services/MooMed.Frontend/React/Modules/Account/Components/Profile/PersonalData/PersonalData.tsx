// Framework
import * as React from "react"

// Components
import Flex from "common/components/Flex";
import UpdateDetails from "./UpdateDetails";
import UpdatePassword from "./UpdatePassword";
import Separator from "common/components/Separator";

// Types
import { Account } from "modules/Account/types";

import "./Styles/PersonalData.less";

type Props = {
	account: Account;
}

export const PersonalData: React.FC<Props> = ({ account }) => (
	<Flex
		className="PersonalData"
		direction="Column">
		<UpdateDetails
			account={account} />
		<Separator />
		<UpdatePassword
			account={account} />
	</Flex>
);

export default PersonalData