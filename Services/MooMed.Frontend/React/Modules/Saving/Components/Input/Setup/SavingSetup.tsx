// Framework
import * as React from "react"

// Components
import Flex from "common/components/Flex"
import SavingSetupStepBasics from "./SavingSetupStepBasics";
import SavingSetupStepWelcome from "./SavingSetupStepWelcome";
import NavigationArrow from "./NavigationArrow";

// Functionality

// Types

import "./Styles/SavingSetup.less";

enum SetupStep {
	Welcome,
	Basics,
}

type StepInfo = {
	stepComponent: JSX.Element;
	onNext?(prevStep: SetupStep): void;
	onPrevious?(prevStep: SetupStep): void;
}

export const SavingSetup: React.FC = () => {

	const [currentStep, setCurrentStep] = React.useState<SetupStep>(SetupStep.Welcome);

	const stepMap: Map<SetupStep, StepInfo> = React.useMemo(() => {
		return new Map<SetupStep, StepInfo>([
			[SetupStep.Welcome, {
				stepComponent: (
					<SavingSetupStepWelcome />
				),
				onNext: _ => setCurrentStep(SetupStep.Basics),

			}],
			[SetupStep.Basics, {
				stepComponent: (
					<SavingSetupStepBasics />
				),
				onNext: _ => setCurrentStep(SetupStep.Basics),
				onPrevious: _ => setCurrentStep(SetupStep.Welcome),
			}]
		]);
	}, []);

	const currentStepInfo = stepMap.get(currentStep);

	if (currentStepInfo === undefined) {
		return null;
	}

	return (
		<Flex className="SavingSetup">
			<Flex 
				className="NavigationArrowContainer"
				direction="Column">
				<If condition={!!currentStepInfo.onPrevious}>
					<NavigationArrow
						direction="Left"
						onClick={() => currentStepInfo.onPrevious(currentStep)} />
				</If>
			</Flex>
			<Flex className="StepComponent">
				{stepMap.get(currentStep)?.stepComponent}
			</Flex>
			<Flex 
				className="NavigationArrowContainer"
				direction="Column">
				<If condition={!!currentStepInfo.onNext}>
					<NavigationArrow
						direction="Right"
						onClick={() => currentStepInfo.onNext(currentStep)} />
				</If>
			</Flex>
		</Flex>
	);
}

export default SavingSetup;