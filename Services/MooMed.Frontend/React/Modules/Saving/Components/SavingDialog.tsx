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
import { CashFlow } from "enums/moomedEnums";

import "./Styles/SavingDialog.less";

type Props = {
	savingInfo: SavingInfo
}

type CashFlowSummary = {
	totalExpenses: number;
	totalIncome: number;
	netFlow: number;
}

export const SavingDialog: React.FC<Props> = ({ savingInfo }) => {

	const { SavingService } = useServices();

	const [loading, setLoading] = React.useState<boolean>(true);

	const flowSummary: CashFlowSummary = React.useMemo(() => {

		const summary: CashFlowSummary = {
			netFlow: 0,
			totalExpenses: 0,
			totalIncome: 0,
		}

		if (savingInfo === undefined) {
			return summary;
		}

		savingInfo.freeFormSavingInfo?.forEach(x => {
			if (x.flowType === CashFlow.Income) {
				summary.totalIncome += x.amount;
			} else {
				summary.totalExpenses += x.amount;
			}
		});

		const basicSavingInfo = savingInfo.basicSavingInfo;

		summary.totalIncome += basicSavingInfo.income.amount;

		summary.totalExpenses += basicSavingInfo.rent.amount;
		summary.totalExpenses += basicSavingInfo.groceries.amount;

		summary.netFlow = summary.totalIncome - summary.totalExpenses;

		return summary;
	}, [savingInfo]);

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
								savingInfo={savingInfo} />
						</Cell>
						<Cell>
							<SavingGrowthChart
								netFlow={flowSummary.netFlow} />
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