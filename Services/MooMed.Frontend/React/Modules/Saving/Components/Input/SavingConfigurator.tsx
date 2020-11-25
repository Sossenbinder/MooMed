// Framework
import * as React from "react"
import { Redirect, Route, Switch } from "react-router";

// Components
import Flex from "common/components/Flex";
import SavingSetup from "./Setup/SavingSetup";

// Functionality

// Types
import { SavingInfo } from "modules/saving/types";

import "./Styles/SavingConfigurator.less";

type Props = {
	savingInfo: SavingInfo;
}

export const SavingConfigurator: React.FC<Props> = ({ savingInfo }) => {
	return (
		<Flex className="SavingConfigurator">
			<Switch>
				<Route 
					path={"/saving/setup"}
					render={info => (<SavingSetup 
							savingInfo={savingInfo} 
							pathName={info.location.pathname}/>
					)}
				/>
				<Redirect
					from="/saving"
					to="/saving/setup/welcome" 
				/>
			</Switch>
		</Flex>
	);
}

export default SavingConfigurator;