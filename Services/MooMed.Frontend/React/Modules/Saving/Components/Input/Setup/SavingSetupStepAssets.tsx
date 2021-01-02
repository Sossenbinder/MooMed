// Framework
import * as React from "react";

// Components
import Flex from "common/components/Flex";
import Separator from "common/components/Separator";
import Grid from "common/components/grid/Grid";
import Cell from "common/components/grid/Cell";
import TextInput from "common/components/general/input/general/TextInput";

// Functionality
import { currencySymbolMap } from "helper/currencyHelper";

// Types
import { AssetInfo } from "modules/saving/types";
import { Currency } from "enums/moomedEnums"

import "./Styles/SavingSetupStepAssets.less";

type Props = {
	assetInfo: AssetInfo;
	currency: Currency;
}

export const SavingSetupStepAssets: React.FC<Props> = ({ assetInfo, currency }) => {


	const [income, setIncome] = React.useState(assetInfo.cash);
	const incomeRef = React.useRef(income);
	incomeRef.current = income;

	const [rent, setRent] = React.useState(assetInfo.commodities);
	const rentRef = React.useRef(rent);
	rentRef.current = rent;

	const [groceries, setGroceries] = React.useState(assetInfo.commodities);
	const groceriesRef = React.useRef(groceries);
	groceriesRef.current = groceries;

	return (
		<Flex
			className="SavingSetupStepAssets"
			direction="Column">
			<h2>Assets</h2>
			<p>In order to know where we are starting off, please enter your current assets:</p>
			<Separator />
			<Grid
				className="SetupGrid"
				gridProperties={{
					gridTemplateColumns: "100px 225px",
					gridRowGap: "10px",
					gridTemplateRows: "38px 38px 38px",
				}}>
				<Cell>
					Your income:
					</Cell>
				<Cell>
					<Flex
						direction="Row">
						<TextInput
							name="Income"
							data={income}
							setData={val => setIncome}
							inputType="number" />
						<span className="Currency">
							{currencySymbolMap.get(currency)}
						</span>
					</Flex>
				</Cell>
			</Grid>
		</Flex>
	);
}

export default SavingSetupStepAssets;