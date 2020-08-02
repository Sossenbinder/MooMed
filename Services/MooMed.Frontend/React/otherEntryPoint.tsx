// Framework
import * as ReactDOM from "react-dom";
import * as React from "react";
import { BrowserRouter  } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "data/store";

// Components
import OtherMain from "views/Pages/Other/OtherMain";

window.onload = () => {
	ReactDOM.render(
		<Provider store={store}>
			<BrowserRouter>
				<OtherMain />
			</BrowserRouter>
		</Provider>,
		document.getElementById("landingContainer"));
};