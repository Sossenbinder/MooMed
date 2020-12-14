// Framework
import * as React from "react"

// Components
import Flex from "common/components/Flex";
import Grid from "common/components/grid/Grid";
import Cell from "common/components/grid/Cell";
import Separator from "common/components/Separator";
import TextInput from "common/components/general/input/general/TextInput";

// Functionality
import { currencySymbolMap } from "helper/currencyHelper";

// Types
import { BasicSavingInfo, CashFlowItem } from "modules/saving/types";
import { CashFlow, CashFlowItemType, Currency } from "enums/moomedEnums"

import "./Styles/SavingSetupStepBasics.less";

type Props = {
	currency: Currency;
	basicSavingInfo?: BasicSavingInfo;

	onUpdate(basicSavingInfo: BasicSavingInfo): void;
}

export const SavingSetupStepBasics: React.FC<Props> = ({basicSavingInfo, currency, onUpdate}) => {

	let inputTimeout: number;

	const [income, setIncome] = React.useState(basicSavingInfo?.income?.amount);
	const incomeRef = React.useRef(income);
	incomeRef.current = income;

	const [rent, setRent] = React.useState(basicSavingInfo?.rent?.amount);
	const rentRef = React.useRef(rent);
	rentRef.current = rent;

	const [groceries, setGroceries] = React.useState(basicSavingInfo?.groceries?.amount);
	const groceriesRef = React.useRef(groceries);
	groceriesRef.current = groceries;

	const updateItem = (val: number, localStateDispatch: React.Dispatch<number>) => {

		localStateDispatch(val);

		if (inputTimeout !== undefined)
		{
			window.clearTimeout(inputTimeout);
		}

		inputTimeout = window.setTimeout(() => {
			const newBasicSavingInfo: BasicSavingInfo = { ...basicSavingInfo } ?? { } as BasicSavingInfo;

			if (incomeRef.current){
				let cashFlowItem: CashFlowItem = newBasicSavingInfo?.income;
				if (!cashFlowItem) {
					cashFlowItem = {
						cashFlowItemType: CashFlowItemType.Income,
						flowType: CashFlow.Income,
						name: "Income",
					} as CashFlowItem;
				}

				if (cashFlowItem.amount != incomeRef.current)
				{
					cashFlowItem.amount = incomeRef.current;
					newBasicSavingInfo.income = cashFlowItem;
				}
			}

			if (rentRef.current){
				let cashFlowItem: CashFlowItem = newBasicSavingInfo?.rent;
				if (!cashFlowItem) {
					cashFlowItem = {
						cashFlowItemType: CashFlowItemType.Rent,
						flowType: CashFlow.Outcome,
						name: "Rent",
					} as CashFlowItem;
				}
				
				if (cashFlowItem.amount != rentRef.current)
				{
					cashFlowItem.amount = rentRef.current;
					newBasicSavingInfo.rent = cashFlowItem;
				}
			}

			if (groceriesRef.current){
				let cashFlowItem: CashFlowItem = newBasicSavingInfo?.groceries;
				if (!cashFlowItem) {
					cashFlowItem = {
						cashFlowItemType: CashFlowItemType.Groceries,
						flowType: CashFlow.Outcome,
						name: "Groceries",
					} as CashFlowItem;
				}
				
				if (cashFlowItem.amount != groceriesRef.current)
				{
					cashFlowItem.amount = groceriesRef.current;
					newBasicSavingInfo.groceries = cashFlowItem;
				}
			}

			onUpdate(newBasicSavingInfo);

			window.clearTimeout(inputTimeout);
		}, 750);
	};

	return (
		<Flex 
			className="SavingSetupStepBasics"
			direction="Column">
			<h2>Basics</h2>
			<p>Let's start off with some basics:</p>
			<Separator />
			<Grid
				className="SetupGrid"
				gridProperties={{
					gridTemplateColumns: "100px 225px",
					gridRowGap: "10px",
					gridTemplateRows: "38px 38px 38px",
				}}>
				<Cell>
					Your income:
				</Cell>
				<Cell>
					<Flex
						direction="Row">
						<TextInput
							name="Income"
							data={income}
							setData={val => updateItem(+val, setIncome)}
							inputType="number"/>
						<span className="Currency">
							{currencySymbolMap.get(currency)}
						</span>
					</Flex>
				</Cell>
				<Cell>
					Your rent:
				</Cell>
				<Cell>
					<Flex
						direction="Row">
						<TextInput
							name="Rent"
							data={rent}
							setData={val => updateItem(+val, setRent)}	
							inputType="number"/>
						<span className="Currency">
							{currencySymbolMap.get(currency)}
						</span>
					</Flex>
				</Cell>
				<Cell>
					Your groceries:
				</Cell>
				<Cell>
					<Flex
						direction="Row">
						<TextInput
							name="Groceries"
							data={groceries}
							setData={val => updateItem(+val, setGroceries)}	
							inputType="number"/>
						<span className="Currency">
							{currencySymbolMap.get(currency)}
						</span>
					</Flex>
				</Cell>
			</Grid>
		</Flex>
	);
}

export default SavingSetupStepBasics;