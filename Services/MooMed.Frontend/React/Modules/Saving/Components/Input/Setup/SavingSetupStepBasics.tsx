// Framework
import * as React from "react"

// Components
import Flex from "common/components/Flex"
import Separator from "common/components/Separator";
import LabelledTextInput from "common/components/general/input/general/LabelledTextInput";

// Functionality

// Types

import "./Styles/SavingSetupStepBasics.less";

type Props = {

}

export const SavingSetupStepBasics: React.FC<Props> = () => {
	
	const [salary, setSalary] = React.useState(0);
	const [rent, setRent] = React.useState(0);
	const [groceries, setGroceries] = React.useState(0);

	return (
		<Flex 
			className="SavingSetupStepBasics"
			direction="Column">
			<h2>Basics</h2>
			<p>Let's start off with some basics:</p>
			<Separator />
			<Flex
				className="Inputs"
				direction="Column">
				<LabelledTextInput<number>
					name="Salary"
					data={salary}
					labelText="Your salary:"
					setData={setSalary}	
					inputType="number"/>
				<LabelledTextInput<number>
					name="Rent"
					data={rent}
					labelText="Your rent:"
					setData={setRent}	
					inputType="number"/>
				<LabelledTextInput<number>
					name="Groceries"
					data={groceries}
					labelText="Your groceries:"
					setData={setGroceries}	
					inputType="number"/>
			</Flex>
		</Flex>
	);
}

export default SavingSetupStepBasics;