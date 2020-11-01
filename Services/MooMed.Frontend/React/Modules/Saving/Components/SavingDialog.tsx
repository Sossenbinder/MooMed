// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import Flex from "common/components/Flex";
import Grid from "common/components/Grid/Grid";
import Cell from "common/components/Grid/Cell";
import SavingConfigurator from "./Input/SavingConfigurator";
import SavingDistributionChart from "./Charts/SavingDistributionChart";
import SavingGrowthChart from "./Charts/SavingGrowthChart";

// Functionality
import { ReduxStore } from "data/store";

// Types
import { SavingInfo } from "modules/saving/types";

import "./Styles/SavingDialog.less";

type Props = {
	savingInfo: SavingInfo
}

export const SavingDialog: React.FC<Props> = ({ savingInfo }) => {
	return (
		<Flex
			className="SavingDialog"
			direction="Column">
			<Grid
				className="SavingDialogGrid"		
				gridProperties={{
					gridTemplateColumns: "3fr 1fr",
					gridTemplateRows: "500px 200px",
				}}>
				<Cell
					cellStyles={{
						gridColumn: "1 / 3"
					}}>					
					<SavingConfigurator 
						savingInfo={savingInfo}/>
				</Cell>
				<Cell
					cellStyles={{
						gridColumn: "1 / 2"
					}}>
					<SavingGrowthChart />
				</Cell>
				<Cell>					
					<SavingDistributionChart />
				</Cell>
			</Grid>
		</Flex>
	);
}

const mapStateToProps = (store: ReduxStore): Props => {
	return {
		savingInfo: store.savingConfigurationReducer.data,
	};
}

export default connect(mapStateToProps)(SavingDialog);