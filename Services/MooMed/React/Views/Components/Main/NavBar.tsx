import * as React from "react";
import { connect } from 'react-redux';
import { Link } from "react-router-dom";

import SearchBar from "./SearchBar/SearchBar"
import SmallAccountManager from "views/Components/Account/SmallAccountManager";

interface IProps {
    account: Account;
}

interface IState {

}

class NavBar extends React.Component<IProps, IState> {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div id="navBar" className="mainNavBar navbar navbar-expand-md navbar-dark bg-dark navbar-fixed-top">
				<Link to="/" className="navbar-brand">MooMed - Finance done right</Link>
                <div className="navBarDiv collapse navbar-collapse">
                    <ul className="navbar-nav mr-auto">
                        <li className="nav-item">
                            <Link to="/" className="nav-link">Home</Link>
                        </li>
                        <li className="nav-item">
                            <Link to="/About" className="nav-link">About</Link>
                        </li>
                        <li className="nav-item">
                            <Link to="/Contact" className="nav-link">Contact</Link>
                        </li>
                    </ul>
                    <div className="searchBar nav navbar-nav ml-auto">
                        <SearchBar />
                    </div>
                </div>
                <SmallAccountManager />
            </div>
        );
    }
}

const mapStateToProps = store => {
    return {
		account: store.accountReducer.account
    };
}

export default connect(mapStateToProps)(NavBar);;