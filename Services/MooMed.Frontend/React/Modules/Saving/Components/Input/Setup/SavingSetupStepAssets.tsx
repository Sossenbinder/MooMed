// Framework
import * as React from "react";

// Components
import Flex from "common/components/Flex";

// Functionality

// Types

import "./Styles/SavingSetupStepAssets.less";

type Props = {

}

export const SavingSetupStepAssets: React.FC<Props> = () => {
	return (
		<Flex 
			className="SavingSetupStepAssets"
			direction="Column">
			<h2>Basics</h2>
			<p>Let's start off with some basics:</p>
		</Flex>
	);
}

export default SavingSetupStepAssets;