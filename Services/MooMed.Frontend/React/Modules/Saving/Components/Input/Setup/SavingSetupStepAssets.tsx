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
import { reducer as savingConfigurationReducer } from "modules/saving/reducer/SavingConfigurationReducer";

// Types
import { AssetInfo } from "modules/saving/types";
import { Currency } from "enums/moomedEnums"

import "./Styles/SavingSetupStepAssets.less";

type Props = {
	assetInfo: AssetInfo;
	currency: Currency;
}

export const SavingSetupStepAssets: React.FC<Props> = ({ assetInfo: { cash, commodities, debt, equity, estate }, currency }) => {

	const onChange = React.useCallback(() => {

	}, []);

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
							data={cash}
							setData={val => setIncome(+val)}
							inputType="number" />
						<span className="Currency">
							{currencySymbolMap.get(currency)}
						</span>
					</Flex>
				</Cell>
				<Cell>
					Your income:
				</Cell>
				<Cell>
					<Flex
						direction="Row">
						<TextInput
							name="Income"
							data={income}
							setData={val => setIncome(+val)}
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