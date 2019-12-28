import * as React from "react";
import { connect } from "react-redux";

interface IProps {
	account: Account;
}

interface IState {

}

class ProfileFull extends React.Component<IProps, IState> {
    render() {
        return (
            <div className="profileFull">
                <div className="header">                    
                    <img className="picture" src={this.props.account.profilePicturePath} alt="Profile picture" />
				    <h2 className="name">{this.props.account.userName}</h2>   
                </div>
            </div>
        );
    }
}


const mapStateToProps = state => {
	return {
		account: state.accountReducer.account
	};
}

export default connect(mapStateToProps)(ProfileFull);