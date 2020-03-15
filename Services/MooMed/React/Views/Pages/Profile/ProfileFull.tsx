// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import Flex from "views/Components/General/Flex";

// Functionality
import { Account } from "modules/Account/types";
import useServices from "hooks/useServices";

type Props = {
    account: Account;

    accountId: number;
}

export const ProfileFull: React.FC<Props> = ({account, accountId}) => {

    const { FriendsService } = useServices();

    const isSelf = React.useMemo(() => account.id === accountId, [account, accountId]);

    return (
        <Flex className={"profileFull"}>
            <Flex 
                direction={"Column"}
                className={"header"}>                    
                <img className={"picture"} src={account.profilePicturePath} alt={"Profile picture"} />
                <h2 className={"name"}>{account.userName}</h2>
                <If condition={!isSelf}>
                    <button onClick={async () => await FriendsService.addFriend(accountId)}>
                        Add as friend
                    </button>
                </If>
            </Flex>
            <Flex>
                
            </Flex>
        </Flex>
    );
}


const mapStateToProps = state => {
	return {
		account: state.accountReducer.account
	};
}

export default connect(mapStateToProps)(ProfileFull);