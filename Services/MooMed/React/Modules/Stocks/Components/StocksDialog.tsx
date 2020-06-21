// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import Flex from "common/components/Flex";
import { ExchangeTradedList } from "./Grid/ExchangeTradedList";

// Functionality
import { ReduxStore } from "data/store";
import { ExchangeTradedItem } from "modules/stocks/types";

type ReduxProps = {
	exchangeTradeds: Array<ExchangeTradedItem>;
}

export const StocksDialog = ({ exchangeTradeds }) => {
	return (
		<Flex
			direction={"Column"}>
			<h2>Here are some stocks to choose from:</h2>
			<ExchangeTradedList 
				exchangeTradeds={exchangeTradeds}/>
		</Flex>
	);
}

const mapStateToProps = (store: ReduxStore): ReduxProps => {
	return {
		exchangeTradeds: store.exchangeTradedsReducer.data,
	};
}

export default connect(mapStateToProps)(StocksDialog);