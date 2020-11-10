// Framework
import * as React from "react"

// Components
import Flex from "common/components/Flex";
import SavingSetup from "./Setup/SavingSetup";

// Functionality

// Types
import { SavingInfo } from "modules/saving/types";

import "./Styles/SavingConfigurator.less";

type Props = {
	savingInfo: SavingInfo;
}

export const SavingConfigurator: React.FC<Props> = ({ savingInfo }) => {
	return (
		<Flex className="SavingConfigurator">
			<SavingSetup 
				savingInfo={savingInfo}/>
		</Flex>
	);
}

export default SavingConfigurator;