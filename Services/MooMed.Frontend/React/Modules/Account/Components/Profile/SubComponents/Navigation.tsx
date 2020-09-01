// Framework
import * as React from "react";
import { useHistory } from "react-router-dom";

// Components
import Flex from "common/Components/Flex";

// Functionality

// Types

import "./Styles/Navigation.less";

type LinkMetaData = {
	title: string;
	path: string;
}

const links: Array<LinkMetaData> = [
	{
		path: "/profileDetails/data",
		title: "Personal Data",
	},
	{
		path: "/profileDetails/portfolio",
		title: "Portfolio",
	}
]

export const Navigation: React.FC = () => {

	const history = useHistory();
	
	const navigationItems = React.useMemo(() => {
		return links.map(metaData => 
			(
				<Flex
					className="Entry"
					mainAlign="Center"
					onClick={() => history.push(metaData.path)}>
					{metaData.title}
				</Flex>
			)
		);
	}, []);

	return (
		<Flex
			className="Navigation"
			direction="Column">
			{ navigationItems }
		</Flex>
	);
}

export default Navigation;