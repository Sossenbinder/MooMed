// Framework
import * as React from "react";

// Components
import Flex from "views/Components/General/Flex";

// Functionality
import { Account } from "modules/Account/types";
import useServices from "hooks/useServices";

type Props = {
    account: Account;
    isSelf: boolean;
}

export const ProfileFull: React.FC<Props> = ({account, isSelf}) => {

    const { FriendsService } = useServices();

    return (
        <Flex className={"profileFull"}>
            <Flex 
                direction={"Column"}
                className={"header"}>                    
                <img className={"picture"} src={account.profilePicturePath} alt={"Profile picture"} />
                <h2 className={"name"}>{account.userName}</h2>
                <If condition={!isSelf}>
                    <button onClick={async () => await FriendsService.addFriend(account.id)}>
                        Add as friend
                    </button>
                </If>
            </Flex>
            <Flex>
                
            </Flex>
        </Flex>
    );
}

export default ProfileFull;