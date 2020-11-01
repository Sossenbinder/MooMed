// Framework
import * as React from "react";

// Components
import PopUpMessageHolder from "views/Components/Main/PopUpMessage/PopUpMessageHolder";
import LanguagePicker from "common/components/General/LanguagePicker";
import Flex from "common/components/Flex";

// Functionality

// Types

import "./Styles/CommonLandingPage.less";

type Props = {
	children: JSX.Element | JSX.Element[];
}

export const CommonLandingPage: React.FC<Props> = ({ children }) => {

	return (
		<Flex 
			space={"Between"}
			direction={"Column"}
			className={"logOnContentContainer"}>
			<PopUpMessageHolder />
			<Flex className={"mooMedLogoContainer"}>
				<Flex className={"mooMedLogo"}>
					MooMed
				</Flex>
			</Flex>
			{ children }
			<Flex
				direction={"Row"}
				mainAlign={"End"}
				className={"languagePicker"}>
				<LanguagePicker />
			</Flex>
		</Flex>
	);
}

export default CommonLandingPage;