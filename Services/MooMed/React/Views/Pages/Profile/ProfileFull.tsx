// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import Flex from "Views/Components/General/Flex";

type Props = {
	account: Account;
}

export const ProfileFull: React.FC<Props> = ({account}) => {
    return (
        <Flex className="profileFull">
            <div className="header">                    
                <img className="picture" src={account.profilePicturePath} alt="Profile picture" />
                <h2 className="name">{account.userName}</h2>   
            </div>            
        </Flex>
    );
}


const mapStateToProps = state => {
	return {
		account: state.accountReducer.account
	};
}

export default connect(mapStateToProps)(ProfileFull);