// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import Flex from "common/components/Flex";
import Separator from "common/components/Separator";
import Grid from "common/components/grid/Grid";
import Cell from "common/components/grid/Cell";
import TextInput from "common/components/general/input/general/TextInput";

// Functionality
import { currencySymbolMap } from "helper/currencyHelper";
import { updateAssets } from "modules/saving/reducer/SavingConfigurationReducer";

// Types
import { Assets, SavingInfo } from "modules/saving/types";
import { Currency } from "enums/moomedEnums"

import "./Styles/SavingSetupStepAssets.less";

type ReduxProps = {
	updateAssets(assets: Assets): void;
}

type Props = ReduxProps & {
	assets: Assets;
	currency: Currency;
}

export const SavingSetupStepAssets: React.FC<Props> = ({ assets, currency, updateAssets: updateAssets }) => {

	const { cash, commodities, debt, equity, estate } = assets ?? {};

	const onChange = React.useCallback(() => {
		const currentAssets = { ...assets };



		updateAssets(currentAssets);
	}, [assets]);

	const setIncome = (nr: number) => { };

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
							data={cash}
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

const mapDispatchToProps = (dispatch: any): ReduxProps => {
	return {
		updateAssets: (assets: Assets) => dispatch(updateAssets(assets))
	}
}

export default connect(null, mapDispatchToProps)(SavingSetupStepAssets);