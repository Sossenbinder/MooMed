// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import Flex from "common/components/Flex";
import { ExchangeTradedList } from "./Grid/ExchangeTradedList";

// Functionality
import { ReduxStore } from "data/store";
import { ExchangeTradedItem } from "modules/stocks/types";

type Props = {
	exchangeTradeds: Array<ExchangeTradedItem>;
}

export const StocksDialog: React.FC<Props> = ({ exchangeTradeds }) => {
	return (
		<Flex
			direction={"Column"}>
			<h2>Here are some stocks to choose from:</h2>
			<ExchangeTradedList 
				exchangeTradeds={exchangeTradeds}/>
		</Flex>
	);
}

const mapStateToProps = (store: ReduxStore): Props => {
	return {
		exchangeTradeds: store.exchangeTradedsReducer.data,
	};
}

export default connect(mapStateToProps)(StocksDialog);