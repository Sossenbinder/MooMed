// Framework
import * as React from "react";
import { PieChart, Pie, ResponsiveContainer } from "recharts";

// Components
import Flex from "common/components/Flex"

// Functionality

// Types

import "./Styles/SavingDistributionChart.less";

type Props = {

}

export const SavingDistributionChart: React.FC<Props> = () => {

	const data = [
		{
		  "name": "Group A",
		  "value": 400
		},
		{
		  "name": "Group B",
		  "value": 300
		},
		{
		  "name": "Group C",
		  "value": 300
		},
		{
		  "name": "Group D",
		  "value": 200
		},
		{
		  "name": "Group E",
		  "value": 278
		},
		{
		  "name": "Group F",
		  "value": 189
		}
	  ];

	return (
		<Flex 
			className="SavingDistributionChart"
			direction="Column">
			<Flex mainAlign="Center">
				<h3>
					Saving distribution
				</h3>
			</Flex>
			<ResponsiveContainer width="95%" height="95%">
				<PieChart>
					<Pie data={data} dataKey="value" nameKey="name" outerRadius={50} fill="#8884d8" label/>
				</PieChart>
			</ResponsiveContainer>
		</Flex>
	);
}

export default SavingDistributionChart;