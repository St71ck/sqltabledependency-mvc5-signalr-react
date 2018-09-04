import React, { Component } from 'react';

class RadioInput extends Component {
    handleClick = () => {
        this.props.onChoiceSelect( this.props.choice );
    }

    render() {
        var disable = this.props.disable;
        var classString = !disable ?  "radio" :  "radio disabled";
        return (
            <div className={classString}>
                <label className={this.props.classType}>
                    <input type="radio" name="optionsRadios" id={this.props.index} value={this.props.choice} onChange={this.handleClick}  />
                    {this.props.choice}
                </label>
            </div>
        );
    }
}

export default RadioInput;