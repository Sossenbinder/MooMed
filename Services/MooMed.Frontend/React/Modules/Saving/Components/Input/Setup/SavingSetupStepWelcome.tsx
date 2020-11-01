// Framework
import * as React from "react"

// Components
import Flex from "common/components/Flex"

// Functionality

// Types

import "./Styles/SavingSetupStepWelcome.less";

type Props = {

}

export const SavingSetupStepWelcome: React.FC<Props> = () => {
	return (
		<Flex
			direction="Column"
			className="WelcomeDialog">
			<h2>Welcome to MooMed Saving &amp; Budgeting! </h2>
			<p>It seems like you don't have a savings profile configured yet - We will now guide you through a small setup to configure your personal savings.</p>
			<p>When finished, you will still be able to edit everything as you want.</p>
			<p>We will come up with personalized charts and diagrams helping you to budget and save money - At the bottom you will see your current outcome and income distribution.</p>
			<p>You can always navigate back and forth with the navigation arrows at the side.</p>
		</Flex>
	);
}

export default SavingSetupStepWelcome;