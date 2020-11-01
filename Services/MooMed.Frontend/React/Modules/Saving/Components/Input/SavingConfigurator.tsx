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
			<If condition={!!savingInfo}>
			</If>
			<If condition={!savingInfo}>
				<SavingSetup />
			</If>
		</Flex>
	);
}

export default SavingConfigurator;