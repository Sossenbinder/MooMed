// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import ProfileFull from "./ProfileFull";
import Flex from "Common/Components/Flex";

// Functionality
import { Account } from "modules/Account/types";
import useServices from "hooks/useServices";
import { ReduxStore } from "data/store";

import "./Styles/Profile.less";

type Props = {
	accountId: number;

	account: Account;
}

export const Profile: React.FC<Props> = ({ accountId, account }) => {

	const [displayAccount, setDisplayAccount] = React.useState<Account>(null);

	const { AccountService } = useServices();

	const isSelf = React.useCallback(() => !accountId || account.id === accountId, [account, accountId])

	const fetchOtherAccount = React.useCallback(async (accountId: number) => {
			const account = await AccountService.getAccount(accountId);
			setDisplayAccount(account);
	}, []);

	React.useEffect(() => {
		isSelf() ? setDisplayAccount(account) : fetchOtherAccount(accountId);
	}, [accountId, account]);

	return (
		<Flex 
			className="Profile">
			<If condition={displayAccount != null}>
				<ProfileFull 
					account={displayAccount}
					isSelf={isSelf()} />        
			</If>
		</Flex>
	);
}

const mapStateToProps = (store: ReduxStore) => {
	return {
		account: store.accountReducer.data[0],
	};
}

export default connect(mapStateToProps)(Profile);