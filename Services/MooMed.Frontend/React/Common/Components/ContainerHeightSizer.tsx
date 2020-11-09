// Framework
import * as React from 'react'

interface State {
  height: number
}

interface Props {
  children: (state: State) => React.ReactNode
}

class ContainerHeightSizer extends React.Component<Props, State> {
  state = {
    height: 0
  }

  container: HTMLDivElement

  componentDidMount() {
    this.setState({
		height: this.container.getBoundingClientRect().height
    })
  }

  render() {
    return (
      <div ref={c => this.container = c}>
        {this.props.children(this.state)}
      </div>
    )
  }
}

export default ContainerHeightSizer