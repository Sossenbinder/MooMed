export function calculateCompoundInterest(
	initialAmount: number,
	interestRate: number,
	years: number){
	const formulaCompatibleInterest = 1 + (interestRate / 100);

	return Number((initialAmount * (formulaCompatibleInterest) ** years).toFixed(2));
}

export function calculateCompoundInterestWithCashflow(
	initialAmount: number,
	interestRate: number,
	yearlyIncrease: number,
	years: number){
	
	let compositeSum = initialAmount;
	for (var i = years; i >= 0; --i)
	{
		compositeSum += calculateCompoundInterest(compositeSum + yearlyIncrease, interestRate, i);
	}
	
	return compositeSum;
}