// Framework
import * as React from "react"

// Components
import Flex from "./Flex";

type Props = {
	borderColor?: string;
}

export const Separator: React.FC<Props> = ({ borderColor = "black" }) => {
	return (
		<Flex 
			style={{borderTop: `1px solid ${borderColor}`}}
		/>
	);
}

export default Separator