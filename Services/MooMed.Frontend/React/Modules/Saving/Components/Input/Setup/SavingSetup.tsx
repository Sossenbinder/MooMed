// Framework
import * as React from "react";
import { connect } from "react-redux";
import { Switch, Route } from "react-router-dom";

// Components
import Flex from "common/components/Flex"
import SavingSetupStepBasics from "./SavingSetupStepBasics";
import SavingSetupStepWelcome from "./SavingSetupStepWelcome";
import NavigationArrow from "./NavigationArrow";

// Functionality
import { useServices } from "hooks/useServices";
import { reducer as savingConfigurationReducer } from "modules/saving/reducer/SavingConfigurationReducer";

// Types
import { BasicSavingInfo, SavingInfo } from "modules/saving/types";

import "./Styles/SavingSetup.less";

enum SetupStep {
	Welcome,
	Basics,
	Free,
}

type NavigationInfo = {
	unAvailable?: boolean;
	disabled?: boolean;
	disabledToolTip?: string;
	navStep: SetupStep;
}

type StepInfo = {
	nextInfo?: NavigationInfo;
	prevInfo?: NavigationInfo;
}

type Props = {
	savingInfo: SavingInfo;
	pathName: string;

	updateSavingInfo(savingInfo: SavingInfo): void;
}

const routePrefix = "/saving/setup/";

export const SavingSetup: React.FC<Props> = ({ savingInfo, pathName, updateSavingInfo }) => {

	const { SavingService } = useServices();

	const [networkCallInProgress, setNetworkCallInProgess] = React.useState<boolean>(true);
	const [currentStep, setCurrentStep] = React.useState<SetupStep>(undefined);

	const onBasicSettingsUpdate = React.useCallback((basicSavingInfo: BasicSavingInfo) => {
		savingInfo.basicSavingInfo = basicSavingInfo;
		updateSavingInfo(savingInfo);
	}, [savingInfo.basicSavingInfo]);
	
	const stepMap: Map<SetupStep, StepInfo> = React.useMemo(() => {
		return new Map<SetupStep, StepInfo>([
			[SetupStep.Welcome, {
				nextInfo: {
					navStep: SetupStep.Basics,
					disabled: savingInfo.currency === undefined,
					disabledToolTip: "No currency picked yet"
				},
			}],
			[SetupStep.Basics, {
				nextInfo: {
					navStep: SetupStep.Free,
				},
				prevInfo: {
					navStep: SetupStep.Welcome,
				},
			}],
			[SetupStep.Basics, {
				nextInfo: {
					navStep: SetupStep.Free,
				},
				prevInfo: {
					navStep: SetupStep.Welcome,
				},
			}]
		]);
	}, [savingInfo.currency]);

	React.useEffect(() => {
		const plainRoute = pathName.substring(pathName.lastIndexOf("/") + 1);
		const step: SetupStep = SetupStep[plainRoute] as SetupStep ?? SetupStep.Welcome;
		setCurrentStep(step);
	}, [pathName]);

	const currentStepInfo = stepMap.get(currentStep);

	if (currentStepInfo === undefined) {
		return null;
	}

	const nextInfo = currentStepInfo.nextInfo;
	const prevInfo = currentStepInfo.prevInfo;

	return (
		<Flex className="SavingSetup">
			<Flex 
				className="NavigationArrowContainer"
				direction="Column">
				<If condition={prevInfo !== undefined && !prevInfo.unAvailable}>
					<NavigationArrow
						direction="Left"
						navTarget={`${routePrefix}${SetupStep[currentStepInfo.prevInfo.navStep]}`}
						disabled={prevInfo.disabled ?? false}
						toolTip={prevInfo.disabledToolTip ?? null}/>
				</If>
			</Flex>
			<Flex className="StepComponent">
				<Choose>
					<If condition={networkCallInProgress}>

					</If>
					<Otherwise>
						<Switch>
							<Route
								path={`${routePrefix}${SetupStep[SetupStep.Welcome]}`} 
								render={() => (
									<SavingSetupStepWelcome 
										currency={savingInfo.currency} />
								)} 
							/>
							<Route
								path={`${routePrefix}${SetupStep[SetupStep.Basics]}`} 
								render={() => (
									<SavingSetupStepBasics
										basicSavingInfo={savingInfo.basicSavingInfo}
										onUpdate={onBasicSettingsUpdate}
										currency={savingInfo.currency} />
								)} 
							/>
						</Switch>
					</Otherwise>
				</Choose>
			</Flex>
			<Flex 
				className="NavigationArrowContainer"
				direction="Column">
				<If condition={nextInfo !== undefined && !nextInfo.unAvailable}>
					<NavigationArrow
						direction="Right"
						navTarget={`${routePrefix}${SetupStep[currentStepInfo.nextInfo.navStep]}`}
						disabled={nextInfo.disabled ?? false}
						toolTip={nextInfo.disabledToolTip ?? null} />
				</If>
			</Flex>
		</Flex>
	);
}

const mapDispatchToProps = (dispatch: any) => {
	return {
		updateSavingInfo: (savingInfo: SavingInfo) => dispatch(savingConfigurationReducer.update(savingInfo))
	}
}

export default connect(null, mapDispatchToProps)(SavingSetup);