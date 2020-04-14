// Framework
import * as React from "react";
import { connect } from 'react-redux';
import { Link } from "react-router-dom";

// Components
import SearchBar from "modules/Search/View/SearchBar"
import SmallAccountManager from "views/Components/Account/SmallAccountManager";

// Functionality
import { Account } from "modules/Account/types";
import { ReduxStore } from "data/store";

interface Props {
	account: Account;
}

export const NavBar: React.FC<Props> = () => 
(
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


const mapStateToProps = (store: ReduxStore) => {
	return {
		account: store.accountReducer.data[0],
	};
}

export default connect(mapStateToProps)(NavBar);;