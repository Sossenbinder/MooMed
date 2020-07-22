// Framework
import * as React from "react";
import { Link } from "react-router-dom";

// Components
import Flex from "common/Components/Flex";

// Functionality

// Types

export const Navigation: React.FC = () => {

	return (
		<Flex>
			<Link 
				to="/profileDetails/portfolio">
				Portfolio
			</Link>
		</Flex>
	);
}

export default Navigation;