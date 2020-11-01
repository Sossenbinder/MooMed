// Framework
import * as React from "react"
import { VictoryPie } from "victory";

// Components
import Flex from "common/components/Flex"

// Functionality

// Types

import "./Styles/SavingDistributionChart.less";

type Props = {

}

export const SavingDistributionChart: React.FC<Props> = () => {

	const data = [
		{x: 1, y: 13000},
		{x: 2, y: 16500},
		{x: 3, y: 14250},
		{x: 4, y: 19000}
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
			<VictoryPie
				data={data}
			/>
		</Flex>
	);
}

export default SavingDistributionChart;