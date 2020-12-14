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
import LoadingBubbles from "common/components/LoadingBubbles";

// Functionality
import { ReduxStore } from "data/store";
import { useServices } from "hooks/useServices";

// Types
import { SavingInfo } from "modules/saving/types";

import "./Styles/SavingDialog.less";

type Props = {
	savingInfo: SavingInfo
}

export const SavingDialog: React.FC<Props> = ({ savingInfo }) => {

	const { SavingService } = useServices();

	const [loading, setLoading] = React.useState<boolean>(true);

	React.useEffect(() => {
		const initSavingData = async () => {
			await SavingService.initSavingService();
			setLoading(false);
		}
		initSavingData();
	}, []);

	return (
		<Flex
			className="SavingDialog"
			direction="Column">
			<Choose>
				<When condition={!loading}>
					<Grid
						className="SavingDialogGrid"		
						gridProperties={{
							gridTemplateColumns: "3fr 1fr",
							gridTemplateRows: "50% 50%",
						}}>
						<Cell
							cellStyles={{
								gridColumn: "1 / 3"
							}}>					
							<SavingConfigurator 
								savingInfo={savingInfo}/>
						</Cell>
						<Cell>
							<SavingGrowthChart 
								savingInfo={savingInfo} />
						</Cell>
						<Cell>					
							<SavingDistributionChart />
						</Cell>
					</Grid>
				</When>
				<Otherwise>
					<Flex
						className="LoadingContainer"
						mainAlignSelf={"Center"}
						crossAlignSelf={"Center"}>
						<LoadingBubbles />
					</Flex>
				</Otherwise>
			</Choose>
		</Flex>
	);
}

const mapStateToProps = (store: ReduxStore): Props => {
	return {
		savingInfo: store.savingConfigurationReducer.data,
	};
}

export default connect(mapStateToProps)(SavingDialog);