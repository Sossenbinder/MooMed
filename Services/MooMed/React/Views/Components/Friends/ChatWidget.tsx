import * as React from "react";

import "Views/Components/Friends/Styles/ChatWidget.less";

interface IProps {

}

interface IState {
    minimized: boolean;
}

class ChatWidget extends React.Component<IProps, IState> {
    constructor(props) {
        super(props);

        this.state = {
            minimized: true
        };
    }

    render() {
        return (
            <div className="chatWidget">
                <div className="chatWidgetBar" onClick={this._minimizeChat}>
                    Chat
                </div>
                {!this.state.minimized &&
                    <div className="chatWidgetContent">
                        <p>Maximized</p>
                    </div>
                }
            </div>
        );
    }

    _minimizeChat = () => {
        this.setState({
            minimized: !this.state.minimized
        });
    }
}

export default ChatWidget;