import * as React from "react";
import AsyncEvent from "helper/Events/AsyncEvent";

export let ClickEvent = new AsyncEvent<MouseEvent>();

export default class GlobalClickCapturer extends React.Component {

	componentDidMount() {
		document.body.addEventListener('click', this._onClick);
	}

	componentWillUnmount() {
		document.body.removeEventListener('click', this._onClick);
	}

	async _onClick(event: MouseEvent) {
		await ClickEvent.Raise(event);
	}

	render() {
		return (
			<div style={{ height: "100%", width: "100%" }}>
				{this.props.children}
			</div>
		);
	}
}