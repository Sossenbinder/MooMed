// Framework
import * as React from "react";
import { connect } from "react-redux";
import { useHistory } from "react-router-dom";

// Components
import Flex from "common/components/Flex"
import NavigationArrow, { Direction } from "./NavigationArrow";
import LoadingBubbles from "common/components/LoadingBubbles";

import SavingSetupStepBasics from "./SavingSetupStepBasics";
import SavingSetupStepWelcome from "./SavingSetupStepWelcome";
import SavingSetupStepAssets from "./SavingSetupStepAssets";

// Functionality
import { useServices } from "hooks/useServices";
import { usingBoolAsync } from "helper/utils/hookUtils";
import { reducer as savingConfigurationReducer } from "modules/saving/reducer/SavingConfigurationReducer";

// Types
import { BasicSavingInfo, SavingInfo } from "modules/saving/types";

import "./Styles/SavingSetup.less";

enum SetupStep {
	Welcome,
	Assets,
	Basics,
}

type NavigationInfo = {
	unAvailable?: boolean;
	disabled?: boolean;
	disabledToolTip?: string;
	navStep?: SetupStep;
	onClick?(): Promise<void>;
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
	const history = useHistory();

	const [networkCallInProgress, setNetworkCallInProgess] = React.useState<boolean>(true);

	const plainRoute = pathName.substring(pathName.lastIndexOf("/") + 1);
	const casedRoute = plainRoute.substring(0, 1).toUpperCase() + plainRoute.substring(1);
	const currentStep: SetupStep = SetupStep[casedRoute] as SetupStep ?? SetupStep.Welcome;

	const onBasicSettingsUpdate = React.useCallback((basicSavingInfo: BasicSavingInfo) => {
		savingInfo.basicSavingInfo = basicSavingInfo;
		updateSavingInfo(savingInfo);
	}, [savingInfo.basicSavingInfo]);

	const stepMap: Map<SetupStep, StepInfo> = React.useMemo(() => {
		return new Map<SetupStep, StepInfo>([
			[SetupStep.Welcome, {
				nextInfo: {
					navStep: SetupStep.Assets,
					disabled: savingInfo.currency === undefined,
					disabledToolTip: "No currency picked yet"
				},
			}],
			[SetupStep.Assets, {
				nextInfo: {
					navStep: SetupStep.Basics,
				},
				prevInfo: {
					navStep: SetupStep.Welcome,
				}
			}],
			[SetupStep.Basics, {
				nextInfo: {
					disabled: savingInfo.basicSavingInfo?.groceries === undefined
						|| savingInfo.basicSavingInfo?.income === undefined
						|| savingInfo.basicSavingInfo?.rent === undefined,
					disabledToolTip: "Not all necessary items filled yet",
					onClick: async () => {
						await usingBoolAsync(setNetworkCallInProgess, async () => await SavingService.saveBasicSettings());
						history.push("saving");
					},
				},
				prevInfo: {
					navStep: SetupStep.Assets,
				},
			}],
		]);
	}, [currentStep, savingInfo.currency, savingInfo.basicSavingInfo]);

	const createNavArrow = (navInfo: NavigationInfo, direction: keyof typeof Direction) => (
		<Flex
			className="NavigationArrowContainer"
			direction="Column">
			<If condition={navInfo !== undefined && !navInfo.unAvailable}>
				<NavigationArrow
					direction={direction}
					navTarget={navInfo?.navStep !== undefined ? `${routePrefix}${SetupStep[navInfo.navStep]}` : null}
					disabled={navInfo.disabled ?? false}
					toolTip={navInfo.disabledToolTip ?? null}
					onClick={navInfo.onClick} />
			</If>
		</Flex>
	);

	const currentStepInfo = stepMap.get(currentStep);

	if (currentStepInfo === undefined) {
		return null;
	}

	return (
		<Flex className="SavingSetup">
			{ createNavArrow(currentStepInfo.prevInfo, "Left")}
			<Flex className="StepComponent">
				<Choose>
					<If condition={networkCallInProgress}>
						<LoadingBubbles />
					</If>
					<Otherwise>
						<Choose>
							<When condition={currentStep === SetupStep.Basics}>
								<SavingSetupStepBasics
									basicSavingInfo={savingInfo.basicSavingInfo}
									onUpdate={onBasicSettingsUpdate}
									currency={savingInfo.currency} />
							</When>
							<When condition={currentStep === SetupStep.Assets}>
								<SavingSetupStepAssets
									assetInfo={savingInfo.assetInfo}
									currency={savingInfo.currency} />
							</When>
							<Otherwise>
								<SavingSetupStepWelcome
									currency={savingInfo.currency} />
							</Otherwise>
						</Choose>
					</Otherwise>
				</Choose>
			</Flex>
			{ createNavArrow(currentStepInfo.nextInfo, "Right")}
		</Flex>
	);
}

const mapDispatchToProps = (dispatch: any) => {
	return {
		updateSavingInfo: (savingInfo: SavingInfo) => dispatch(savingConfigurationReducer.update(savingInfo))
	}
}

export default connect(null, mapDispatchToProps)(SavingSetup);