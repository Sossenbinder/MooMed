export function calculateCompoundInterest(
	initialAmount: number,
	interestRate: number,
	years: number) {
	const formulaCompatibleInterest = 1 + (interestRate / 100);

	return calculateInterest(initialAmount, formulaCompatibleInterest, years).toFixed(2);
}

export function calculateCompoundInterestWithCashflow(
	initialAmount: number,
	interestRate: number,
	years: number,
	yearlyIncrease: number) {

	const formulaCompatibleInterest = 1 + (interestRate / 100);

	const interestPowerYears = (formulaCompatibleInterest ** years);

	const compositeSum = initialAmount * interestPowerYears + yearlyIncrease * ((interestPowerYears - 1) / (formulaCompatibleInterest - 1));

	return `${compositeSum.toFixed(2)}`;
}

function calculateInterest(
	initialAmount: number,
	formulaCompatibleInterest: number,
	years: number) {
	return (initialAmount * (formulaCompatibleInterest) ** years);
}