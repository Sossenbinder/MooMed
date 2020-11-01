// Framework
import * as React from "react"
import { VictoryTheme, VictoryChart, VictoryLine } from "victory";

// Components
import Flex from "common/components/Flex"

// Functionality

// Types

type Props = {

}

export const SavingGrowthChart: React.FC<Props> = () => {
	return (
		<VictoryChart
			theme={VictoryTheme.material}>
			<VictoryLine
				style={{
					data: { stroke: "#c43a31" },
					parent: { border: "1px solid #ccc"}
				}}
				data={[
					{ x: 1, y: 2 },
					{ x: 2, y: 3 },
					{ x: 3, y: 5 },
					{ x: 4, y: 4 },
					{ x: 5, y: 7 }
				]}
			/>
		</VictoryChart>
	);
}

export default SavingGrowthChart;