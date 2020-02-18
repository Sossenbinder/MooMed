// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import Flex from "views/Components/General/Flex";

type Props = {
    account: Account;
    
    profileAccount: Account;
}

export const ProfileFull: React.FC<Props> = ({account, profileAccount}) => {

    const isLoggedInAccount = React.useMemo(() => account.id === profileAccount.id, [account, profileAccount]);

    return (
        <Flex className="profileFull">
            <div className="header">                    
                <img className="picture" src={account.profilePicturePath} alt="Profile picture" />
                <h2 className="name">{account.userName}</h2>   
            </div>
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