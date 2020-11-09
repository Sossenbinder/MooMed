// Framework
import * as React from "react";
import { AreaChart, XAxis, YAxis, CartesianGrid, Tooltip, Area, ResponsiveContainer } from "recharts";

// Components
import Flex from "common/components/Flex"

// Functionality

// Types

import "./Styles/SavingGrowthChart.less";

type Props = {

}

export const SavingGrowthChart: React.FC<Props> = () => {

	const data = [
		{
		  "name": "Page A",
		  "uv": 4000,
		  "pv": 2400,
		  "amt": 2400
		},
		{
		  "name": "Page B",
		  "uv": 3000,
		  "pv": 1398,
		  "amt": 2210
		},
		{
		  "name": "Page C",
		  "uv": 2000,
		  "pv": 9800,
		  "amt": 2290
		},
		{
		  "name": "Page D",
		  "uv": 2780,
		  "pv": 3908,
		  "amt": 2000
		},
		{
		  "name": "Page E",
		  "uv": 1890,
		  "pv": 4800,
		  "amt": 2181
		},
		{
		  "name": "Page F",
		  "uv": 2390,
		  "pv": 3800,
		  "amt": 2500
		},
		{
		  "name": "Page G",
		  "uv": 3490,
		  "pv": 4300,
		  "amt": 2100
		}
	  ];

	return (
		<Flex className="SavingGrowthChart">
			<ResponsiveContainer width="95%">
				<AreaChart data={data}>
					<XAxis dataKey="name" />
					<YAxis />
					<Tooltip />
					<Area type="monotone" dataKey="uv" stroke="#8884d8" fillOpacity={1} fill="url(#colorUv)" />
					<Area type="monotone" dataKey="pv" stroke="#82ca9d" fillOpacity={1} fill="url(#colorPv)" />
				</AreaChart>
			</ResponsiveContainer>
		</Flex>
	);
}

export default SavingGrowthChart;