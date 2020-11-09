// Framework
import * as React from "react";

// Components
import Flex from "common/components/Flex";
import Button from "common/components/general/input/Buttons/Button";

// Functionality
import useServices from "hooks/useServices";
import getTranslationForErrorCode from "Translations/helpers/IdentityErrorLookupHelper";

import "./Styles/AccountValidationDialog.less";
import { IdentityErrorCode } from "enums/moomedEnums";

type Props = {
	accountId: number;
	token: string
}

enum ValidationStep {
	Validating,
	Success,
	Failure,
}

type ValidationStepInfo = {
	info: React.ReactNode;

	showActionButton?: boolean;
	actionButtonTitle?: string;
	onActionButtonClick?(): Promise<void>;
}

export const AccountValidationDialog: React.FC<Props> = ({ accountId, token }) => {

	const [validationStep, setValidationStep] = React.useState<ValidationStep>(ValidationStep.Validating);
	const [identityError, setIdentityError] = React.useState<IdentityErrorCode>(IdentityErrorCode.Success);

	const { AccountValidationService } = useServices();

	const validationStepMap: Map<ValidationStep, ValidationStepInfo> = React.useMemo(() => {
		return new Map<ValidationStep, ValidationStepInfo>([
			[
				ValidationStep.Validating,
				{
					actionButtonTitle: "Validate",
					info: "Do you want to validate your account?",
					showActionButton: true,
					onActionButtonClick: async () => {
						const result = await AccountValidationService.validateRegistration(accountId, token);

						setValidationStep(result.success ? ValidationStep.Success : ValidationStep.Failure);

						if (!result.success) {
							setIdentityError(result.identityErrorCode);
						}
					},
				},
			],
			[
				ValidationStep.Success,
				{
					info: "Great - your account was validated successfully. You can now login.",
					showActionButton: false,
				}
			],
			[
				ValidationStep.Failure,
				{
					actionButtonTitle: "Back",
					info: (
						<>
							<p>Sadly your account could not be validated due to the following error:</p>

							{ getTranslationForErrorCode(identityError) }
						</>
					),
					onActionButtonClick: () => {
						setValidationStep(ValidationStep.Validating);
						setIdentityError(IdentityErrorCode.Success);
						return Promise.resolve();
					}
				}
			]
		]);
	}, []);

	const stepInfo: ValidationStepInfo = validationStepMap.get(validationStep);

	return (
		<Flex className="AccountValidationContainer">
			<Flex
				className="Dialog"
				direction="Column"
				crossAlign="Center">
					<h2>Account validation</h2>
					
					{ stepInfo.info }

					<Flex
						className="InputButtons"
						direction="Row"
						space="Between"
						crossAlign="End">
						<If condition={stepInfo.showActionButton ?? true}>
							<Button
								title={stepInfo.actionButtonTitle}
								onClick={stepInfo.onActionButtonClick}
							/>
						</If>
						<Button
							title={"Back to login"}
							onClick={() => {
								location.href = "/";
								return Promise.resolve();
							}}
						/>
					</Flex>
			</Flex>
		</Flex>
	);
}

export default AccountValidationDialog;