// Framework
import * as React from "react"

// Components
import Flex from "common/components/Flex"
import SavingSetupStepBasics from "./SavingSetupStepBasics";
import SavingSetupStepWelcome from "./SavingSetupStepWelcome";
import NavigationArrow from "./NavigationArrow";

// Functionality

// Types
import { Currency } from "enums/moomedEnums";

import "./Styles/SavingSetup.less";

enum SetupStep {
	Welcome,
	Basics,
}

type NavigationInfo = {
	unAvailable?: boolean;
	disabled?: boolean;
	disabledToolTip?: string;
	onClick?(currentStep: SetupStep): void;
}

type StepInfo = {
	stepComponent: JSX.Element;
	nextInfo?: NavigationInfo;
	prevInfo?: NavigationInfo;
}

export const SavingSetup: React.FC = () => {

	const [currency, setCurrency] = React.useState<Currency>(null);

	const [currentStep, setCurrentStep] = React.useState<SetupStep>(SetupStep.Welcome);

	const stepMap: Map<SetupStep, StepInfo> = React.useMemo(() => {
		return new Map<SetupStep, StepInfo>([
			[SetupStep.Welcome, {
				stepComponent: (
					<SavingSetupStepWelcome 
						currency={currency}/>
				),
				nextInfo: {
					onClick: _ => setCurrentStep(SetupStep.Basics),
					disabled: currency === null,
					disabledToolTip: "No currency picked yet"
				},
			}],
			[SetupStep.Basics, {
				stepComponent: (
					<SavingSetupStepBasics />
				),
				nextInfo: {
					onClick: _ => setCurrentStep(SetupStep.Basics)
				},
				prevInfo: {
					onClick: _ => setCurrentStep(SetupStep.Welcome)
				},
			}]
		]);
	}, [currency]);

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
						onClick={() => prevInfo.onClick(currentStep)}
						disabled={prevInfo.disabled ?? false}
						toolTip={prevInfo.disabledToolTip ?? null}/>
				</If>
			</Flex>
			<Flex className="StepComponent">
				{stepMap.get(currentStep)?.stepComponent}
			</Flex>
			<Flex 
				className="NavigationArrowContainer"
				direction="Column">
				<If condition={nextInfo !== undefined && !nextInfo.unAvailable}>
					<NavigationArrow
						direction="Right"
						onClick={() => nextInfo.onClick(currentStep)}
						disabled={nextInfo.disabled ?? false}
						toolTip={nextInfo.disabledToolTip ?? null} />
				</If>
			</Flex>
		</Flex>
	);
}

export default SavingSetup;