// Framework
import * as React from "react";
import { $enum } from "ts-enum-util";

// Components
import Flex from "common/components/Flex";
import CurrencyItem from "./CurrencyItem";

// Functionality
import { currencySymbolMap } from "helper/currencyHelper";

// Types
import { Currency } from "enums/moomedEnums";

import "./Styles/SavingSetupStepWelcome.less";
import useServices from "hooks/useServices";

type Props = {
	currency: Currency;
}

export const SavingSetupStepWelcome: React.FC<Props> = ({ currency }) => {

	const { SavingService } = useServices();

	const currencies: Array<JSX.Element> = React.useMemo(() => {
		const currencyEnum = $enum(Currency);
		return currencyEnum
			.map((curr, index) => {
				return <CurrencyItem 
					text={currencySymbolMap.get(curr)}
					size={60}
					onClick={() => SavingService.setCurrency(curr)} 
					key={`${index}_${curr.toString()}`}
					isSelected={currency === curr}/>
			});
	}, [currency]);

	return (
		<Flex
			direction="Column"
			className="WelcomeDialog">
			<h2>Welcome to MooMed Saving &amp; Budgeting! </h2>
			<p>It seems like you either don't have a savings profile configured yet, or didn't finish - We will now guide you through a small setup to configure your personal savings.</p>
			<p>When finished, you will still be able to edit everything as you want. We will come up with personalized charts and diagrams helping you to budget and save money - At the bottom you will see your current outcome and income distribution.</p>
			<p>You can always navigate back and forth with the navigation arrows at the side. Now, please pick your currency first.</p>
			<Flex 
				className="CurrencyPicker"
				crossAlignSelf="Center"
				space="Between">
					{ currencies }
			</Flex>
		</Flex>
	);
}

export default SavingSetupStepWelcome;