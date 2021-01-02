// Framework
import * as React from "react";
import { LineChart, XAxis, YAxis, Tooltip, Line, ResponsiveContainer } from "recharts";
import * as moment from 'moment';

// Components
import Flex from "common/components/Flex"

// Functionality
import { calculateCompoundInterestWithCashflow } from "modules/saving/utils/savingUtils";

import "./Styles/SavingGrowthChart.less";

const noInterest = "No Interest";
const onePercentInterest = "1% Interest";
const twoPercentInterest = "2% Interest";
const fourPercentInterest = "4% Interest";
const sixPercentInterest = "6% Interest";
const eigthPercentInterest = "8% Interest";

type Props = {
	netFlow: number;
}

type ChartData = {
	Year: number;
	[noInterest]: string;
	[onePercentInterest]: string;
	[twoPercentInterest]: string;
	[fourPercentInterest]: string;
	[sixPercentInterest]: string;
	[eigthPercentInterest]: string;
}

export const SavingGrowthChart: React.FC<Props> = ({ netFlow }) => {

	const currentWealth = 0 + netFlow;

	const currentDate = moment().year();

	const dates = React.useMemo(() => {
		return [
			currentDate,
			currentDate + 1,
			currentDate + 2,
			currentDate + 5,
			currentDate + 10,
			currentDate + 20,
			currentDate + 45,
		];
	}, []);

	const valuesByDate = React.useMemo((): Array<ChartData> => dates.map(date => ({
		Year: date,
		[noInterest]: calculateCompoundInterestWithCashflow(currentWealth, 0, date - currentDate, netFlow),
		[onePercentInterest]: calculateCompoundInterestWithCashflow(currentWealth, 1, date - currentDate, netFlow),
		[twoPercentInterest]: calculateCompoundInterestWithCashflow(currentWealth, 2, date - currentDate, netFlow),
		[fourPercentInterest]: calculateCompoundInterestWithCashflow(currentWealth, 4, date - currentDate, netFlow),
		[sixPercentInterest]: calculateCompoundInterestWithCashflow(currentWealth, 6, date - currentDate, netFlow),
		[eigthPercentInterest]: calculateCompoundInterestWithCashflow(currentWealth, 8, date - currentDate, netFlow),
	})), [netFlow, dates]);

	return (
		<Flex className="SavingGrowthChart">
			<ResponsiveContainer width="95%">
				<LineChart data={valuesByDate}>
					<XAxis dataKey="Year" />
					<YAxis />
					<Tooltip
						formatter={(value, name, props) => {
							return [`${value}€`, name, props];
						}}
					/>
					<Line type="monotone" dataKey={noInterest} stroke="#8884d8" />
					<Line type="monotone" dataKey={onePercentInterest} stroke="#82ca9d" />
					<Line type="monotone" dataKey={twoPercentInterest} stroke="#82ca9d" />
					<Line type="monotone" dataKey={fourPercentInterest} stroke="#82ca9d" />
					<Line type="monotone" dataKey={sixPercentInterest} stroke="#82ca9d" />
					<Line type="monotone" dataKey={eigthPercentInterest} stroke="#82ca9d" />
				</LineChart>
			</ResponsiveContainer>
		</Flex>
	);
}

export default SavingGrowthChart;