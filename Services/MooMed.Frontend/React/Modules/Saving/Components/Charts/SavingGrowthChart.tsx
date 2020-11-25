// Framework
import * as React from "react";
import { LineChart, XAxis, YAxis, Tooltip, Line, ResponsiveContainer } from "recharts";
import * as moment from 'moment';

// Components
import Flex from "common/components/Flex"

// Functionality
import { calculateCompoundInterest } from "modules/saving/utils/savingUtils";

// Types
import { SavingInfo } from "modules/saving/types";

import "./Styles/SavingGrowthChart.less";

const noInterest = "No Interest";
const onePercentInterest = "1% Interest";
const twoPercentInterest = "2% Interest";
const fourPercentInterest = "4% Interest";
const sixPercentInterest = "6% Interest";
const eigthPercentInterest = "8% Interest";

type Props = {
	savingInfo: SavingInfo;
}

type ChartData = {
	Year: number;
	[noInterest]: number;
	[onePercentInterest]: number;
	[twoPercentInterest]: number;
	[fourPercentInterest]: number;
	[sixPercentInterest]: number;
	[eigthPercentInterest]: number;
}

export const SavingGrowthChart: React.FC<Props> = ({ savingInfo }) => {

	const initialAmount = 5000;
	
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
		[noInterest]: calculateCompoundInterest(initialAmount, 0, date - currentDate),
		[onePercentInterest]: calculateCompoundInterest(initialAmount, 1, date - currentDate),
		[twoPercentInterest]: calculateCompoundInterest(initialAmount, 2, date - currentDate),
		[fourPercentInterest]: calculateCompoundInterest(initialAmount, 4, date - currentDate),
		[sixPercentInterest]: calculateCompoundInterest(initialAmount, 6, date - currentDate),
		[eigthPercentInterest]: calculateCompoundInterest(initialAmount, 8, date - currentDate),
	})), [dates]);

	return (
		<Flex className="SavingGrowthChart">
			<ResponsiveContainer width="95%">
				<LineChart data={valuesByDate}>
					<XAxis dataKey="Year" />
					<YAxis/>
					<Tooltip 
						formatter={(value, name, props) => {
							return [value, name, props];
						}}
					/>
					<Line type="monotone" dataKey={noInterest} stroke="#8884d8"/>
					<Line type="monotone" dataKey={onePercentInterest} stroke="#82ca9d"/>
					<Line type="monotone" dataKey={twoPercentInterest} stroke="#82ca9d"/>
					<Line type="monotone" dataKey={fourPercentInterest} stroke="#82ca9d"/>
					<Line type="monotone" dataKey={sixPercentInterest} stroke="#82ca9d"/>
					<Line type="monotone" dataKey={eigthPercentInterest} stroke="#82ca9d"/>
				</LineChart>
			</ResponsiveContainer>
		</Flex>
	);
}

export default SavingGrowthChart;